using CCL.Core;
using System;

namespace CCL.Libraries
{
    public static class StringLib
    {
        [BinaryOperator(typeof(String), Token.Type.Addition, typeof(Boolean))]
        public static object Add_Bool(object arg0, object arg1, CompilerData cdata)
        {
            Func<string> arg0Func = CompilerUtility.ForceGetFunction<string>(arg0, cdata);
            Func<bool> arg1Func = CompilerUtility.ForceGetFunction<bool>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                string temp0 = (string)arg0;
                bool temp1 = (bool)arg1;
                return (Func<string>)(() =>
                {
                    return temp0 + temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                string temp0 = (string)arg0;
                return (Func<string>)(() =>
                {
                    return temp0 + arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                bool temp1 = (bool)arg1;
                return (Func<string>)(() =>
                {
                    return arg0Func() + temp1;
                });
            }
            else
            {
                return (Func<string>)(() =>
                {
                    return arg0Func() + arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(String), Token.Type.Addition, typeof(Single))]
        public static object Add_Float(object arg0, object arg1, CompilerData cdata)
        {
            Func<string> arg0Func = CompilerUtility.ForceGetFunction<string>(arg0, cdata);
            Func<float> arg1Func = CompilerUtility.ForceGetFunction<float>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                string temp0 = (string)arg0;
                float temp1 = (float)arg1;
                return (Func<string>)(() =>
                {
                    return temp0 + temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                string temp0 = (string)arg0;
                return (Func<string>)(() =>
                {
                    return temp0 + arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                float temp1 = (float)arg1;
                return (Func<string>)(() =>
                {
                    return arg0Func() + temp1;
                });
            }
            else
            {
                return (Func<string>)(() =>
                {
                    return arg0Func() + arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(String), Token.Type.Addition, typeof(Int32))]
        public static object Add_Int(object arg0, object arg1, CompilerData cdata)
        {
            Func<string> arg0Func = CompilerUtility.ForceGetFunction<string>(arg0, cdata);
            Func<int> arg1Func = CompilerUtility.ForceGetFunction<int>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                string temp0 = (string)arg0;
                int temp1 = (int)arg1;
                return (Func<string>)(() =>
                {
                    return temp0 + temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                string temp0 = (string)arg0;
                return (Func<string>)(() =>
                {
                    return temp0 + arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                int temp1 = (int)arg1;
                return (Func<string>)(() =>
                {
                    return arg0Func() + temp1;
                });
            }
            else
            {
                return (Func<string>)(() =>
                {
                    return arg0Func() + arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(String), Token.Type.Addition, typeof(String))]
        public static object Add_String(object arg0, object arg1, CompilerData cdata)
        {
            Func<string> arg0Func = CompilerUtility.ForceGetFunction<string>(arg0, cdata);
            Func<string> arg1Func = CompilerUtility.ForceGetFunction<string>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                string temp0 = (string)arg0;
                string temp1 = (string)arg1;
                return (Func<string>)(() =>
                {
                    return temp0 + temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                string temp0 = (string)arg0;
                return (Func<string>)(() =>
                {
                    return temp0 + arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                string temp1 = (string)arg1;
                return (Func<string>)(() =>
                {
                    return arg0Func() + temp1;
                });
            }
            else
            {
                return (Func<string>)(() =>
                {
                    return arg0Func() + arg1Func();
                });
            }
        }

        [BinaryOperator(typeof(String), Token.Type.Assign, typeof(String))]
        public static object Assign_String(object arg0, object arg1, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            Func<string> arg1Func = CompilerUtility.ForceGetFunction<string>(arg1, cdata);

            if (arg1Func != null)
            {
                return ((Reference)arg0).CreateSetFunction<string>(arg1Func, cdata);
            }
            else
            {
                string temp = (string)arg1;
                return ((Reference)arg0).CreateSetFunction<string>(temp, cdata);
            }
        }

        [BinaryOperator(typeof(String), Token.Type.AssignAdd, typeof(String))]
        public static object AssignAdd_String(object arg0, object arg1, CompilerData cdata)
        {
            if (!CompilerUtility.IsReference(arg0)) throw cdata.CreateException("lvalue must be an assignable type");
            Func<string> arg1Func = CompilerUtility.ForceGetFunction<string>(arg1, cdata);

            if (arg1Func != null)
            {
                return ((Reference)arg0).CreateModifyFunction<string>(arg1Func, cdata, Token.Type.Addition);
            }
            else
            {
                string temp = (string)arg1;
                return ((Reference)arg0).CreateModifyFunction<string>(temp, cdata, Token.Type.Addition);
            }
        }

        [BinaryOperator(typeof(String), Token.Type.Equals, typeof(String))]
        public static object Equals_String(object arg0, object arg1, CompilerData cdata)
        {
            Func<string> arg0Func = CompilerUtility.ForceGetFunction<string>(arg0, cdata);
            Func<string> arg1Func = CompilerUtility.ForceGetFunction<string>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                string temp0 = (string)arg0;
                string temp1 = (string)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 == temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                string temp0 = (string)arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 == arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                string temp1 = (string)arg1;
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

        [BinaryOperator(typeof(String[]), Token.Type.IsNotSubsetOf, typeof(String[]))]
        public static object IsArrayNotSubsetOf_StringArray(object arg0, object arg1, CompilerData cdata)
        {
            Func<string[]> arg0Func = CompilerUtility.ForceGetFunction<string[]>(arg0, cdata);
            Func<string[]> arg1Func = CompilerUtility.ForceGetFunction<string[]>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                string[] temp0 = (string[])arg0;
                string[] temp1 = (string[])arg1;
                return (Func<bool>)(() =>
                {
                    return !ArrayUtility.IsSubsetOf((Array)temp0, (Array)temp1);
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                string[] temp0 = (string[])arg0;
                return (Func<bool>)(() =>
                {
                    return !ArrayUtility.IsSubsetOf((Array)temp0, (Array)arg1Func());
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                string[] temp1 = (string[])arg1;
                return (Func<bool>)(() =>
                {
                    return !ArrayUtility.IsSubsetOf((Array)arg0Func(), (Array)temp1);
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return !ArrayUtility.IsSubsetOf((Array)arg0Func(), (Array)arg1Func());
                });
            }
        }

        [BinaryOperator(typeof(String[]), Token.Type.IsSubsetOf, typeof(String[]))]
        public static object IsArraySubsetOf_StringArray(object arg0, object arg1, CompilerData cdata)
        {
            Func<string[]> arg0Func = CompilerUtility.ForceGetFunction<string[]>(arg0, cdata);
            Func<string[]> arg1Func = CompilerUtility.ForceGetFunction<string[]>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                string[] temp0 = (string[])arg0;
                string[] temp1 = (string[])arg1;
                return (Func<bool>)(() =>
                {
                    return ArrayUtility.IsSubsetOf((Array)temp0, (Array)temp1);
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                string[] temp0 = (string[])arg0;
                return (Func<bool>)(() =>
                {
                    return ArrayUtility.IsSubsetOf((Array)temp0, (Array)arg1Func());
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                string[] temp1 = (string[])arg1;
                return (Func<bool>)(() =>
                {
                    return ArrayUtility.IsSubsetOf((Array)arg0Func(), (Array)temp1);
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return ArrayUtility.IsSubsetOf((Array)arg0Func(), (Array)arg1Func());
                });
            }
        }

        [BinaryOperator(typeof(String), Token.Type.IsNotSubsetOf, typeof(String))]
        public static object IsNotSubsetOf_String(object arg0, object arg1, CompilerData cdata)
        {
            Func<string> arg0Func = CompilerUtility.ForceGetFunction<string>(arg0, cdata);
            Func<string> arg1Func = CompilerUtility.ForceGetFunction<string>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                string temp0 = (string)arg0;
                string temp1 = (string)arg1;
                return (Func<bool>)(() =>
                {
                    return !temp1.Contains(temp0);
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                string temp0 = (string)arg0;
                return (Func<bool>)(() =>
                {
                    return !arg1Func().Contains(temp0);
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                string temp1 = (string)arg1;
                return (Func<bool>)(() =>
                {
                    return !temp1.Contains(arg0Func());
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return !arg1Func().Contains(arg0Func());
                });
            }
        }

        [BinaryOperator(typeof(String), Token.Type.IsNotSubsetOf, typeof(String[]))]
        public static object IsNotSubsetOf_StringArray(object arg0, object arg1, CompilerData cdata)
        {
            Func<string> arg0Func = CompilerUtility.ForceGetFunction<string>(arg0, cdata);
            Func<string[]> arg1Func = CompilerUtility.ForceGetFunction<string[]>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                string temp0 = (string)arg0;
                string[] temp1 = (string[])arg1;
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf((Array)temp1, temp0) < 0;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                string temp0 = (string)arg0;
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf(arg1Func(), temp0) < 0;
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                string[] temp1 = (string[])arg1;
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf(temp1, arg0Func()) < 0;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf(arg1Func(), arg0Func()) < 0;
                });
            }
        }

        [BinaryOperator(typeof(String), Token.Type.IsSubsetOf, typeof(String))]
        public static object IsSubsetOf_String(object arg0, object arg1, CompilerData cdata)
        {
            Func<string> arg0Func = CompilerUtility.ForceGetFunction<string>(arg0, cdata);
            Func<string> arg1Func = CompilerUtility.ForceGetFunction<string>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                string temp0 = (string)arg0;
                string temp1 = (string)arg1;
                return (Func<bool>)(() =>
                {
                    return temp1.Contains(temp0);
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                string temp0 = (string)arg0;
                return (Func<bool>)(() =>
                {
                    return arg1Func().Contains(temp0);
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                string temp1 = (string)arg1;
                return (Func<bool>)(() =>
                {
                    return temp1.Contains(arg0Func());
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return arg1Func().Contains(arg0Func());
                });
            }
        }

        [BinaryOperator(typeof(String), Token.Type.IsSubsetOf, typeof(String[]))]
        public static object IsSubsetOf_StringArray(object arg0, object arg1, CompilerData cdata)
        {
            Func<string> arg0Func = CompilerUtility.ForceGetFunction<string>(arg0, cdata);
            Func<string[]> arg1Func = CompilerUtility.ForceGetFunction<string[]>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                string temp0 = (string)arg0;
                string[] temp1 = (string[])arg1;
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf((Array)temp1, temp0) > -1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                string temp0 = (string)arg0;
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf(arg1Func(), temp0) > -1;
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                string[] temp1 = (string[])arg1;
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf(temp1, arg0Func()) > -1;
                });
            }
            else
            {
                return (Func<bool>)(() =>
                {
                    return Array.IndexOf(arg1Func(), arg0Func()) > -1;
                });
            }
        }

        [BinaryOperator(typeof(String), Token.Type.NotEquals, typeof(String))]
        public static object NotEquals_String(object arg0, object arg1, CompilerData cdata)
        {
            Func<string> arg0Func = CompilerUtility.ForceGetFunction<string>(arg0, cdata);
            Func<string> arg1Func = CompilerUtility.ForceGetFunction<string>(arg1, cdata);

            if (arg0Func == null && arg1Func == null)
            {
                string temp0 = (string)arg0;
                string temp1 = (string)arg1;
                return (Func<bool>)(() =>
                {
                    return temp0 != temp1;
                });
            }
            else if (arg0Func == null && arg1Func != null)
            {
                string temp0 = (string)arg0;
                return (Func<bool>)(() =>
                {
                    return temp0 != arg1Func();
                });
            }
            else if (arg0Func != null && arg1Func == null)
            {
                string temp1 = (string)arg1;
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

        [DelegateSignature(typeof(Action<String>))]
        public static object Signature_Action_String(object func, object[] args)
        {
            ((Action<string>)func)((string)args[0]);
            return null;
        }

        [DelegateSignature(typeof(Action<String, String>))]
        public static object Signature_Action_String2(object func, object[] args)
        {
            ((Action<string, string>)func)((string)args[0], (string)args[1]);
            return null;
        }

        [DelegateSignature(typeof(Action<String, String, String>))]
        public static object Signature_Action_String3(object func, object[] args)
        {
            ((Action<string, string, string>)func)((string)args[0], (string)args[1], (string)args[2]);
            return null;
        }

        [DelegateSignature(typeof(Func<String>))]
        public static object Signature_Func_String(object func, object[] args)
        {
            return ((Func<string>)func)();
        }

        [DelegateSignature(typeof(Func<String[], String>))]
        public static object Signature_Func_String_StringArr(object func, object[] args)
        {
            return ((Func<string[], string>)func)((string[])args[0]);
        }

        [DelegateSignature(typeof(Func<String, String>))]
        public static object Signature_Func_String2(object func, object[] args)
        {
            return ((Func<string, string>)func)((string)args[0]);
        }

        [DelegateSignature(typeof(Func<String, String, String>))]
        public static object Signature_Func_String3(object func, object[] args)
        {
            return ((Func<string, string, string>)func)((string)args[0], (string)args[1]);
        }

        public class StringType : TypeDef
        {
            public override string[] alias
            {
                get
                {
                    return new string[] { "String" };
                }
            }

            public override string name
            {
                get
                {
                    return "string";
                }
            }

            public override Type type
            {
                get
                {
                    return typeof(string);
                }
            }

            public override object CallFunction(object func)
            {
                return ((Func<string>)func)();
            }

            public override object Cast(object arg, CompilerData _internal)
            {
                Type argType = CompilerUtility.GetReturnType(arg);

                if (CompilerUtility.IsFunc(arg) || CompilerUtility.IsReference(arg))
                {
                    if (argType == typeof(bool))
                    {
                        Func<bool> func = CompilerUtility.ForceGetFunction<bool>(arg, _internal);
                        return (Func<string>)(() => { return func().ToString(); });
                    }
                    if (argType == typeof(sbyte))
                    {
                        Func<sbyte> func = CompilerUtility.ForceGetFunction<sbyte>(arg, _internal);
                        return (Func<string>)(() => { return func().ToString(); });
                    }
                    else if (argType == typeof(int))
                    {
                        Func<int> func = CompilerUtility.ForceGetFunction<int>(arg, _internal);
                        return (Func<string>)(() => { return func().ToString(); });
                    }
                    else if (argType == typeof(float))
                    {
                        Func<float> func = CompilerUtility.ForceGetFunction<float>(arg, _internal);
                        return (Func<string>)(() => { return func().ToString(); });
                    }
                    else if (argType == typeof(string))
                    {
                        Func<string> func = CompilerUtility.ForceGetFunction<string>(arg, _internal);
                        return func;
                    }
                    else
                    {
                        Func<object> func = (Func<object>)arg;
                        return (Func<string>)(() => { return func().ToString(); });
                    }
                }
                else
                {
                    if (argType == typeof(bool))
                    {
                        bool temp = (bool)arg;
                        return (Func<string>)(() => { return temp.ToString(); });
                    }
                    if (argType == typeof(sbyte))
                    {
                        sbyte temp = (sbyte)arg;
                        return (Func<string>)(() => { return temp.ToString(); });
                    }
                    else if (argType == typeof(int))
                    {
                        int temp = (int)arg;
                        return (Func<string>)(() => { return temp.ToString(); });
                    }
                    else if (argType == typeof(float))
                    {
                        float temp = (float)arg;
                        return (Func<string>)(() => { return temp.ToString(); });
                    }
                    else if (argType == typeof(string))
                    {
                        string temp = (string)arg;
                        return (Func<string>)(() => { return temp; });
                    }
                    else
                    {
                        object temp = arg;
                        return (Func<string>)(() => { return temp.ToString(); });
                    }
                }

                //throw cdata.CreateException("No supported cast for type string and " + argType.Name);
            }

            public override object CastArray(object arg, CompilerData _internal)
            {
                Type argType = CompilerUtility.GetReturnType(arg);

                if (CompilerUtility.IsFunc(arg) || CompilerUtility.IsReference(arg))
                {
                    if (argType == typeof(object))
                    {
                        Func<object> func = CompilerUtility.ForceGetFunction(arg, _internal);
                        return (Func<string[]>)(() => { return (string[])func(); });
                    }
                }
                else
                {
                    if (argType == typeof(object))
                    {
                        object temp = arg;
                        return (Func<string[]>)(() => { return (string[])temp; });
                    }
                }
                throw _internal.CreateException("No supported cast for type bool[] and " + argType.Name);
            }

            public override object CreateArrayLiteral(object[] elements, CompilerData cdata)
            {
                return CreateArrayLiteral<string>(elements, cdata);
            }

            public override Func<object> DefaultConstructor()
            {
                return () => { return ""; };
            }

            public override Func<object> ToGenericFunction(object arg, CompilerData _internal)
            {
                if (arg.GetType() == typeof(Func<string>))
                {
                    return () =>
                    {
                        return ((Func<string>)(arg))();
                    };
                }
                else if (arg.GetType() == typeof(Func<string[]>))
                {
                    return () =>
                    {
                        return ((Func<string[]>)(arg))();
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