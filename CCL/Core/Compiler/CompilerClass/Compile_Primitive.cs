using System;
using System.Collections.Generic;

namespace CCL.Core
{
    public partial class Compiler
    {
        protected virtual object CompileArrayLiteral()
        {
            if (MatchToken(Token.Type.ClosingArrayIndex))
            {
                return new object[0];
            }
            List<object> list = new List<object>();
            Type arrType = null;

            do
            {
                object elem = CompileAddition();
                Type elemType = CompilerUtility.GetReturnType(elem);
                TypeDef elemTypeDef = _cdata._assembly.GetTypeDef(elemType, false);

                if (arrType == null) arrType = elemType;
                if (elemType == typeof(float) && arrType == typeof(int)) arrType = typeof(float);

                if (arrType != elemType)
                {
                    if (!(arrType == typeof(float) && elemType == typeof(int)) && !(arrType == typeof(object) || elemType == typeof(object)))
                    {
                        throw new Exception("All elements must be the same type: " + elemType.Name + " is not " + arrType.Name);
                    }
                }

                var elemFunc = CompilerUtility.ForceGetFunction(elem, _cdata);

                if (elemFunc != null)
                {
                    elem = elemFunc;
                }
                list.Add(elem);
            } while (!isAtEnd && MatchToken(Token.Type.Comma));

            TypeDef typeDef = _assembly.GetTypeDef(arrType, false);
            for (int i = 0; i < list.Count; i += 1)
            {
                list[i] = typeDef.Cast(list[i], _cdata);
            }

            Require(Token.Type.ClosingArrayIndex, "] Expected");

            return typeDef.CreateArrayLiteral(list.ToArray(), _cdata);
        }

        protected virtual object CompilePrimitive()
        {
            int line = UpdateLineNumber();
            if (MatchToken(Token.Type.True)) return true;
            if (MatchToken(Token.Type.False)) return false;
            if (MatchToken(Token.Type.Null)) return null;

            if (MatchToken(Token.Type.StringLiteral))
            {
                return previous.text;
            }

            if (MatchToken(Token.Type.HexLiteral))
            {
                return Convert.ToInt32(previous.text, 16);
            }

            if (MatchToken(Token.Type.Numeric))
            {
                // If float
                if (previous.text.IndexOf(".") > -1)
                {
                    return System.Single.Parse(previous.text);
                }
                // else int
                return System.Int32.Parse(previous.text);
            }

            if (MatchToken(Token.Type.OpenParenthesis))
            {
                object sub = CompileExpression();
                Require(Token.Type.ClosingParethesis, ") Expected");
                return sub;
            }

            if (MatchToken(Token.Type.ArrayIndexBegin))
            {
                return CompileArrayLiteral();
            }

            // If we got here, then this is not a declaration and we are trying to use the type as a variable
            if (MatchToken(Token.Type.TypeName))
            {
                TypeDef def = _cdata._assembly.GetTypeDef(previous.text);
                if (!def.allowStaticMembers) throw new CompilationException("Cannot access static definitions on type (" + def.name + ") allowStaticMembers =  false");
                else return (Func<object>)(() => { return def.type; });
            }

            throw CompilationException.Create("Token(" + Peek().text + ") at index " + _index + " cannot be parsed", line);
        }
    }
}