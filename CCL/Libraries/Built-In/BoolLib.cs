using CCL.Core;
using System;

namespace CCL.Libraries
{
    public static class BoolLib
    {
        [DelegateSignature(typeof(Action<bool>))]
        public static object Action_Bool(object func, object[] args)
        {
            ((Action<bool>)func)((bool)args[0]);
            return null;
        }

        [DelegateSignature(typeof(Action<bool, bool>))]
        public static object Action_Bool_Bool(object func, object[] args)
        {
            ((Action<bool, bool>)func)((bool)args[0], (bool)args[1]);
            return null;
        }

        [DelegateSignature(typeof(Action<bool[]>))]
        public static object Action_BoolArr(object func, object[] args)
        {
            ((Action<bool[]>)func)((bool[])args[0]);
            return null;
        }

        [BinaryOperator(typeof(Boolean), Token.Type.And, typeof(Boolean))]
        public static object And_Bool(object arg0, object arg1, CompilerData cdata)
        {
            Func<bool> arg0Func = CompilerUtility.ForceGetFunction<bool>(arg0, cdata);
            Func<bool> arg1Func = CompilerUtility.ForceGetFunction<bool>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                bool temp0 = (bool)arg0;
                bool temp1 = (bool)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 && temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                bool temp0 = (bool)arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 && arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                bool temp1 = (bool)arg1;
                return (Func<bool>)(() =>
                {
                    return arg0Func() && temp1;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return arg0Func() && arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(Boolean), Token.Type.Assign, typeof(Boolean))]
        public static object Assign_Bool(object arg0, object arg1, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            Func<bool> arg1Func = CompilerUtility.ForceGetFunction<bool>(arg1, cdata);

            if (arg1Func != null)
            {
                return ((Reference)arg0).CreateSetFunction<bool>(arg1Func, cdata);
            }
            else
            {
                bool temp = (bool)arg1;
                return ((Reference)arg0).CreateSetFunction<bool>(temp, cdata);
            }
        }

        [BinaryOperator(typeof(Boolean), Token.Type.Equals, typeof(Boolean))]
        public static object Equals_Bool(object arg0, object arg1, CompilerData cdata)
        {
            Func<bool> arg0Func = CompilerUtility.ForceGetFunction<bool>(arg0, cdata);
            Func<bool> arg1Func = CompilerUtility.ForceGetFunction<bool>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                bool temp0 = (bool)arg0;
                bool temp1 = (bool)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 == temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                bool temp0 = (bool)arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 == arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                bool temp1 = (bool)arg1;
                return (Func<bool>)(() =>
                {
                    return arg0Func() == temp1;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return arg0Func() == arg1Func();
                });
            }
        }

        [DelegateSignature(typeof(Func<bool>))]
        public static object Func_Bool(object func, object[] args)
        {
            return ((Func<bool>)func)();
        }

        [DelegateSignature(typeof(Func<bool, bool>))]
        public static object Func_Bool_Bool(object func, object[] args)
        {
            return ((Func<bool, bool>)func)((bool)args[0]);
        }

        [DelegateSignature(typeof(Func<bool, bool, bool>))]
        public static object Func_Bool_Bool_Bool(object func, object[] args)
        {
            return ((Func<bool, bool, bool>)func)((bool)args[0], (bool)args[1]);
        }

        [DelegateSignature(typeof(Func<bool[], bool>))]
        public static object Func_BoolArr_Bool(object func, object[] args)
        {
            return ((Func<bool[], bool>)func)((bool[])args[0]);
        }

        [UnaryOperator(typeof(Boolean), Token.Type.Not)]
        public static object Negate(object arg0, CompilerData cdata)
        {
            Func<bool> arg0Func = CompilerUtility.ForceGetFunction<bool>(arg0, cdata);
            if (arg0Func == null)
            {
                bool temp = (bool)arg0;
                return (Func<bool>)(() =>
                {
                    return !temp;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return !arg0Func();
                });
            }
        }

        [BinaryOperator(typeof(Boolean), Token.Type.Or, typeof(Boolean))]
        public static object Or_Bool(object arg0, object arg1, CompilerData cdata)
        {
            Func<bool> arg0Func = CompilerUtility.ForceGetFunction<bool>(arg0, cdata);
            Func<bool> arg1Func = CompilerUtility.ForceGetFunction<bool>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                bool temp0 = (bool)arg0;
                bool temp1 = (bool)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 || temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                bool temp0 = (bool)arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 || arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                bool temp1 = (bool)arg1;
                return (Func<bool>)(() =>
                {
                    return arg0Func() || temp1;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return arg0Func() || arg1Func();
                });
            }
        }

        public class Bool : TypeDef
        {
            public override string[] alias
            {
                get
                {
                    return new string[] { "Boolean" };
                }
            }

            public override string name
            {
                get
                {
                    return "bool";
                }
            }

            public override Type type
            {
                get
                {
                    return typeof(bool);
                }
            }

            public override object Cast(object arg, CompilerData _internal)
            {
                Type argType = CompilerUtility.GetReturnType(arg);

                if (CompilerUtility.IsFunc(arg) || CompilerUtility.IsReference(arg))
                {
                    if (argType == typeof(bool))
                    {
                        Func<bool> func = CompilerUtility.ForceGetFunction<bool>(arg, _internal);
                        return func;
                    }
                    if (argType == typeof(sbyte))
                    {
                        Func<sbyte> func = CompilerUtility.ForceGetFunction<sbyte>(arg, _internal);
                        return (Func<bool>)(() => { return func() != 0; });
                    }
                    else if (argType == typeof(int))
                    {
                        Func<int> func = CompilerUtility.ForceGetFunction<int>(arg, _internal);
                        return (Func<bool>)(() => { return func() != 0; });
                    }
                    else if (argType == typeof(float))
                    {
                        Func<float> func = CompilerUtility.ForceGetFunction<float>(arg, _internal);
                        return (Func<bool>)(() => { return func() != 0; });
                    }
                    else if (argType == typeof(string))
                    {
                        Func<string> func = CompilerUtility.ForceGetFunction<string>(arg, _internal);
                        return (Func<bool>)(() => { return func() != ""; });
                    }
                    else if (argType == typeof(object))
                    {
                        Func<object> func = CompilerUtility.ForceGetFunction<object>(arg, _internal);
                        return (Func<bool>)(() => { return (bool)func(); });
                    }
                }
                else
                {
                    if (argType == typeof(bool))
                    {
                        bool temp = (bool)arg;
                        return (Func<bool>)(() => { return temp; });
                    }
                    if (argType == typeof(sbyte))
                    {
                        sbyte temp = (sbyte)arg;
                        return (Func<bool>)(() => { return temp != 0; });
                    }
                    else if (argType == typeof(int))
                    {
                        int temp = (int)arg;
                        return (Func<bool>)(() => { return temp != 0; });
                    }
                    else if (argType == typeof(float))
                    {
                        float temp = (float)arg;
                        return (Func<bool>)(() => { return temp != 0; });
                    }
                    else if (argType == typeof(string))
                    {
                        string temp = (string)arg;
                        return (Func<bool>)(() => { return temp != ""; });
                    }
                    else if (argType == typeof(object))
                    {
                        object temp = (object)arg;
                        return (Func<bool>)(() => { return (bool)arg; });
                    }
                }

                throw _internal.CreateException("No supported cast for type int and " + argType.Name);
            }

            public override object CastArray(object arg, CompilerData _internal)
            {
                Type argType = CompilerUtility.GetReturnType(arg);

                if (CompilerUtility.IsFunc(arg) || CompilerUtility.IsReference(arg))
                {
                    if (argType == typeof(object))
                    {
                        Func<object> func = CompilerUtility.ForceGetFunction(arg, _internal);
                        return (Func<bool[]>)(() => { return (bool[])func(); });
                    }
                }
                else
                {
                    if (argType == typeof(object))
                    {
                        object temp = arg;
                        return (Func<bool[]>)(() => { return (bool[])temp; });
                    }
                }
                throw _internal.CreateException("No supported cast for type bool[] and " + argType.Name);
            }

            public override object CreateArrayLiteral(object[] elements, CompilerData cdata)
            {
                return CreateArrayLiteral<bool>(elements, cdata);
            }

            public override Func<object> DefaultConstructor()
            {
                return () => { return false; };
            }

            public override Func<object> ToGenericFunction(object arg, CompilerData _internal)
            {
                if (arg.GetType() == typeof(Func<bool>))
                {
                    return () =>
                    {
                        return ((Func<bool>)(arg))();
                    };
                }
                else if (arg.GetType() == typeof(Func<bool[]>))
                {
                    return () =>
                    {
                        return ((Func<bool[]>)(arg))();
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