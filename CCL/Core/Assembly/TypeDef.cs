using System;
using System.Collections;
using System.Reflection;

namespace CCL.Core
{
    public abstract class TypeDef
    {
        public virtual string[] alias
        {
            get
            {
                return new string[0];
            }
        }

        public virtual bool allowStaticMembers
        {
            get
            {
                return true;
            }
        }

        public abstract string name { get; }

        public virtual bool staticClass
        {
            get
            {
                return false;
            }
        }

        public abstract Type type { get; }

        public virtual object CallFunction(object func)
        {
            return func.GetType().GetMethod("Invoke").Invoke(func, null);
        }

        public virtual object Cast(object arg, CompilerData _internal)
        {
            return (Func<object>)(() => { return arg; });
        }

        public virtual object CastArray(object arg, CompilerData _internal)
        {
            return (Func<object[]>)(() => { return (object[])arg; });
        }

        public virtual object CreateArrayLiteral(object[] elements, CompilerData cdata)
        {
            return CreateArrayLiteral<object>(elements, cdata);
        }

        public virtual Func<object> DefaultConstructor()
        {
            ConstructorInfo ctorInfo = type.GetConstructor(new Type[0]);
            return (Func<object>)(() => { return ctorInfo.Invoke(null); });
        }

        public virtual object RuntimeCast(object arg)
        {
            return (object)arg;
        }

        public virtual object RuntimeCastArray(object arg)
        {
            return (object[])arg;
        }

        public virtual Func<object> ToGenericFunction(object arg, CompilerData _internal)
        {
            if (CompilerUtility.IsFunc(arg))
            {
                if (type.IsSubclassOf(typeof(object)))
                {
                    return (Func<object>)arg;
                }
                throw new InvalidCastException("Cannot cast " + type.Name + " to Func<object>");
            }
            else
            {
                return () => { return arg; };
            }
        }

        protected virtual Func<T[]> CreateArrayLiteral<T>(object[] elements, CompilerData cdata)
        {
            Type elemType = typeof(T);
            return (Func<T[]>)(() =>
            {
                int length = elements.Length;
                IList output = Array.CreateInstance(type, length);
                for (int i = 0; i < length; i += 1)
                {
                    var func = elements[i] as Func<T>;

                    if (func != null)
                    {
                        output[i] = func();
                    }
                    else
                    {
                        output[i] = (T)elements[i];
                    }
                }
                return (T[])output;
            });
        }
    }
}