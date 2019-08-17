using CCL.Core;
using System;
using UnityEngine;

namespace CCL.Libraries
{
    public static class QuaternionLib
    {
        public class QuaternionType : TypeDef
        {
            public override string name
            {
                get
                {
                    return "Quaternion";
                }
            }

            public override Type type
            {
                get
                {
                    return typeof(Quaternion);
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
                        return (Func<Quaternion>)(() =>
                        {
                            float[] arr = func();
                            return new Quaternion(arr[0], arr[1], arr[2], arr[3]);
                        });
                    }
                    else if (argType == typeof(Int32[]))
                    {
                        Func<int[]> func = CompilerUtility.ForceGetFunction<int[]>(arg, _internal);
                        return (Func<Quaternion>)(() =>
                        {
                            int[] arr = func();
                            return new Quaternion(arr[0], arr[1], arr[2], arr[3]);
                        });
                    }
                    else if (argType == typeof(Quaternion))
                    {
                        Func<Quaternion> func = CompilerUtility.ForceGetFunction<Quaternion>(arg, _internal);
                        return func;
                    }
                    else if (argType == typeof(object))
                    {
                        Func<object> func = CompilerUtility.ForceGetFunction(arg, _internal);
                        return (Func<Quaternion>)(() => { return (Quaternion)func(); });
                    }
                }
                else
                {
                    if (argType == typeof(Single[]))
                    {
                        float[] temp = (float[])arg;
                        return (Func<Quaternion>)(() =>
                        {
                            return new Quaternion(temp[0], temp[1], temp[2], temp[3]);
                        });
                    }
                    if (argType == typeof(Int32[]))
                    {
                        int[] temp = (int[])arg;
                        return (Func<Quaternion>)(() =>
                        {
                            return new Quaternion(temp[0], temp[1], temp[2], temp[3]);
                        });
                    }
                    else if (argType == typeof(Quaternion))
                    {
                        Quaternion temp = (Quaternion)arg;
                        return (Func<Quaternion>)(() => { return temp; });
                    }
                    else if (argType == typeof(object))
                    {
                        object temp = arg;
                        return (Func<Quaternion>)(() => { return (Quaternion)temp; });
                    }
                }

                throw _internal.CreateException("No supported cast for type Quaternion and " + argType.Name);
            }

            public override object CastArray(object arg, CompilerData _internal)
            {
                Type argType = CompilerUtility.GetReturnType(arg);

                if (CompilerUtility.IsFunc(arg) || CompilerUtility.IsReference(arg))
                {
                    if (argType == typeof(object))
                    {
                        Func<object> func = CompilerUtility.ForceGetFunction(arg, _internal);
                        return (Func<Quaternion[]>)(() => { return (Quaternion[])func(); });
                    }
                }
                else
                {
                    if (argType == typeof(object))
                    {
                        object temp = arg;
                        return (Func<Quaternion[]>)(() => { return (Quaternion[])temp; });
                    }
                }
                throw _internal.CreateException("No supported cast for type Quaternion[] and " + argType.Name);
            }

            public override object CreateArrayLiteral(object[] elements, CompilerData cdata)
            {
                return CreateArrayLiteral<Quaternion>(elements, cdata);
            }

            public override Func<object> DefaultConstructor()
            {
                return () => { return new Quaternion(); };
            }

            public override Func<object> ToGenericFunction(object arg, CompilerData _internal)
            {
                if (arg.GetType() == typeof(Func<Quaternion>))
                {
                    return () =>
                    {
                        return ((Func<Quaternion>)(arg))();
                    };
                }
                if (arg.GetType() == typeof(Func<Quaternion[]>))
                {
                    return () =>
                    {
                        return ((Func<Quaternion[]>)(arg))();
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