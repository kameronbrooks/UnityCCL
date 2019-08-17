using CCL.Core;
using System;

namespace CCL.Libraries
{
    public static class ObjectLib
    {
        [BinaryOperator(typeof(Object), Token.Type.Assign, typeof(Object))]
        public static object Assign_Object(object arg0, object arg1, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            Func<object> arg1Func = CompilerUtility.ForceGetFunction(arg1, cdata);
            if (arg1Func != null)
            {
                return ((Reference)arg0).CreateSetFunction<object>(arg1Func, cdata);
            }
            else
            {
                object temp = arg1;
                return ((Reference)arg0).CreateSetFunction<object>(temp, cdata);
            }
        }

        [BinaryOperator(typeof(Object), Token.Type.Equals, null)]
        public static object Equals_Null(object arg0, object arg1, CompilerData cdata)
        {
            Func<object> arg0Func = CompilerUtility.ForceGetFunction<object>(arg0, cdata);

            if (arg0Func == null)
            {
                object temp0 = arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 == null;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return arg0Func() == null;
                });
            }
        }

        [BinaryOperator(typeof(Object), Token.Type.Equals, typeof(Object))]
        public static object Equals_Object(object arg0, object arg1, CompilerData cdata)
        {
            Func<object> arg0Func = CompilerUtility.ForceGetFunction<object>(arg0, cdata);
            Func<object> arg1Func = CompilerUtility.ForceGetFunction<object>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                object temp0 = arg0;
                object temp1 = arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 == temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                object temp0 = arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 == arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                object temp1 = arg1;
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

        [BinaryOperator(typeof(Object), Token.Type.NotEquals, null)]
        public static object NotEquals_Null(object arg0, object arg1, CompilerData cdata)
        {
            Func<object> arg0Func = CompilerUtility.ForceGetFunction<object>(arg0, cdata);

            if (arg0Func == null)
            {
                object temp0 = arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 != null;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return arg0Func() != null;
                });
            }
        }

        [BinaryOperator(typeof(Object), Token.Type.NotEquals, typeof(Object))]
        public static object NotEquals_Object(object arg0, object arg1, CompilerData cdata)
        {
            Func<object> arg0Func = CompilerUtility.ForceGetFunction<object>(arg0, cdata);
            Func<object> arg1Func = CompilerUtility.ForceGetFunction<object>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                object temp0 = arg0;
                object temp1 = arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 != temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                object temp0 = arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 != arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                object temp1 = arg1;
                return (Func<bool>)(() =>
                {
                    return arg0Func() != temp1;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return arg0Func() != arg1Func();
                });
            }
        }

        [BinaryOperator(null, Token.Type.Equals, typeof(Object))]
        public static object Null_Equals_Object(object arg0, object arg1, CompilerData cdata)
        {
            Func<object> arg1Func = CompilerUtility.ForceGetFunction<object>(arg1, cdata);

            if (arg1Func == null)
            {
                object temp1 = arg1;
                return (Func<bool>)(() =>
                {
                    return temp1 == null;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return arg1Func() == null;
                });
            }
        }

        [BinaryOperator(null, Token.Type.NotEquals, typeof(Object))]
        public static object Null_NotEquals_Object(object arg0, object arg1, CompilerData cdata)
        {
            Func<object> arg1Func = CompilerUtility.ForceGetFunction<object>(arg1, cdata);

            if (arg1Func == null)
            {
                object temp1 = arg1;
                return (Func<bool>)(() =>
                {
                    return temp1 != null;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return arg1Func() != null;
                });
            }
        }

        [DelegateSignature(typeof(Action))]
        public static object Signature_Action(object func, object[] args)
        {
            ((Action)func)();
            return null;
        }

        [DelegateSignature(typeof(Action<Object>))]
        public static object Signature_Action_Object(object func, object[] args)
        {
            ((Action<object>)func)((object)args[0]);
            return null;
        }

        [DelegateSignature(typeof(Action<Object, Object>))]
        public static object Signature_Action_Object2(object func, object[] args)
        {
            ((Action<object, object>)func)((object)args[0], (object)args[1]);
            return null;
        }

        [DelegateSignature(typeof(Action<Object, Object, Object>))]
        public static object Signature_Action_Object3(object func, object[] args)
        {
            ((Action<object, object, object>)func)((object)args[0], (object)args[1], (object)args[2]);
            return null;
        }

        [DelegateSignature(typeof(Func<Object>))]
        public static object Signature_Func_Object(object func, object[] args)
        {
            return ((Func<object>)func)();
        }

        [DelegateSignature(typeof(Func<Object[], Object>))]
        public static object Signature_Func_Object_ObjectArr(object func, object[] args)
        {
            return ((Func<object[], object>)func)((object[])args[0]);
        }

        [DelegateSignature(typeof(Func<Object, Object>))]
        public static object Signature_Func_Object2(object func, object[] args)
        {
            return ((Func<object, object>)func)((object)args[0]);
        }

        [DelegateSignature(typeof(Func<Object, Object, Object>))]
        public static object Signature_Func_Object3(object func, object[] args)
        {
            return ((Func<object, object, object>)func)((object)args[0], (object)args[1]);
        }

        public class ObjectType : TypeDef
        {
            public override string[] alias
            {
                get
                {
                    return new string[] { "Object" };
                }
            }

            public override string name
            {
                get
                {
                    return "object";
                }
            }

            public override Type type
            {
                get
                {
                    return typeof(object);
                }
            }

            public override object Cast(object arg, CompilerData _internal)
            {
                return arg;
                //throw cdata.CreateException("No supported cast for type string and " + argType.Name);
            }

            public override Func<object> DefaultConstructor()
            {
                return () => { return new object(); };
            }

            public override Func<object> ToGenericFunction(object arg, CompilerData _internal)
            {
                if (arg.GetType() == typeof(Func<object>))
                {
                    return (Func<object>)arg;
                }
                return () =>
                {
                    return arg;
                };
            }
        }

        public class VoidType : TypeDef
        {
            public override string[] alias
            {
                get
                {
                    return new string[] { "Void" };
                }
            }

            public override string name
            {
                get
                {
                    return "void";
                }
            }

            public override Type type
            {
                get
                {
                    return typeof(void);
                }
            }

            public override object Cast(object arg, CompilerData _internal)
            {
                Type argType = CompilerUtility.GetReturnType(arg);

                if (argType == typeof(object)) return arg;

                Func<object> func = CompilerUtility.ForceGetFunction(arg, _internal);

                if (func != null)
                {
                    return (Func<object>)(() => { return (object)func(); });
                }
                else
                {
                    return (Func<object>)(() => { return (object)arg; });
                }

                //throw cdata.CreateException("No supported cast for type string and " + argType.Name);
            }

            public override object CreateArrayLiteral(object[] elements, CompilerData cdata)
            {
                return CreateArrayLiteral<object>(elements, cdata);
            }

            public override Func<object> DefaultConstructor()
            {
                return () => { return null; };
            }

            public override Func<object> ToGenericFunction(object arg, CompilerData _internal)
            {
                if (arg.GetType() == typeof(Action))
                {
                    return (Func<object>)(() => { ((Action)arg)(); return null; });
                }
                return () =>
                {
                    return arg;
                };
            }
        }
    }
}