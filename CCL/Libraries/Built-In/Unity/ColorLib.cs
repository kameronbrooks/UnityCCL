using CCL.Core;
using System;
using UnityEngine;

namespace CCL.Libraries
{
    public static class ColorLib
    {
        [BinaryOperator(typeof(Color), Token.Type.Addition, typeof(Color))]
        public static object Add_Color(object arg0, object arg1, CompilerData cdata)
        {
            Func<Color> arg0Func = CompilerUtility.ForceGetFunction<Color>(arg0, cdata);
            Func<Color> arg1Func = CompilerUtility.ForceGetFunction<Color>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                Color temp0 = (Color)arg0;
                Color temp1 = (Color)arg1;
                return (Func<Color>)(() =>
                {
                    return temp0 + temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                Color temp0 = (Color)arg0;
                return (Func<Color>)(() =>
                {
                    return temp0 + arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                Color temp1 = (Color)arg1;
                return (Func<Color>)(() =>
                {
                    return arg0Func() + temp1;
                });
            }
            else
            {
                return (Func<Color>)(() =>
                {
                    return arg0Func() + arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Color), Token.Type.Assign, typeof(Single[]))]
        public static object Assign_FloatArr(object arg0, object arg1, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            Func<Single[]> arg1Func = CompilerUtility.ForceGetFunction<Single[]>(arg1, cdata);

            if (arg1Func != null)
            {
                return ((Reference)arg0).CreateSetFunction<Color>((Func<Color>)(() =>
                {
                    float[] arr = arg1Func();
                    if (arr.Length > 3)
                    {
                        return new Color(arr[0], arr[1], arr[2], arr[3]);
                    }
                    else if (arr.Length > 2)
                    {
                        return new Color(arr[0], arr[1], arr[2], 1);
                    }
                    throw new RuntimeException("Cannot cast array to Color with length less than 3");
                }), cdata);
            }
            else
            {
                float[] temp = (float[])arg1;
                return ((Reference)arg0).CreateSetFunction<Color>((Func<Color>)(() =>
                {
                    if (temp.Length > 3)
                    {
                        return new Color(temp[0], temp[1], temp[2], temp[3]);
                    }
                    else if (temp.Length > 2)
                    {
                        return new Color(temp[0], temp[1], temp[2], 1);
                    }
                    throw new RuntimeException("Cannot cast array to Color with length less than 3");
                }), cdata);
            }
        }

        [BinaryOperator(typeof(Color), Token.Type.Assign, typeof(Int32[]))]
        public static object Assign_IntArr(object arg0, object arg1, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            Func<int[]> arg1Func = CompilerUtility.ForceGetFunction<int[]>(arg1, cdata);

            if (arg1Func != null)
            {
                return ((Reference)arg0).CreateSetFunction<Color>((Func<Color>)(() =>
                {
                    int[] arr = arg1Func();
                    if (arr.Length > 3)
                    {
                        return new Color(arr[0], arr[1], arr[2], arr[3]);
                    }
                    else if (arr.Length > 2)
                    {
                        return new Color(arr[0], arr[1], arr[2], 1);
                    }
                    throw new RuntimeException("Cannot cast array to Color with length less than 3");
                }), cdata);
            }
            else
            {
                int[] temp = (int[])arg1;
                return ((Reference)arg0).CreateSetFunction<Color>((Func<Color>)(() =>
                {
                    if (temp.Length > 3)
                    {
                        return new Color(temp[0], temp[1], temp[2], temp[3]);
                    }
                    else if (temp.Length > 2)
                    {
                        return new Color(temp[0], temp[1], temp[2], 1);
                    }
                    throw new RuntimeException("Cannot cast array to Color with length less than 3");
                }), cdata);
            }
        }

        [BinaryOperator(typeof(Color), Token.Type.Division, typeof(Single))]
        public static object Divide_Single(object arg0, object arg1, CompilerData cdata)
        {
            Func<Color> arg0Func = CompilerUtility.ForceGetFunction<Color>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                Color temp0 = (Color)arg0;
                float temp1 = (float)arg1;
                return (Func<Color>)(() =>
                {
                    return temp0 / temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                Color temp0 = (Color)arg0;
                return (Func<Color>)(() =>
                {
                    return temp0 / arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float temp1 = (float)arg1;
                return (Func<Color>)(() =>
                {
                    return arg0Func() / temp1;
                });
            }
            else
            {
                return (Func<Color>)(() =>
                {
                    return arg0Func() / arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Color), Token.Type.Multiplication, typeof(Single))]
        public static object Multiply_Float(object arg0, object arg1, CompilerData cdata)
        {
            Func<Color> arg0Func = CompilerUtility.ForceGetFunction<Color>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                Color temp0 = (Color)arg0;
                float temp1 = (float)arg1;
                return (Func<Color>)(() =>
                {
                    return temp0 * temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                Color temp0 = (Color)arg0;
                return (Func<Color>)(() =>
                {
                    return temp0 * arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float temp1 = (float)arg1;
                return (Func<Color>)(() =>
                {
                    return arg0Func() * temp1;
                });
            }
            else
            {
                return (Func<Color>)(() =>
                {
                    return arg0Func() * arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Single), Token.Type.Multiplication, typeof(Color))]
        public static object Single_Multiply(object arg0, object arg1, CompilerData cdata)
        {
            Func<float> arg0Func = CompilerUtility.ForceGetFunction<float>(arg0, cdata);
            Func<Color> arg1Func = CompilerUtility.ForceGetFunction<Color>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                float temp0 = (float)arg0;
                Color temp1 = (Color)arg1;
                return (Func<Color>)(() =>
                {
                    return temp0 * temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                float temp0 = (float)arg0;
                return (Func<Color>)(() =>
                {
                    return temp0 * arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                Color temp1 = (Color)arg1;
                return (Func<Color>)(() =>
                {
                    return arg0Func() * temp1;
                });
            }
            else
            {
                return (Func<Color>)(() =>
                {
                    return arg0Func() * arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Color), Token.Type.Subtraction, typeof(Color))]
        public static object Subtract_Color(object arg0, object arg1, CompilerData cdata)
        {
            Func<Color> arg0Func = CompilerUtility.ForceGetFunction<Color>(arg0, cdata);
            Func<Color> arg1Func = CompilerUtility.ForceGetFunction<Color>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                Color temp0 = (Color)arg0;
                Color temp1 = (Color)arg1;
                return (Func<Color>)(() =>
                {
                    return temp0 - temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                Color temp0 = (Color)arg0;
                return (Func<Color>)(() =>
                {
                    return temp0 - arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                Color temp1 = (Color)arg1;
                return (Func<Color>)(() =>
                {
                    return arg0Func() - temp1;
                });
            }
            else
            {
                return (Func<Color>)(() =>
                {
                    return arg0Func() - arg1Func();
                });
            }
        }

        public class ColorType : TypeDef
        {
            public override string name
            {
                get
                {
                    return "Color";
                }
            }

            public override Type type
            {
                get
                {
                    return typeof(Color);
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
                        return (Func<Color>)(() =>
                        {
                            float[] arr = func();
                            if (arr.Length > 3)
                            {
                                return new Color(arr[0], arr[1], arr[2], arr[3]);
                            }
                            else if (arr.Length > 2)
                            {
                                return new Color(arr[0], arr[1], arr[2], 1);
                            }
                            throw new RuntimeException("Cannot cast array to Color with length less than 3");
                        });
                    }
                    else if (argType == typeof(Int32[]))
                    {
                        Func<int[]> func = CompilerUtility.ForceGetFunction<int[]>(arg, _internal);
                        return (Func<Color>)(() =>
                        {
                            int[] arr = func();
                            if (arr.Length > 3)
                            {
                                return new Color(arr[0], arr[1], arr[2], arr[3]);
                            }
                            else if (arr.Length > 2)
                            {
                                return new Color(arr[0], arr[1], arr[2], 1);
                            }
                            throw new RuntimeException("Cannot cast array to Color with length less than 3");
                        });
                    }
                    else if (argType == typeof(Vector4))
                    {
                        Func<Vector4> func = CompilerUtility.ForceGetFunction<Vector4>(arg, _internal);
                        return (Func<Color>)(() =>
                        {
                            Vector4 arr = func();
                            return new Color(arr[0], arr[1], arr[2], arr[3]);
                        });
                    }
                    else if (argType == typeof(Vector3))
                    {
                        Func<Vector3> func = CompilerUtility.ForceGetFunction<Vector3>(arg, _internal);
                        return (Func<Color>)(() =>
                        {
                            Vector3 arr = func();
                            return new Color(arr[0], arr[1], arr[2], 1);
                        });
                    }
                    else if (argType == typeof(object))
                    {
                        Func<object> func = CompilerUtility.ForceGetFunction(arg, _internal);
                        return (Func<Color>)(() =>
                        {
                            return (Color)func();
                        });
                    }
                }
                else
                {
                    if (argType == typeof(Single[]))
                    {
                        float[] temp = (float[])arg;
                        return (Func<Color>)(() =>
                        {
                            if (temp.Length > 3)
                            {
                                return new Color(temp[0], temp[1], temp[2], temp[3]);
                            }
                            else if (temp.Length > 2)
                            {
                                return new Color(temp[0], temp[1], temp[2], 1);
                            }
                            throw new RuntimeException("Cannot cast array to Color with length less than 3");
                        });
                    }
                    else if (argType == typeof(Int32[]))
                    {
                        int[] temp = (int[])arg;
                        return (Func<Color>)(() =>
                        {
                            if (temp.Length > 3)
                            {
                                return new Color(temp[0], temp[1], temp[2], temp[3]);
                            }
                            else if (temp.Length > 2)
                            {
                                return new Color(temp[0], temp[1], temp[2], 1);
                            }
                            throw new RuntimeException("Cannot cast array to Color with length less than 3");
                        });
                    }
                    else if (argType == typeof(Vector4))
                    {
                        Vector4 temp = (Vector4)arg;
                        return (Func<Color>)(() =>
                        {
                            return new Color(temp[0], temp[1], temp[2], temp[3]);
                        });
                    }
                    else if (argType == typeof(Vector3))
                    {
                        Vector4 temp = (Vector4)arg;
                        return (Func<Color>)(() =>
                        {
                            return new Color(temp[0], temp[1], temp[2], 1);
                        });
                    }
                    else if (argType == typeof(object))
                    {
                        object temp = arg;
                        return (Func<Color>)(() =>
                        {
                            return (Color)temp;
                        });
                    }
                }

                throw _internal.CreateException("No supported cast for type Color and " + argType.Name);
            }

            public override object CastArray(object arg, CompilerData _internal)
            {
                Type argType = CompilerUtility.GetReturnType(arg);

                if (CompilerUtility.IsFunc(arg) || CompilerUtility.IsReference(arg))
                {
                    if (argType == typeof(object))
                    {
                        Func<object> func = CompilerUtility.ForceGetFunction(arg, _internal);
                        return (Func<Color[]>)(() => { return (Color[])func(); });
                    }
                }
                else
                {
                    if (argType == typeof(object))
                    {
                        object temp = arg;
                        return (Func<Color[]>)(() => { return (Color[])temp; });
                    }
                }
                throw _internal.CreateException("No supported cast for type Vector3[] and " + argType.Name);
            }

            public override object CreateArrayLiteral(object[] elements, CompilerData cdata)
            {
                return CreateArrayLiteral<Color>(elements, cdata);
            }

            public override Func<object> DefaultConstructor()
            {
                return () => { return new Color(); };
            }

            public override Func<object> ToGenericFunction(object arg, CompilerData _internal)
            {
                if (arg.GetType() == typeof(Func<Color>))
                {
                    return () =>
                    {
                        return ((Func<Color>)(arg))();
                    };
                }
                else if (arg.GetType() == typeof(Func<Color[]>))
                {
                    return () =>
                    {
                        return ((Func<Color[]>)(arg))();
                    };
                }
                return () =>
                {
                    return arg;
                };
            }
        }

        /*
        [BinaryOperator(typeof(Color), Token.Type.Multiplication, typeof(Color))]
        public static object Multiply_Color(object arg0, object arg1, CompilerData cdata)
        {
            Func<Color> arg0Func = CompilerUtility.ForceGetFunction<Color>(arg0, cdata);
            Func<Color> arg1Func = CompilerUtility.ForceGetFunction<Color>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                Color temp0 = (Color)arg0;
                Color temp1 = (Color)arg1;
                return (Func<Color>)(() =>
                {
                    return temp0 * temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                Color temp0 = (Color)arg0;
                return (Func<Color>)(() =>
                {
                    return temp0 * arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                Color temp1 = (Color)arg1;
                return (Func<Color>)(() =>
                {
                    return arg0Func() * temp1;
                });
            }
            else
            {
                return (Func<Color>)(() =>
                {
                    return arg0Func() * arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Color), Token.Type.Division, typeof(Color))]
        public static object Divide_Color(object arg0, object arg1, CompilerData cdata)
        {
            Func<Color> arg0Func = CompilerUtility.ForceGetFunction<Color>(arg0, cdata);
            Func<Color> arg1Func = CompilerUtility.ForceGetFunction<Color>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                Color temp0 = (Color)arg0;
                Color temp1 = (Color)arg1;
                return (Func<Color>)(() =>
                {
                    return temp0 / temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                Color temp0 = (Color)arg0;
                return (Func<Color>)(() =>
                {
                    return temp0 / arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                Color temp1 = (Color)arg1;
                return (Func<Color>)(() =>
                {
                    return arg0Func() / temp1;
                });
            }
            else
            {
                return (Func<Color>)(() =>
                {
                    return arg0Func() / arg1Func();
                });
            }
        }
        */
    }
}