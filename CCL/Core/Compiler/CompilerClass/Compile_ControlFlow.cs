using System;
using System.Collections.Generic;

namespace CCL.Core
{
    public partial class Compiler
    {
        protected virtual object CompileBlockStatement()
        {
            List<Func<object>> functions = new List<Func<object>>();
            _cdata._scopeResolver.Push();
            while (!isAtEnd && Peek().type != Token.Type.CloseBlock)
            {
                object compiled = CompileDeclaration();

                Func<object> function = CompilerUtility.ForceGetFunction(compiled, _cdata);
                if (function == null)
                {
                    function = () => { return compiled; };
                }
                functions.Add(function);
            }
            Func<object>[] funcArray = functions.ToArray();

            Func<object> block = () =>
            {
                object output = null;
                for (int mn = 0; mn < funcArray.Length; mn += 1)
                {
                    output = funcArray[mn]();
                    if ((_cdata._runtimeFlags & (int)CompilerData.ControlInstruction.Break) != 0) break;
                }
                return output;
            };

            Require(Token.Type.CloseBlock, "Closing } Expected");
            _cdata._scopeResolver.Pop();
            return block;
        }

        protected virtual object CompileControlInstructionStatement()
        {
            if (MatchToken(Token.Type.Break))
            {
                Func<object> output = _cdata._break;
                Require(Token.Type.EOS, "; Expected");
                return output;
            }
            else if (MatchToken(Token.Type.Continue))
            {
                Func<object> output = _cdata._continue;
                Require(Token.Type.EOS, "; Expected");
                return output;
            }
            else
            {
                return CompileControlStatement();
            }
        }

        protected virtual object CompileControlStatement()
        {
            if (MatchToken(Token.Type.OpenBlock)) return CompileBlockStatement();
            else if (MatchToken(Token.Type.If)) return CompileIfStatement();
            else if (MatchToken(Token.Type.While)) return CompileWhileStatement();
            else if (MatchToken(Token.Type.For)) return CompileForStatement();
            else if (MatchToken(Token.Type.Return)) return CompileReturnStatement();
            else if (MatchToken(Token.Type.Require)) return CompileRequireStatement();
            else if (MatchToken(Token.Type.Help)) return CompileHelpStatement();

            return CompileExpressionStatement(); ;
        }

        protected virtual object CompileForStatement()
        {
            _cdata._scopeResolver.Push();

            Require(Token.Type.OpenParenthesis, "( Expected");
            object initializer = CompileDeclaration();
            object condition = CompileAssignment();
            Require(Token.Type.EOS, "; Expected");
            object incrementor = CompileExpression();
            Require(Token.Type.ClosingParethesis, ") Expected");
            Require(Token.Type.OpenBlock, "{ Expected");

            Func<object> initFunction = CompilerUtility.ForceGetFunction(initializer, _cdata);
            Func<bool> conditionFunction = CompilerUtility.ForceGetFunction<bool>(condition, _cdata);
            Func<object> incrementorFunction = CompilerUtility.ForceGetFunction(incrementor, _cdata);

            if (initFunction == null && initializer != null)
            {
                initFunction = () => { return initializer; };
            }
            if (conditionFunction == null)
            {
                if (condition != null)
                {
                    conditionFunction = () => { return (bool)condition; };
                }
                else
                {
                    conditionFunction = () => { return true; };
                }
            }
            if (incrementorFunction == null && incrementor != null)
            {
                incrementorFunction = () => { return incrementor; };
            }

            if (CompilerUtility.GetReturnType(conditionFunction) != typeof(bool)) throw new Exception("for loop condition must be a bool");

            List<Func<object>> functions = new List<Func<object>>();

            while (!isAtEnd && Peek().type != Token.Type.CloseBlock)
            {
                object compiled = CompileDeclaration();
                Func<object> function = CompilerUtility.ForceGetFunction(compiled, _cdata);
                if (function == null && compiled != null)
                {
                    function = () => { return compiled; };
                }
                functions.Add(function);
            }

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
                        _cdata._runtimeFlags = 0;
                        break;
                    }
                    if ((_cdata._runtimeFlags & (int)CompilerData.ControlInstruction.Exit) != 0) return null;
                }
                return output;
            };

            Func<object> forFunc = () =>
            {
                for (initFunction(); conditionFunction(); incrementorFunction())
                {
                    block();
                    if ((_cdata._runtimeFlags & (int)CompilerData.ControlInstruction.Break) != 0)
                    {
                        _cdata._runtimeFlags = 0;
                        break;
                    }
                    if ((_cdata._runtimeFlags & (int)CompilerData.ControlInstruction.Exit) != 0) return null;
                }
                return null;
            };

            Require(Token.Type.CloseBlock, "} Expected");

            _cdata._scopeResolver.Pop();
            return forFunc;
        }

        protected virtual object CompileIfStatement()
        {
            Require(Token.Type.OpenParenthesis, "Parenthesis Required");
            object condition = CompileAssignment();
            Require(Token.Type.ClosingParethesis, ") Expected");
            object statement = CompileDeclaration();

            Func<bool> conditionFunc = CompilerUtility.ForceGetFunction<bool>(condition, _cdata);
            if (conditionFunc == null)
            {
                conditionFunc = () => { return (bool)condition; };
            }

            Func<object> statementFunc = CompilerUtility.ForceGetFunction(statement, _cdata);
            if (statementFunc == null)
            {
                statementFunc = () => { return statement; };
            }

            Func<object> elseFunc = null;
            Func<object> ifFunc = null;
            if (MatchToken(Token.Type.Else))
            {
                if (MatchToken(Token.Type.If))
                {
                    object elseData = CompileIfStatement();
                    if (elseData.GetType() == typeof(Func<object>))
                    {
                        elseFunc = (Func<object>)elseData;
                    }
                }
                else
                {
                    object elseData = CompileDeclaration();
                    if (elseData.GetType() == typeof(Func<object>))
                    {
                        elseFunc = (Func<object>)elseData;
                    }
                }

                ifFunc = () =>
                {
                    if (conditionFunc())
                    {
                        statementFunc();
                    }
                    else
                    {
                        elseFunc();
                    }
                    return null;
                };
            }
            else
            {
                ifFunc = () =>
                {
                    if ((bool)conditionFunc())
                    {
                        statementFunc();
                    }
                    return null;
                };
            }

            return ifFunc;
        }

        protected virtual object CompileReturnStatement()
        {
            if (isAtEnd || Peek().type == Token.Type.EOS)
            {
                return (Func<object>)(() =>
                {
                    _cdata._exit(null);
                    return null;
                });
            }

            Func<object> func = null;
            object statement = CompileExpressionStatement();

            if (statement == null)
            {
                return (Func<object>)(() =>
                {
                    return null;
                });
            }

            Type returnType = CompilerUtility.GetReturnType(statement);

            TypeDef typedef = _assembly.GetTypeDef(returnType);

            if (CompilerUtility.IsFunc(statement))
            {
                if (returnType != typeof(object)) func = typedef.ToGenericFunction(statement, _cdata);
                else func = (Func<object>)statement;

                return (Func<object>)(() =>
                {
                    object val = func();
                    _cdata._exit(val);
                    return val;
                });
            }
            else if (CompilerUtility.IsReference(statement))
            {
                func = ((Reference)statement).CreateGetFunction(_cdata);

                return (Func<object>)(() =>
                {
                    object val = func();
                    _cdata._exit(val);
                    return val;
                });
            }
            else
            {
                return (Func<object>)(() =>
                {
                    _cdata._exit(statement);
                    return statement;
                });
            }
        }

        protected virtual object CompileWhileStatement()
        {
            Require(Token.Type.OpenParenthesis, "Parenthesis Required");
            object condition = CompileExpression();
            Require(Token.Type.ClosingParethesis, ") Expected");
            object statement = CompileDeclaration();
            Func<object> whileFunc = null;
            Func<object> blockFunc = (Func<object>)statement;
            Func<bool> conditionFunc = CompilerUtility.ForceGetFunction<bool>(condition, _cdata);

            if (conditionFunc != null)
            {
                whileFunc = () =>
                {
                    while (conditionFunc())
                    {
                        blockFunc();
                        if ((_cdata._runtimeFlags & (int)CompilerData.ControlInstruction.Break) != 0)
                        {
                            _cdata._runtimeFlags = 0;
                            break;
                        }
                        if ((_cdata._runtimeFlags & (int)CompilerData.ControlInstruction.Exit) != 0) return null;
                    }
                    return null;
                };
            }
            else
            {
                whileFunc = () =>
                {
                    while ((bool)condition)
                    {
                        blockFunc();
                        if ((_cdata._runtimeFlags & (int)CompilerData.ControlInstruction.Break) != 0)
                        {
                            _cdata._runtimeFlags = 0;
                            break;
                        }
                        if ((_cdata._runtimeFlags & (int)CompilerData.ControlInstruction.Exit) != 0) return null;
                    }
                    return null;
                };
            }

            return whileFunc;
        }
    }
}