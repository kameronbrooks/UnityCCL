using System;
using System.Collections;

namespace CCL.Core
{
    internal class CompileTimeArrayAccessReferenceListAccess : CompileTimeArrayAccessReference
    {
        private Type _elemType;

        public CompileTimeArrayAccessReferenceListAccess(Type t, string identifer, int envIndex, object accessor) : base(t, identifer, envIndex, accessor)
        {
            _accessor = accessor;
            _elemType = (t != typeof(object)) ? ArrayUtility.GetElementType(t) : typeof(object);
        }

        public override Type type
        {
            get
            {
                return _type;
            }
        }

        public override Func<T> CreateGetFunction<T>(CompilerData cdata)
        {
            Func<object> accessorFunc = CompilerUtility.ForceGetFunction<object>(_accessor, cdata);

            if (accessorFunc == null)
            {
                IList list = _accessor as IList;
                int accessorCount = list.Count;
                return () =>
                {
                    IList arr = System.Array.CreateInstance(_elemType, accessorCount);
                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        arr[i] = ((IList)cdata._environment[_environmentIndex].data)[(int)list[i]];
                    }
                    return (T)arr;
                };
            }
            else
            {
                return () =>
                {
                    IList list = accessorFunc() as IList;
                    int accessorCount = list.Count;

                    IList arr = System.Array.CreateInstance(_elemType, accessorCount);
                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        arr[i] = ((IList)cdata._environment[_environmentIndex].data)[(int)list[i]];
                    }
                    return (T)arr;
                };
            }
        }

        public override Func<T> CreateModifyFunction<T>(object value, CompilerData cdata, Token.Type operation)
        {
            throw new System.InvalidOperationException("Cannot modify array indexed results");
            /*
            Type rtype = CompilerUtility.GetReturnType(value);
            Operation.BinaryOperationDelegate del = cdata._assembly.GetBinaryOperation(operation, ArrayUtility.GetElementType(type), rtype);

            // This section was made for runtime type inference
            // May cause some weird issues, definitely check this out if there are problems
            if (del == null && type != typeof(Object) && rtype == typeof(Object)) del = cdata._assembly.GetBinaryOperation(operation, type, type);

            if (del == null && type == typeof(Object) && rtype != typeof(Object)) del = cdata._assembly.GetBinaryOperation(operation, rtype, rtype);

            if (del == null) throw new CompilationException("Cannot perform the operation " + operation.ToString() + " on " + type.Name + " and " + rtype.Name);

            Func<object> accessorFunc = CompilerUtility.ForceGetFunction<object>(_accessor, cdata);

            object func = del(this, value, cdata);
            Type returnType = CompilerUtility.GetReturnType(func);
            TypeDef returnTypeDef = cdata._assembly.GetTypeDef(returnType);

            if (accessorFunc == null)
            {
                T val = (T)value;
                IList list = _accessor as IList;
                int accessorCount = list.Count;
                return () =>
                {
                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        object ob = returnTypeDef.CallFunction(del(((IList)cdata._environment[_environmentIndex].data)[(int)list[i]], val, cdata));
                        ((IList)cdata._environment[_environmentIndex].data)[(int)list[i]] = ob;
                    }
                    return (T)val;
                };
            }
            else
            {
                return () =>
                {
                    T val = (T)value;
                    IList list = accessorFunc() as IList;
                    int accessorCount = list.Count;

                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        object ob = returnTypeDef.CallFunction(del(((IList)cdata._environment[_environmentIndex].data)[(int)list[i]], val, cdata));
                        ((IList)cdata._environment[_environmentIndex].data)[(int)list[i]] = ob;
                    }
                    return (T)val;
                };
            }
            */
        }

        public override Func<T> CreateSetFunction<T>(object value, CompilerData cdata)
        {
            Func<object> accessorFunc = CompilerUtility.ForceGetFunction(_accessor, cdata);
            Func<T> valueFunc = CompilerUtility.ForceGetFunction<T>(value, cdata);

            if (valueFunc == null && accessorFunc == null)
            {
                T val = (T)value;
                IList list = _accessor as IList;
                int accessorCount = list.Count;
                return () =>
                {
                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        ((IList)cdata._environment[_environmentIndex].data)[(int)list[i]] = val;
                    }
                    return (T)val;
                };
            }
            else if (valueFunc == null && accessorFunc != null)
            {
                T val = (T)value;
                return () =>
                {
                    IList list = accessorFunc() as IList;
                    int accessorCount = list.Count;

                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        ((IList)cdata._environment[_environmentIndex].data)[(int)list[i]] = val;
                    }
                    return (T)val;
                };
            }
            if (valueFunc != null && accessorFunc == null)
            {
                T val = valueFunc();
                IList list = _accessor as IList;
                int accessorCount = list.Count;
                return () =>
                {
                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        ((IList)cdata._environment[_environmentIndex].data)[(int)list[i]] = val;
                    }
                    return (T)val;
                };
            }
            else
            {
                return () =>
                {
                    T val = valueFunc();
                    IList list = accessorFunc() as IList;
                    int accessorCount = list.Count;

                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        ((IList)cdata._environment[_environmentIndex].data)[(int)list[i]] = val;
                    }
                    return (T)val;
                };
            }
        }
    }
}