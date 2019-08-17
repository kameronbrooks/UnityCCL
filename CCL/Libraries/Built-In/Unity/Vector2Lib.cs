using CCL.Core;
using System;
using UnityEngine;

namespace CCL.Libraries
{
    public static class Vector2Lib
    {
        [BinaryOperator(typeof(Vector2), Token.Type.Addition, typeof(Vector2))]
        public static object Add_Vector2(object arg0, object arg1, CompilerData cdata)
        {
            Func<Vector2> arg0Func = CompilerUtility.ForceGetFunction<Vector2>(arg0, cdata);
            Func<Vector2> arg1Func = CompilerUtility.ForceGetFunction<Vector2>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                Vector2 temp0 = (Vector2)arg0;
                Vector2 temp1 = (Vector2)arg1;
                return (Func<Vector2>)(() =>
                {
                    return temp0 + temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                Vector2 temp0 = (Vector2)arg0;
                return (Func<Vector2>)(() =>
                {
                    return temp0 + arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                Vector2 temp1 = (Vector2)arg1;
                return (Func<Vector2>)(() =>
                {
                    return arg0Func() + temp1;
                });
            }
            else
            {
                return (Func<Vector2>)(() =>
                {
                    return arg0Func() + arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Vector2), Token.Type.Assign, typeof(Single[]))]
        public static object Assign_FloatArr(object arg0, object arg1, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            Func<Single[]> arg1Func = CompilerUtility.ForceGetFunction<Single[]>(arg1, cdata);

            if (arg1Func != null)
            {
                return ((Reference)arg0).CreateSetFunction<Vector2>((Func<Vector2>)(() =>
                {
                    float[] arr = arg1Func();
                    return new Vector2(arr[0], arr[1]);
                }), cdata);
            }
            else
            {
                float[] temp = (float[])arg1;
                return ((Reference)arg0).CreateSetFunction<Vector2>((Func<Vector2>)(() =>
                {
                    return new Vector2(temp[0], temp[1]);
                }), cdata);
            }
        }

        [BinaryOperator(typeof(Vector2), Token.Type.Assign, typeof(Int32[]))]
        public static object Assign_IntArr(object arg0, object arg1, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            Func<int[]> arg1Func = CompilerUtility.ForceGetFunction<int[]>(arg1, cdata);

            if (arg1Func != null)
            {
                return ((Reference)arg0).CreateSetFunction<Vector2>((Func<Vector2>)(() =>
                {
                    int[] arr = arg1Func();
                    return new Vector2(arr[0], arr[1]);
                }), cdata);
            }
            else
            {
                int[] temp = (int[])arg1;
                return ((Reference)arg0).CreateSetFunction<Vector2>((Func<Vector2>)(() =>
                {
                    return new Vector2(temp[0], temp[1]);
                }), cdata);
            }
        }

        [BinaryOperator(typeof(Vector2), Token.Type.Division, typeof(Single))]
        public static object Divide_Single(object arg0, object arg1, CompilerData cdata)
        {
            Func<Vector2> arg0Func = CompilerUtility.ForceGetFunction<Vector2>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                Vector2 temp0 = (Vector2)arg0;
                float temp1 = (float)arg1;
                return (Func<Vector2>)(() =>
                {
                    return temp0 / temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                Vector2 temp0 = (Vector2)arg0;
                return (Func<Vector2>)(() =>
                {
                    return temp0 / arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float temp1 = (float)arg1;
                return (Func<Vector2>)(() =>
                {
                    return arg0Func() / temp1;
                });
            }
            else
            {
                return (Func<Vector2>)(() =>
                {
                    return arg0Func() / arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Vector2), Token.Type.Division, typeof(Vector2))]
        public static object Divide_Vector2(object arg0, object arg1, CompilerData cdata)
        {
            Func<Vector2> arg0Func = CompilerUtility.ForceGetFunction<Vector2>(arg0, cdata);
            Func<Vector2> arg1Func = CompilerUtility.ForceGetFunction<Vector2>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                Vector2 temp0 = (Vector2)arg0;
                Vector2 temp1 = (Vector2)arg1;
                return (Func<Vector2>)(() =>
                {
                    return temp0 / temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                Vector2 temp0 = (Vector2)arg0;
                return (Func<Vector2>)(() =>
                {
                    return temp0 / arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                Vector2 temp1 = (Vector2)arg1;
                return (Func<Vector2>)(() =>
                {
                    return arg0Func() / temp1;
                });
            }
            else
            {
                return (Func<Vector2>)(() =>
                {
                    return arg0Func() / arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Vector2), Token.Type.Multiplication, typeof(Single))]
        public static object Multiply_Float(object arg0, object arg1, CompilerData cdata)
        {
            Func<Vector2> arg0Func = CompilerUtility.ForceGetFunction<Vector2>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                Vector2 temp0 = (Vector2)arg0;
                float temp1 = (float)arg1;
                return (Func<Vector2>)(() =>
                {
                    return temp0 * temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                Vector2 temp0 = (Vector2)arg0;
                return (Func<Vector2>)(() =>
                {
                    return temp0 * arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float temp1 = (float)arg1;
                return (Func<Vector2>)(() =>
                {
                    return arg0Func() * temp1;
                });
            }
            else
            {
                return (Func<Vector2>)(() =>
                {
                    return arg0Func() * arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Vector2), Token.Type.Multiplication, typeof(Vector2))]
        public static object Multiply_Vector2(object arg0, object arg1, CompilerData cdata)
        {
            Func<Vector2> arg0Func = CompilerUtility.ForceGetFunction<Vector2>(arg0, cdata);
            Func<Vector2> arg1Func = CompilerUtility.ForceGetFunction<Vector2>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                Vector2 temp0 = (Vector2)arg0;
                Vector2 temp1 = (Vector2)arg1;
                return (Func<Vector2>)(() =>
                {
                    return temp0 * temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                Vector2 temp0 = (Vector2)arg0;
                return (Func<Vector2>)(() =>
                {
                    return temp0 * arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                Vector2 temp1 = (Vector2)arg1;
                return (Func<Vector2>)(() =>
                {
                    return arg0Func() * temp1;
                });
            }
            else
            {
                return (Func<Vector2>)(() =>
                {
                    return arg0Func() * arg1Func();
                });
            }
        }

        [UnaryOperator(typeof(Vector2), Token.Type.Subtraction)]
        public static object Negate(object arg0, CompilerData cdata)
        {
            Func<Vector2> arg0Func = CompilerUtility.ForceGetFunction<Vector2>(arg0, cdata);
            if (arg0Func == null)
            {
                Vector2 temp = (Vector2)arg0;
                return (Func<Vector2>)(() =>
                {
                    return -temp;
                });
            }
            else
            {
                return (Func<Vector2>)(() =>
                {
                    return -arg0Func();
                });
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.Multiplication, typeof(Vector2))]
        public static object Single_Multiply(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<Vector2> arg1Func = CompilerUtility.ForceGetFunction<Vector2>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                Vector2 temp1 = (Vector2)arg1;
                return (Func<Vector2>)(() =>
                {
                    return temp0 * temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<Vector2>)(() =>
                {
                    return temp0 * arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                Vector2 temp1 = (Vector2)arg1;
                return (Func<Vector2>)(() =>
                {
                    return arg0Func() * temp1;
                });
            }
            else
            {
                return (Func<Vector2>)(() =>
                {
                    return arg0Func() * arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Vector2), Token.Type.Subtraction, typeof(Vector2))]
        public static object Subtract_Vector2(object arg0, object arg1, CompilerData cdata)
        {
            Func<Vector2> arg0Func = CompilerUtility.ForceGetFunction<Vector2>(arg0, cdata);
            Func<Vector2> arg1Func = CompilerUtility.ForceGetFunction<Vector2>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                Vector2 temp0 = (Vector2)arg0;
                Vector2 temp1 = (Vector2)arg1;
                return (Func<Vector2>)(() =>
                {
                    return temp0 - temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                Vector2 temp0 = (Vector2)arg0;
                return (Func<Vector2>)(() =>
                {
                    return temp0 - arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                Vector2 temp1 = (Vector2)arg1;
                return (Func<Vector2>)(() =>
                {
                    return arg0Func() - temp1;
                });
            }
            else
            {
                return (Func<Vector2>)(() =>
                {
                    return arg0Func() - arg1Func();
                });
            }
        }

        public class Vector2Type : TypeDef
        {
            public override string name
            {
                get
                {
                    return "Vector2";
                }
            }

            public override Type type
            {
                get
                {
                    return typeof(Vector2);
                }
            }

            public override object Cast(object arg, CompilerData _internal)
            {
                Type argType = CompilerUtility.GetReturnType(arg);
                if (CompilerUtility.IsFunc(arg) || CompilerUtility.IsReference(arg))
                {
                    if (argType == typeof(Single[]))
                    {
                        Func<float[]> func = CompilerUtility.ForceGetFunction<float[]>(arg, _internal);
                        return (Func<Vector2>)(() =>
                        {
                            float[] arr = func();
                            return new Vector2(arr[0], arr[1]);
                        });
                    }
                    else if (argType == typeof(Int32[]))
                    {
                        Func<int[]> func = CompilerUtility.ForceGetFunction<int[]>(arg, _internal);
                        return (Func<Vector2>)(() =>
                        {
                            int[] arr = func();
                            return new Vector2(arr[0], arr[1]);
                        });
                    }
                    else if (argType == typeof(Vector2))
                    {
                        Func<Vector2> func = CompilerUtility.ForceGetFunction<Vector2>(arg, _internal);
                        return func;
                    }
                    else if (argType == typeof(object))
                    {
                        Func<object> func = CompilerUtility.ForceGetFunction(arg, _internal);
                        return (Func<Vector2>)(() => { return (Vector2)func(); });
                    }
                }
                else
                {
                    if (argType == typeof(Single[]))
                    {
                        float[] temp = (float[])arg;
                        return (Func<Vector2>)(() =>
                        {
                            return new Vector2(temp[0], temp[1]);
                        });
                    }
                    else if (argType == typeof(Int32[]))
                    {
                        int[] temp = (int[])arg;
                        return (Func<Vector2>)(() =>
                        {
                            return new Vector2(temp[0], temp[1]);
                        });
                    }
                    else if (argType == typeof(Vector2))
                    {
                        Vector2 temp = (Vector2)arg;
                        return (Func<Vector2>)(() => { return temp; });
                    }
                    else if (argType == typeof(object))
                    {
                        object temp = arg;
                        return (Func<Vector2>)(() => { return (Vector2)temp; });
                    }
                }

                throw _internal.CreateException("No supported cast for type vector2 and " + argType.Name);
            }

            public override object CastArray(object arg, CompilerData _internal)
            {
                Type argType = CompilerUtility.GetReturnType(arg);

                if (CompilerUtility.IsFunc(arg) || CompilerUtility.IsReference(arg))
                {
                    if (argType == typeof(object))
                    {
                        Func<object> func = CompilerUtility.ForceGetFunction(arg, _internal);
                        return (Func<Vector2[]>)(() => { return (Vector2[])func(); });
                    }
                }
                else
                {
                    if (argType == typeof(object))
                    {
                        object temp = arg;
                        return (Func<Vector2[]>)(() => { return (Vector2[])temp; });
                    }
                }
                throw _internal.CreateException("No supported cast for type Vector2[] and " + argType.Name);
            }

            public override object CreateArrayLiteral(object[] elements, CompilerData cdata)
            {
                return CreateArrayLiteral<Vector2>(elements, cdata);
            }

            public override Func<object> DefaultConstructor()
            {
                return () => { return new Vector2(); };
            }

            public override Func<object> ToGenericFunction(object arg, CompilerData _internal)
            {
                if (arg.GetType() == typeof(Func<Vector2>))
                {
                    return () =>
                    {
                        return ((Func<Vector2>)(arg))();
                    };
                }
                else if (arg.GetType() == typeof(Func<Vector2[]>))
                {
                    return () =>
                    {
                        return ((Func<Vector2[]>)(arg))();
                    };
                }
                return () =>
                {
                    return arg;
                };
            }
        }
    }
}