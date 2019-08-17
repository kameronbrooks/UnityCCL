using System;
using System.Collections;

namespace CCL.Core
{
    internal class RunTimeArrayAccessReference : RunTimeReference
    {
        protected object _accessor;

        public RunTimeArrayAccessReference(Type t, string identifer, object target, object accessor, Type targetType) : base(t, identifer, target, targetType)
        {
            _accessor = accessor;
            _type = t;
            _targetType = targetType;
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
            Func<object> accessorFunc = CompilerUtility.ForceGetFunction(_accessor, cdata);
            Func<object> targetFunc = (CompilerUtility.IsFunc(_targetObject)) ? (Func<object>)_targetObject : null;

            if (targetFunc == null && accessorFunc == null)
            {
                return () =>
                {
                    return (T)((IList)_targetObject)[(int)_accessor];
                };
            }
            else if (targetFunc == null && accessorFunc != null)
            {
                return () =>
                {
                    return (T)((IList)_targetObject)[(int)accessorFunc()];
                };
            }
            else if (targetFunc != null && accessorFunc == null)
            {
                return () =>
                {
                    return (T)((IList)targetFunc())[(int)_accessor];
                };
            }
            else
            {
                return () =>
                {
                    return (T)((IList)targetFunc())[(int)accessorFunc()];
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

            Func<object> targetFunc = CompilerUtility.ForceGetFunction<object>(_targetObject, cdata);
            Func<object> accessorFunc = CompilerUtility.ForceGetFunction<object>(_accessor, cdata);

            object func = del(this, value, cdata);
            Type returnType = CompilerUtility.GetReturnType(func);
            TypeDef returnTypeDef = cdata._assembly.GetTypeDef(returnType);

            if (targetFunc == null && accessorFunc == null)
            {
                return () =>
                {
                    object ob = returnTypeDef.CallFunction(func);
                    ((IList)(_targetObject))[(int)_accessor] = ob;
                    return (T)ob;
                };
            }
            if (targetFunc == null && accessorFunc != null)
            {
                return () =>
                {
                    object ob = returnTypeDef.CallFunction(func);
                    ((IList)(_targetObject))[(int)accessorFunc()] = ob;
                    return (T)ob;
                };
            }
            if (targetFunc != null && accessorFunc == null)
            {
                return () =>
                {
                    object ob = returnTypeDef.CallFunction(func);
                    ((IList)(targetFunc()))[(int)_accessor] = ob;
                    return (T)ob;
                };
            }
            else
            {
                return () =>
                {
                    object ob = returnTypeDef.CallFunction(func);
                    ((IList)(targetFunc()))[(int)accessorFunc()] = ob;
                    return (T)ob;
                };
            }
        }

        public override Func<T> CreateSetFunction<T>(object value, CompilerData cdata)
        {
            Func<object> accessorFunc = CompilerUtility.ForceGetFunction(_accessor, cdata);
            Func<object> targetFunc = CompilerUtility.ForceGetFunction(_targetObject, cdata);
            Func<object> valueFunc = CompilerUtility.ForceGetFunction(value, cdata);

            if (valueFunc == null && targetFunc == null && accessorFunc == null)
            {
                return () =>
                {
                    ((IList)(_targetObject))[(int)_accessor] = value;
                    return (T)value;
                };
            }
            else if (valueFunc == null && targetFunc == null && accessorFunc != null)
            {
                return () =>
                {
                    ((IList)(_targetObject))[(int)accessorFunc()] = value;
                    return (T)value;
                };
            }
            else if (valueFunc == null && targetFunc != null && accessorFunc == null)
            {
                return () =>
                {
                    ((IList)(targetFunc()))[(int)_accessor] = value;
                    return (T)value;
                };
            }
            else if (valueFunc == null && targetFunc != null && accessorFunc != null)
            {
                return () =>
                {
                    ((IList)(targetFunc()))[(int)accessorFunc()] = value;
                    return (T)value;
                };
            }
            else if (valueFunc != null && targetFunc == null && accessorFunc == null)
            {
                return () =>
                {
                    object val = valueFunc();
                    ((IList)(_targetObject))[(int)_accessor] = val;
                    return (T)val;
                };
            }
            else if (valueFunc != null && targetFunc == null && accessorFunc != null)
            {
                return () =>
                {
                    object val = valueFunc();
                    ((IList)(_targetObject))[(int)accessorFunc()] = val;
                    return (T)val;
                };
            }
            else if (valueFunc != null && targetFunc != null && accessorFunc == null)
            {
                return () =>
                {
                    object val = valueFunc();
                    ((IList)(targetFunc()))[(int)_accessor] = val;
                    return (T)val;
                };
            }
            else
            {
                return () =>
                {
                    object val = valueFunc();
                    ((IList)(targetFunc()))[(int)accessorFunc()] = val;
                    return (T)val;
                };
            }
        }
    }
}