using System;

namespace CCL.Core
{
    internal class RunTimeReference : Reference
    {
        protected object _targetObject;
        protected Type _targetType;

        public RunTimeReference(Type t, string identifier, object target, Type targetType) : base(t, identifier)
        {
            _targetObject = target;
            _targetType = targetType;
        }

        public object targetObject
        {
            get { return _targetObject; }
        }

        public Type targetType
        {
            get
            {
                return _targetType;
            }
        }

        public override Func<T> CreateGetFunction<T>(CompilerData cdata)
        {
            //Func<object> targetFunc = CompilerUtility.ForceGetFunction(_targetObject, cdata);
            Func<object> targetFunc = (Func<object>)_targetObject;

            if (targetFunc != null)
            {
                return () =>
                {
                    return (T)RunTimeUtility.GetMemberValue(targetFunc(), _identifer, cdata);
                };
            }
            else
            {
                return () =>
                {
                    return (T)RunTimeUtility.GetMemberValue(_targetObject, _identifer, cdata);
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

            object func = del(this, value, cdata);
            Type returnType = CompilerUtility.GetReturnType(func);
            TypeDef returnTypeDef = cdata._assembly.GetTypeDef(returnType);

            if (targetFunc == null)
            {
                return () =>
                {
                    object ob = returnTypeDef.CallFunction(func);
                    return (T)RunTimeUtility.SetMemberValue(_targetObject, _identifer, cdata, ob);
                };
            }
            else
            {
                return () =>
                {
                    object ob = returnTypeDef.CallFunction(func);
                    return (T)RunTimeUtility.SetMemberValue(targetFunc(), _identifer, cdata, ob);
                };
            }
        }

        public override Func<T> CreateSetFunction<T>(object value, CompilerData cdata)
        {
            Func<object> targetFunc = CompilerUtility.ForceGetFunction<object>(_targetObject, cdata);
            Func<T> valueFunc = CompilerUtility.ForceGetFunction<T>(value, cdata);

            if (targetFunc == null && valueFunc == null)
            {
                T val = (T)value;
                return () =>
                {
                    return (T)RunTimeUtility.SetMemberValue(_targetObject, _identifer, cdata, val);
                };
            }
            else if (targetFunc == null && valueFunc != null)
            {
                return () =>
                {
                    return (T)RunTimeUtility.SetMemberValue(_targetObject, _identifer, cdata, valueFunc());
                };
            }
            else if (targetFunc != null && valueFunc == null)
            {
                T val = (T)value;
                return () =>
                {
                    return (T)RunTimeUtility.SetMemberValue(targetFunc(), _identifer, cdata, val);
                };
            }
            else
            {
                return () =>
                {
                    return (T)RunTimeUtility.SetMemberValue(targetFunc(), _identifer, cdata, valueFunc());
                };
            }
        }

        /*
        protected override Func<T> CreateModifyFunction_Add<T>(object value, CompilerData cdata)
        {
            Func<object> targetFunc = CompilerUtility.ForceGetFunction<object>(_targetObject, cdata);
            Func<T> valueFunc = CompilerUtility.ForceGetFunction<T>(value, cdata);

            if (targetFunc == null && valueFunc == null)
            {
                T val = (T)value;
                return () =>
                {
                    return (T)RunTimeUtility.AddToMemberValue(_targetObject, _identifer, cdata, val);
                };
            }
            else if (targetFunc == null && valueFunc != null)
            {
                return () =>
                {
                    return (T)RunTimeUtility.AddToMemberValue(_targetObject, _identifer, cdata, valueFunc());
                };
            }
            else if (targetFunc != null && valueFunc == null)
            {
                T val = (T)value;
                return () =>
                {
                    return (T)RunTimeUtility.AddToMemberValue(targetFunc(), _identifer, cdata, val);
                };
            }
            else
            {
                return () =>
                {
                    return (T)RunTimeUtility.AddToMemberValue(targetFunc(), _identifer, cdata, valueFunc());
                };
            }
        }

        protected override Func<T> CreateModifyFunction_Subtract<T>(object value, CompilerData cdata)
        {
            Func<object> targetFunc = CompilerUtility.ForceGetFunction<object>(_targetObject, cdata);
            Func<T> valueFunc = CompilerUtility.ForceGetFunction<T>(value, cdata);

            if (targetFunc == null && valueFunc == null)
            {
                T val = (T)value;
                return () =>
                {
                    return (T)RunTimeUtility.SubtractFromMemberValue(_targetObject, _identifer, cdata, val);
                };
            }
            else if (targetFunc == null && valueFunc != null)
            {
                return () =>
                {
                    return (T)RunTimeUtility.SubtractFromMemberValue(_targetObject, _identifer, cdata, valueFunc());
                };
            }
            else if (targetFunc != null && valueFunc == null)
            {
                T val = (T)value;
                return () =>
                {
                    return (T)RunTimeUtility.SubtractFromMemberValue(targetFunc(), _identifer, cdata, val);
                };
            }
            else
            {
                return () =>
                {
                    return (T)RunTimeUtility.SubtractFromMemberValue(targetFunc(), _identifer, cdata, valueFunc());
                };
            }
        }

        protected override Func<T> CreateModifyFunction_Multiply<T>(object value, CompilerData cdata)
        {
            Func<object> targetFunc = CompilerUtility.ForceGetFunction<object>(_targetObject, cdata);
            Func<T> valueFunc = CompilerUtility.ForceGetFunction<T>(value, cdata);

            if (targetFunc == null && valueFunc == null)
            {
                T val = (T)value;
                return () =>
                {
                    return (T)RunTimeUtility.MultiplyMemberValueBy(_targetObject, _identifer, cdata, val);
                };
            }
            else if (targetFunc == null && valueFunc != null)
            {
                return () =>
                {
                    return (T)RunTimeUtility.MultiplyMemberValueBy(_targetObject, _identifer, cdata, valueFunc());
                };
            }
            else if (targetFunc != null && valueFunc == null)
            {
                T val = (T)value;
                return () =>
                {
                    return (T)RunTimeUtility.MultiplyMemberValueBy(targetFunc(), _identifer, cdata, val);
                };
            }
            else
            {
                return () =>
                {
                    return (T)RunTimeUtility.MultiplyMemberValueBy(targetFunc(), _identifer, cdata, valueFunc());
                };
            }
        }

        protected override Func<T> CreateModifyFunction_Divide<T>(object value, CompilerData cdata)
        {
            Func<object> targetFunc = CompilerUtility.ForceGetFunction<object>(_targetObject, cdata);
            Func<T> valueFunc = CompilerUtility.ForceGetFunction<T>(value, cdata);

            if (targetFunc == null && valueFunc == null)
            {
                T val = (T)value;
                return () =>
                {
                    return (T)RunTimeUtility.DivideMemberValueBy(_targetObject, _identifer, cdata, val);
                };
            }
            else if (targetFunc == null && valueFunc != null)
            {
                return () =>
                {
                    return (T)RunTimeUtility.DivideMemberValueBy(_targetObject, _identifer, cdata, valueFunc());
                };
            }
            else if (targetFunc != null && valueFunc == null)
            {
                T val = (T)value;
                return () =>
                {
                    return (T)RunTimeUtility.DivideMemberValueBy(targetFunc(), _identifer, cdata, val);
                };
            }
            else
            {
                return () =>
                {
                    return (T)RunTimeUtility.DivideMemberValueBy(targetFunc(), _identifer, cdata, valueFunc());
                };
            }
        }
        */
    }
}