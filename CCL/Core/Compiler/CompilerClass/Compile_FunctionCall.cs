using System;
using System.Collections.Generic;

namespace CCL.Core
{
    public partial class Compiler
    {
        protected virtual object BuildFunctionCall(object callingFunction)
        {
            // For now, every function call is a runtime reference so we do not have to worry about the rest
            List<object> argList = new List<object>();
            if (Peek().type != Token.Type.ClosingParethesis)
            {
                do
                {
                    object expr = CompileExpression();
                    Func<object> func = null;
                    //Type returnType = CompilerUtility.GetReturnType(expr);
                    //TypeDef typeDef = _assembly.GetTypeDef(returnType);

                    func = CompilerUtility.ForceGetFunction(expr, _cdata);
                    if (func == null)
                    {
                        func = () => { return expr; };
                    }
                    argList.Add(func);
                } while (MatchToken(Token.Type.Comma));
            }
            if (Step().type != Token.Type.ClosingParethesis) throw new Exception(") expected after function call arguments");

            RunTimeReference callingReference = callingFunction as RunTimeReference;
            if (callingReference != null)
            {
                Func<object> targetFunc = CompilerUtility.ForceGetFunction(callingReference.targetObject, _cdata);
                if (targetFunc == null)
                {
                    return new MethodCall(callingReference.type, callingReference.identifer, callingReference.targetObject, argList.ToArray(), _cdata, callingReference.targetType);
                }
                else
                {
                    return new MethodCall(callingReference.type, callingReference.identifer, targetFunc, argList.ToArray(), _cdata, callingReference.targetType);
                }
            }
            CompileTimeReference compileTimeReference = callingFunction as CompileTimeReference;
            if (compileTimeReference != null)
            {
                return new FunctionCall(compileTimeReference.type, compileTimeReference.identifer, compileTimeReference.environmentIndex, argList.ToArray(), _cdata);
            }
            return callingFunction;
        }

        protected virtual object CompileFunctionCall()
        {
            object left = CompileIdentifier();
            object output = null;

            while (MatchToken(Token.Type.OpenParenthesis, Token.Type.Dot, Token.Type.IfDot, Token.Type.ArrayIndexBegin))
            {
                Token.Type op = previous.type;
                int line = UpdateLineNumber();
                if (output == null) output = left;
                Type leftType = CompilerUtility.GetReturnType(output);

                if (leftType == null) throw RuntimeException.Create("Cannot call " + previous.text + " on NULL", line);

                if (op == Token.Type.OpenParenthesis)
                {
                    output = BuildFunctionCall(output);
                }
                else if (op == Token.Type.Dot)
                {
                    Token identifier = Require(Token.Type.Identifier, "Identifier Expected");
                    Func<object> parentGetter = CompilerUtility.ForceGetFunction(output, _cdata);
                    if (leftType != typeof(object))
                    {
                        Type t = ReflectionUtility.GetMemberDataType(leftType, identifier.text);
                        output = new RunTimeReference(t, identifier.text, parentGetter, leftType);
                    }
                    else
                    {
                        output = new RunTimeReference(typeof(object), identifier.text, parentGetter, typeof(object));
                    }
                }
                else if (op == Token.Type.ArrayIndexBegin)
                {
                    output = CompileArrayAccess(output);
                }
            }

            return (output != null) ? (object)output : left;
        }
    }
}