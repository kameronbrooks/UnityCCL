using System;
using System.Collections;

namespace CCL.Core
{
    internal class CompileTimeArrayAccessReference : CompileTimeReference
    {
        protected object _accessor;

        public CompileTimeArrayAccessReference(Type t, string identifer, int envIndex, object accessor) : base(t, identifer, envIndex)
        {
            _accessor = accessor;
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

            if (accessorFunc != null)
            {
                Type accessorFuncType = CompilerUtility.GetReturnType(accessorFunc);

                return () =>
                {
                    return (T)((IList)cdata._environment[_environmentIndex].data)[(int)accessorFunc()];
                };
            }
            else
            {
                return () =>
                {
                    return (T)((IList)cdata._environment[_environmentIndex].data)[(int)_accessor];
                };
            }
        }

        public override Func<T> CreateModifyFunction<T>(object value, CompilerData cdata, Token.Type operation)
        {
            Type rtype = CompilerUtility.GetReturnType(value);
            Operation.BinaryOperationDelegate del = cdata._assembly.GetBinaryOperation(operation, type, rtype);

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
                return () =>
                {
                    object ob = returnTypeDef.CallFunction(func);
                    ((IList)(cdata._environment[_environmentIndex].data))[(int)_accessor] = ob;
                    return (T)ob;
                };
            }
            else
            {
                return () =>
                {
                    object ob = returnTypeDef.CallFunction(func);
                    ((IList)(cdata._environment[_environmentIndex].data))[(int)accessorFunc()] = ob;
                    return (T)ob;
                };
            }
        }

        public override Func<T> CreateSetFunction<T>(object value, CompilerData cdata)
        {
            Func<object> accessorFunc = CompilerUtility.ForceGetFunction(_accessor, cdata);
            Func<T> valueFunc = CompilerUtility.ForceGetFunction<T>(value, cdata);
            if (valueFunc == null && accessorFunc == null)
            {
                T val = (T)value;
                return () =>
                {
                    ((IList)(cdata._environment[_environmentIndex].data))[(int)_accessor] = val;
                    return (T)val;
                };
            }
            else if (valueFunc == null && accessorFunc != null)
            {
                T val = (T)value;
                return () =>
                {
                    ((IList)(cdata._environment[_environmentIndex].data))[(int)accessorFunc()] = val;
                    return (T)val;
                };
            }
            if (valueFunc != null && accessorFunc == null)
            {
                return () =>
                {
                    T val = (T)valueFunc();
                    ((IList)(cdata._environment[_environmentIndex].data))[(int)_accessor] = val;
                    return (T)val;
                };
            }
            else
            {
                return () =>
                {
                    T val = (T)valueFunc();
                    ((IList)(cdata._environment[_environmentIndex].data))[(int)accessorFunc()] = val;
                    return (T)val;
                };
            }
        }
    }
}