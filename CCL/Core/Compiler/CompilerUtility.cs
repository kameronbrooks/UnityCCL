using System;

namespace CCL.Core
{
    internal class CompilerUtility
    {
        public static Func<T> ForceGetFunction<T>(object refOrFunc, CompilerData cdata)
        {
            if (refOrFunc == null) return null;
            else if (IsFunc(refOrFunc))
            {
                if ((refOrFunc as Func<T>) != null)
                {
                    return (Func<T>)refOrFunc;
                }
                else if ((refOrFunc as Func<object>) != null)
                {
                    if (typeof(T) == typeof(object))
                    {
                        return () => { return (T)((Func<object>)refOrFunc)(); };
                    }
                    else
                    {
                        return (Func<T>)refOrFunc;
                    }
                }
                else
                {
                    TypeDef typedef = cdata._assembly.GetTypeDef(CompilerUtility.GetReturnType(refOrFunc));
                    if (typedef != null)
                    {
                        Func<object> genFunc = typedef.ToGenericFunction(refOrFunc, cdata);
                        if (typeof(T) == typeof(object))
                        {
                            return () => { return (T)genFunc(); };
                        }
                        else
                        {
                            return (Func<T>)(object)genFunc;
                        }
                    }
                }
                throw new Exception("Cannot cast from " + refOrFunc.GetType().ToString() + " to " + typeof(Func<T>).ToString());
            }
            else if (IsReference(refOrFunc)) return ((Reference)refOrFunc).CreateGetFunction<T>(cdata);
            else return null;
        }

        public static Func<object> ForceGetFunction(object refOrFunc, CompilerData cdata)
        {
            return ForceGetFunction<object>(refOrFunc, cdata);
        }

        /// <summary>
        /// Returns the return type of a function, or type of reference or object specified
        /// </summary>
        ///
        public static Type GetReturnType(Type t)
        {
            if (IsAction(t))
            {
                return typeof(void);
            }
            else if (IsFunc(t))
            {
                Type generic = t.GetGenericTypeDefinition();
                Type[] t2 = t.GenericTypeArguments;
                return t2[t2.Length - 1];
            }
            else
            {
                return t;
            }
        }

        public static Type GetReturnType(object func)
        {
            if (func == null) return null;
            Type t = func.GetType();
            if (IsFunc(func))
            {
                Type generic = t.GetGenericTypeDefinition();
                Type[] t2 = t.GenericTypeArguments;
                return t2[t2.Length - 1];
            }
            else if (IsReference(func))
            {
                return ((Reference)func).type;
            }
            else if (IsAction(func))
            {
                return typeof(void);
            }
            else
            {
                return t;
            }
        }

        /// <summary>
        /// Returns true if the object is an Action
        /// </summary>
        public static bool IsAction(object ob)
        {
            if (ob == null) return false;
            return ob.GetType().FullName.Contains("System.Action");
        }

        public static bool IsAction(Type t)
        {
            if (t == null) return false;
            return t.FullName.Contains("System.Action");
        }

        /// <summary>
        /// Returns true if the object is a Func
        /// </summary>
        public static bool IsFunc(object ob)
        {
            if (ob == null) return false;
            return ob.GetType().FullName.Contains("System.Func");
        }

        public static bool IsFunc(Type t)
        {
            if (t == null) return false;
            return t.FullName.Contains("System.Func");
        }

        /// <summary>
        /// Returns true if the object is a Refernece type
        /// </summary>
        public static bool IsReference(object ob)
        {
            if (ob == null) return false;
            return (ob as Reference) != null;
        }

        /*
        public static Func<T> ForceGetFunction<T>(object refOrFunc, CompilerData cdata, Assembly assembly)
        {
            if (refOrFunc == null) return null;
            else if (IsFunc(refOrFunc)) {
                if((refOrFunc as Func<T>) != null)
                {
                    return (Func<T>)refOrFunc;
                }
                else if ((refOrFunc as Func<object>) != null)
                {
                    return () => { return (T)((Func<object>)refOrFunc)(); };
                }
                else
                {
                    TypeDef typedef = assembly.GetTypeDef(GetReturnType(refOrFunc));
                    if(typedef != null)
                    {
                        return () => { return (T)(typedef.ToGenericFunction(refOrFunc, cdata))(); };
                    }
                    throw new Exception("Cannot cast from " + refOrFunc.GetType().ToString() + " to " + typeof(Func<T>).ToString());
                }
            }
            else if (IsReference(refOrFunc)) return ((Reference)refOrFunc).CreateGetFunction<T>(cdata);
            else return null;
        }

        public static Func<object> ForceGetFunction(object refOrFunc, CompilerData cdata, Assembly assembly)
        {
            if (refOrFunc == null) return null;
            else if (IsFunc(refOrFunc))
            {
                if ((refOrFunc as Func<object>) != null)
                {
                    return (Func<object>)refOrFunc;
                }
                else
                {
                    TypeDef typedef = assembly.GetTypeDef(GetReturnType(refOrFunc));
                    if (typedef != null)
                    {
                        return typedef.ToGenericFunction(refOrFunc, cdata);
                    }
                    throw new Exception("Cannot cast from " + refOrFunc.GetType().ToString() + " to " + typeof(Func<object>).ToString());
                }
            }
            else if (IsReference(refOrFunc)) return ((Reference)refOrFunc).CreateGetFunction(cdata);
            else return null;
        }
        */

        public static Func<T> ObjectToFunction<T>(object ob)
        {
            return () =>
            {
                return (T)ob;
            };
        }

        public static Func<object> ObjectToFunction(object ob)
        {
            return () =>
            {
                return ob;
            };
        }
    }
}