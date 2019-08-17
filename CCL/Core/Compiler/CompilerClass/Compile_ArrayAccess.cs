using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCL.Core
{
    public partial class Compiler
    {
        protected virtual object CompileArrayAccess(object output)
        {
            // This is to handle the last identifier
            SetContextualIdentifierHandler(CompileLastKeyWord, output);

            object right = CompileArrowFunctionIndexer(output);

            ClearContextualIdentifierHandler();
            Require(Token.Type.ClosingArrayIndex, "] Expected");

            Func<object> rightFunc = CompilerUtility.ForceGetFunction(right, _cdata);
            if (rightFunc != null)
            {
                right = rightFunc;
            }

            Type type = CompilerUtility.GetReturnType(right);
            if (typeof(IList).IsAssignableFrom(type))
            {
                output = CreateListAccessorReference(output, right);
            }
            else
            {
                output = CreateIntAccessorReference(output, right);
            }

            return output;
        }

        protected virtual object CompileArrowFunctionIndexer(object left)
        {
            if (LookAhead(Token.Type.OpenParenthesis, Token.Type.Identifier, Token.Type.ClosingParethesis, Token.Type.ArrowOp))
            {
            }
            return CompileDimensionalIndexer();
        }

        protected virtual object CompileDimensionalIndexer()
        {
            object left = CompileExpression();
            object output = left;
            if (MatchToken(Token.Type.Colon))
            {
                Type leftType = CompilerUtility.GetReturnType(left);
                if (leftType != typeof(Int32) && leftType != typeof(object))
                {
                    throw new Exception("Function is not supported for type: " + leftType.Name);
                }
                List<object> integerList = new List<object>();

                object right = CompileExpression();
                Type rightType = CompilerUtility.GetReturnType(right);

                if (rightType != typeof(Int32) && rightType != typeof(object))
                {
                    throw new Exception("Function is not supported for type: " + rightType.Name);
                }

                Func<int> leftFunc = CompilerUtility.ForceGetFunction<int>(left, _cdata);
                if (leftFunc != null)
                {
                    left = leftFunc;
                }
                integerList.Add(left);

                Func<int> rightFunc = CompilerUtility.ForceGetFunction<int>(right, _cdata);
                if (rightFunc != null)
                {
                    right = rightFunc;
                }
                integerList.Add(right);
                while (MatchToken(Token.Type.Comma))
                {
                    left = CompileExpression();
                    leftFunc = CompilerUtility.ForceGetFunction<int>(left, _cdata);
                    if (leftFunc != null)
                    {
                        left = leftFunc;
                    }
                    integerList.Add(left);
                    if (MatchToken(Token.Type.Colon))
                    {
                        right = CompileExpression();
                        rightFunc = CompilerUtility.ForceGetFunction<int>(right, _cdata);
                        if (rightFunc != null)
                        {
                            right = rightFunc;
                        }
                        integerList.Add(right);
                    }
                }

                return (Func<int>)(() =>
                {
                    int total = 0;
                    int dimensionSize = 1;
                    int count = integerList.Count;
                    for (int i = 0; i < count; i += 2)
                    {
                        Func<int> lFunc = integerList[i] as Func<int>;
                        if (lFunc != null)
                        {
                            total += lFunc() * dimensionSize;
                        }
                        else
                        {
                            Debug.Log(integerList[i]);
                            total += (int)integerList[i] * dimensionSize;
                        }
                        if (i + 1 < count)
                        {
                            Func<int> rFunc = integerList[i + 1] as Func<int>;
                            if (rFunc != null)
                            {
                                dimensionSize *= rFunc();
                            }
                            else
                            {
                                dimensionSize *= (int)integerList[i + 1];
                            }
                        }
                    }
                    return total;
                });
            }

            return output;
        }

        protected virtual object CompileLastKeyWord(object left)
        {
            if (LookAhead(Token.Type.Dot, Token.Type.Identifier))
            {
                if (_tokens[_index + 1].text == "last")
                {
                    Step(); Step();
                }
                Func<object> arrayFunc = CompilerUtility.ForceGetFunction(left, _cdata);

                if (arrayFunc == null)
                {
                    return (Func<int>)(() =>
                    {
                        return ((IList)left).Count - 1;
                    });
                }
                else
                {
                    return (Func<int>)(() =>
                    {
                        return ((IList)arrayFunc()).Count - 1;
                    });
                }
            }

            return null;
        }

        protected virtual object CreateIntAccessorReference(object output, object accessor)
        {
            if (output as RunTimeReference != null)
            {
                RunTimeReference reference = output as RunTimeReference;
                object target = reference.CreateGetFunction(_cdata);

                if (reference.type == null || reference.type == typeof(object))
                {
                    output = new RunTimeArrayAccessReference(typeof(object), reference.identifer, target, accessor, typeof(object));
                }
                else
                {
                    output = new RunTimeArrayAccessReference(ArrayUtility.GetElementType(reference.type), reference.identifer, target, accessor, reference.targetType);
                }
            }
            else if (output as FunctionCall != null || output as MethodCall != null || output as CompileTimeArrayAccessReference != null)
            {
                Reference reference = output as Reference;
                object target = reference.CreateGetFunction(_cdata);
                if (reference.type == null || reference.type == typeof(object))
                {
                    output = new RunTimeArrayAccessReference(typeof(object), reference.identifer, target, accessor, typeof(object));
                }
                else
                {
                    output = new RunTimeArrayAccessReference(ArrayUtility.GetElementType(reference.type), reference.identifer, target, accessor, reference.type);
                }
            }
            else if (output as CompileTimeReference != null)
            {
                CompileTimeReference reference = output as CompileTimeReference;

                if (reference.type == null || reference.type == typeof(object))
                {
                    output = new CompileTimeArrayAccessReference(typeof(object), reference.identifer, reference.environmentIndex, accessor);
                }
                else
                {
                    output = new CompileTimeArrayAccessReference(ArrayUtility.GetElementType(reference.type), reference.identifer, reference.environmentIndex, accessor);
                }
            }
            return output;
        }

        protected virtual object CreateListAccessorReference(object output, object accessor)
        {
            if (output as RunTimeReference != null)
            {
                RunTimeReference reference = output as RunTimeReference;
                object target = reference.CreateGetFunction(_cdata);

                if (reference.type == null || reference.type == typeof(object))
                {
                    output = new RunTimeArrayAccessReferenceListAccess(typeof(object), reference.identifer, target, accessor, typeof(object));
                }
                else
                {
                    output = new RunTimeArrayAccessReferenceListAccess(reference.type, reference.identifer, target, accessor, reference.targetType);
                }
            }
            else if (output as FunctionCall != null || output as MethodCall != null || output as CompileTimeArrayAccessReference != null)
            {
                Reference reference = output as Reference;
                object target = reference.CreateGetFunction(_cdata);
                if (reference.type == null || reference.type == typeof(object))
                {
                    output = new RunTimeArrayAccessReferenceListAccess(typeof(object), reference.identifer, target, accessor, typeof(object));
                }
                else
                {
                    output = new RunTimeArrayAccessReferenceListAccess(reference.type, reference.identifer, target, accessor, reference.type);
                }
            }
            else if (output as CompileTimeReference != null)
            {
                CompileTimeReference reference = output as CompileTimeReference;

                if (reference.type == null || reference.type == typeof(object))
                {
                    output = new CompileTimeArrayAccessReferenceListAccess(typeof(object), reference.identifer, reference.environmentIndex, accessor);
                }
                else
                {
                    output = new CompileTimeArrayAccessReferenceListAccess(reference.type, reference.identifer, reference.environmentIndex, accessor);
                }
            }
            return output;
        }
    }
}