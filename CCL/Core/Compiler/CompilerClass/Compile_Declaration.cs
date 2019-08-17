using System;
using System.Collections.Generic;

namespace CCL.Core
{
    public partial class Compiler
    {
        /// <summary>
        /// Compile a variable declaration
        /// </summary>
        /// <param name="allowFunction"></param>
        /// <param name="requireEOS"></param>
        /// <returns></returns>
        protected virtual object CompileDeclaration(bool allowFunction = true, bool requireEOS = true)
        {
            if (MatchToken(Token.Type.EOS))
            {
                return null;
            }

            if (MatchToken(Token.Type.TypeName))
            {
                string typeName = previous.text;
                TypeDef typedef = _assembly.GetTypeDef(typeName);
                bool isArray = false;
                int arraySize = 0;

                if (MatchToken(Token.Type.ArrayIndexBegin))
                {
                    isArray = true;
                    if (MatchToken(Token.Type.Numeric))
                    {
                        arraySize = Int32.Parse(previous.text);
                    }
                    Require(Token.Type.ClosingArrayIndex, "] Expected");
                }

                if (Peek().type == Token.Type.Dot)
                {
                    BackStep();
                    return CompileFunctionCall();
                }

                if (typedef.staticClass)
                {
                    throw new Exception("Cannot create an instance variable of a static class/struct");
                }

                Token name = Require(Token.Type.Identifier, "identifier expected");

                if (allowFunction && Peek().type == Token.Type.OpenParenthesis)
                {
                    return CompileFunctionDeclaration(typedef, name.text, isArray);
                }

                int refIndex = -1;
                if (isArray)
                {
                    object arr = Array.CreateInstance(typedef.type, arraySize);
                    refIndex = _cdata.AddMemoryObject(arr);
                }
                else
                {
                    refIndex = _cdata.AddMemoryObject(typedef.DefaultConstructor()());
                }

                _cdata._scopeResolver.Add(name.text, refIndex);

                if (Peek().type == Token.Type.Assign)
                {
                    BackStep();
                    object result = CompileAssignment();
                    Require(Token.Type.EOS, "; Expected");
                    return result;
                }
                Require(Token.Type.EOS, "; Expected");
                return null;
            }
            return CompileControlInstructionStatement();
        }

        /// <summary>
        /// Compile function declaration argument
        /// </summary>
        protected virtual void CompileFunctionArgumentDeclaration()
        {
            if (MatchToken(Token.Type.TypeName))
            {
                string typeName = previous.text;
                TypeDef typedef = _assembly.GetTypeDef(typeName);
                bool isArray = false;
                int arraySize = 0;

                if (MatchToken(Token.Type.ArrayIndexBegin))
                {
                    isArray = true;
                    if (MatchToken(Token.Type.Numeric))
                    {
                        arraySize = Int32.Parse(previous.text);
                    }
                    Require(Token.Type.ClosingArrayIndex, "] Expected");
                }

                Token name = Require(Token.Type.Identifier, "identifier expected");

                int refIndex = -1;
                if (isArray)
                {
                    object arr = Array.CreateInstance(typedef.type, arraySize);
                    refIndex = _cdata.AddMemoryObject(arr);
                }
                else
                {
                    refIndex = _cdata.AddMemoryObject(typedef.DefaultConstructor()());
                }

                _cdata._scopeResolver.Add(name.text, refIndex);
            }
        }

        /// <summary>
        /// Compile function declaration
        /// </summary>
        /// <param name="type"></param>
        /// <param name="identifier"></param>
        /// <returns></returns>
        protected virtual object CompileFunctionDeclaration(TypeDef type, string identifier, bool isArray = false)
        {
            string tempIdentifier = identifier;

            Require(Token.Type.OpenParenthesis, "( Expected");

            // Prebind the function to support recursion
            //_cdata._environment.Add(type.Cast((Func<object>)(() => { return null; }), _cdata));
            //int refIndex = _cdata._environment.Count - 1;
            //_cdata._scopeResolver.Add(identifier, refIndex);

            // Push local function scope
            _cdata._scopeResolver.AddScopeStack(tempIdentifier);
            _cdata._scopeResolver.SetScopeByID(tempIdentifier);

            // Process arguments
            if (Peek().type != Token.Type.ClosingParethesis)
            {
                int arrCount = 0;
                do
                {
                    CompileFunctionArgumentDeclaration();
                    arrCount += 1;
                }
                while (MatchToken(Token.Type.Comma));
                _cdata._scopeResolver.localArgumentCount = arrCount;
            }
            else
            {
                _cdata._scopeResolver.localArgumentCount = 0;
            }

            // continue function
            Require(Token.Type.ClosingParethesis, ") Expected");
            List<Func<object>> functions = new List<Func<object>>();
            Require(Token.Type.OpenBlock, "{ Expected");
            while (!isAtEnd && Peek().type != Token.Type.CloseBlock)
            {
                object compiled = CompileDeclaration(false);
                Func<object> function = CompilerUtility.ForceGetFunction(compiled, _cdata);
                if (function == null && compiled != null)
                {
                    function = () => { return compiled; };
                }
                if (function != null)
                {
                    functions.Add(function);
                }
            }
            _cdata._scopeResolver.SetScopeByID(ScopeResolver.GLOBAL_ID);
            Require(Token.Type.CloseBlock, "} Expected");

            Func<object>[] funcArray = functions.ToArray();

            Func<object> block = () =>
            {
                object output = null;
                for (int mn = 0; mn < funcArray.Length; mn += 1)
                {
                    output = funcArray[mn]();
                    if ((_cdata._runtimeFlags & (int)CompilerData.ControlInstruction.Break) != 0) break;
                    if ((_cdata._runtimeFlags & (int)CompilerData.ControlInstruction.Continue) != 0)
                    {
                        _cdata._runtimeFlags ^= (int)CompilerData.ControlInstruction.Continue;
                        break;
                    }
                    if ((_cdata._runtimeFlags & (int)CompilerData.ControlInstruction.Exit) != 0)
                    {
                        _cdata._runtimeFlags ^= (int)CompilerData.ControlInstruction.Exit;
                        return _cdata._returnValue;
                    }
                }
                return output;
            };

            object func;
            if (isArray)
            {
                func = type.CastArray(block, _cdata);
            }
            else
            {
                func = type.Cast(block, _cdata);
            }
            int refIndex = _cdata.AddMemoryObject(func);
            _cdata._scopeResolver.Add(identifier, refIndex);

            return null;
        }
    }
}