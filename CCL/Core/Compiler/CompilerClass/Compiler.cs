using System;
using System.Collections.Generic;

namespace CCL.Core
{
    public partial class Compiler
    {
        // The list of tokens passed in at compile  time
        private List<Token> _tokens;

        // Token parsing index
        private int _index;

        // Persistent Compiler Data
        private CompilerData _cdata;

        // Context Type at compileTime
        private Type _contextType;

        private Assembly _assembly;

        public Compiler(Token[] tokens, Type contextType = null)
        {
            _tokens = new List<Token>(tokens);
            _index = 0;
            _cdata = null;
            _contextType = contextType;
            _assembly = Assembly.main;
        }

        public Compiler(Token[] tokens, Assembly assembly, Type contextType = null)
        {
            _tokens = new List<Token>(tokens);
            _index = 0;
            _cdata = null;
            _contextType = contextType;
            _assembly = assembly;
        }

        /// <summary>
        /// Look at next token without incrementing index
        /// </summary>
        private Token Peek()
        {
            return _tokens[_index];
        }

        /// <summary>
        /// current token
        /// </summary>
        private Token previous
        {
            get
            {
                return _tokens[_index - 1];
            }
        }

        private bool LookAhead(params Token.Type[] types)
        {
            for (int i = 0; i < types.Length; i += 1)
            {
                if (isAtEnd) return false;
                if (_tokens[_index + i].type != types[i]) return false;
            }
            return true;
        }

        /// <summary>
        /// return current token and increment index
        /// </summary>
        private Token Step()
        {
            if (!isAtEnd) _index += 1;
            return previous;
        }

        /// <summary>
        /// decrement index
        /// </summary>
        private void BackStep()
        {
            if (_index > 0) _index -= 1;
        }

        /// <summary>
        /// Requires current token to be of a certain type and increments index, or throws exception
        /// </summary>
        private Token Require(Token.Type type, string errorMessage)
        {
            if (!isAtEnd)
            {
                if (_tokens[_index].type == type)
                {
                    _index += 1;
                    return previous;
                }
            }
            throw new Exception("Parsing Error [line " + _tokens[_index]._meta_lineNumber + "]: " + errorMessage);
        }

        /// <summary>
        /// Are we out of tokens?
        /// </summary>
        private bool isAtEnd
        {
            get
            {
                return _index >= _tokens.Count;
            }
        }

        /// <summary>
        /// Checks to see if the current token type is the spefified type
        /// </summary>
        private bool CheckType(Token.Type type)
        {
            if (isAtEnd) return false;
            return Peek().type == type;
        }

        /// <summary>
        /// Checks to see if the current token text matches
        /// </summary>
        private bool CheckToken(string str)
        {
            if (isAtEnd) return false;
            return Peek().text == str;
        }

        /// <summary>
        /// Checks to see if the current token matches one of several strings
        /// If so, returns true and increments index
        /// </summary>
        private bool MatchToken(params string[] values)
        {
            foreach (string value in values)
            {
                if (CheckToken(value))
                {
                    Step();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the current token matches one of several types
        /// If so, returns true and increments index
        /// </summary>
        private bool MatchToken(params Token.Type[] types)
        {
            foreach (Token.Type type in types)
            {
                if (CheckType(type))
                {
                    Step();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the current token matches one of several types
        /// If so, returns token and increments index
        /// </summary>
        private Token MatchTokenAndReturn(params Token.Type[] types)
        {
            foreach (Token.Type type in types)
            {
                if (CheckType(type))
                {
                    return Step();
                }
            }
            return null;
        }

        private int UpdateLineNumber(Token token = null)
        {
            if (token == null) _cdata._currentLine = previous._meta_lineNumber;
            else _cdata._currentLine = token._meta_lineNumber;
            return _cdata._currentLine;
        }

        private object CompileDefaultBinaryOperator(Func<object> nextFunc, params Token.Type[] tokenTypes)
        {
            object left = nextFunc();
            object output = null;

            while (MatchToken(tokenTypes))
            {
                Token op = previous;
                int line = UpdateLineNumber();
                object right = nextFunc();

                if (output == null) output = left;

                Type ltype = CompilerUtility.GetReturnType(output);
                Type rtype = CompilerUtility.GetReturnType(right);

                Operation.BinaryOperationDelegate del = _assembly.GetBinaryOperation(op.type, ltype, rtype);

                // This section was made for runtime type inference
                // May cause some weird issues, definitely check this out if there are problems
                if (del == null && ltype != typeof(Object) && rtype == typeof(Object)) del = _assembly.GetBinaryOperation(op.type, ltype, ltype);

                if (del == null && ltype == typeof(Object) && rtype != typeof(Object)) del = _assembly.GetBinaryOperation(op.type, rtype, rtype);
                // * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
                if (del == null) del = _assembly.GetBinaryOperation(op.type, typeof(Object), typeof(Object));

                if (del == null) throw new CompilationException("[line " + line + "] Cannot perform the operation " + op.text + " on " + ltype.Name + " and " + rtype.Name);

                output = del(output, right, _cdata);
            }
            return (output != null) ? (object)output : left;
        }

        /// <summary>
        /// Main compilation entry
        /// </summary>
        /// <returns></returns>
        ///
        protected virtual CompiledScript _InternalCompile()
        {
            _cdata = new CompilerData(_assembly);
            List<Func<object>> functions = new List<Func<object>>();

            while (!isAtEnd)
            {
                Func<object> function = null;
                object compiled = CompileDeclaration();
                if (compiled == null) continue;
                Type returnType = CompilerUtility.GetReturnType(compiled);

                TypeDef typeDef = _assembly.GetTypeDef(returnType);

                if (CompilerUtility.IsFunc(compiled))
                {
                    if (compiled.GetType() != typeof(Func<object>))
                    {
                        function = typeDef.ToGenericFunction(compiled, _cdata);
                    }
                    else
                    {
                        function = (Func<object>)compiled;
                    }
                }
                else if (CompilerUtility.IsReference(compiled))
                {
                    function = ((Reference)compiled).CreateGetFunction(_cdata);
                }
                else
                {
                    function = () => { return compiled; };
                }
                functions.Add(function);
            }
            Func<object>[] funcArray = functions.ToArray();

            CompiledScript compiledScript = new CompiledScript(() =>
            {
                object output = null;
                for (int mn = 0; mn < funcArray.Length; mn += 1)
                {
                    output = funcArray[mn]();
                    if ((_cdata._runtimeFlags & (int)CompilerData.ControlInstruction.Exit) != 0) break;
                }
                _cdata._runtimeFlags = 0;
                return _cdata._returnValue;
            },
            _cdata);

            return compiledScript;
        }

        public virtual CompiledScript Compile()
        {
#if DEBUG
            return _InternalCompile();
#else
            try
            {
                return _InternalCompile();
            }
            catch (CompilationException e)
            {
                throw _cdata.CreateException(e.Message);
            }
            catch (RuntimeException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw _cdata.CreateException(e.Message);
            }
#endif
        }
    }
}