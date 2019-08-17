using System;
using System.Collections;

namespace CCL.Core
{
    internal class RunTimeArrayAccessReferenceListAccess : RunTimeArrayAccessReference
    {
        private Type _elemType;

        public RunTimeArrayAccessReferenceListAccess(Type t, string identifer, object target, object accessor, Type targetType) : base(t, identifer, target, accessor, targetType)
        {
            _accessor = accessor;
            _type = t;
            _targetType = targetType;
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
            Func<object> accessorFunc = CompilerUtility.ForceGetFunction(_accessor, cdata);
            Func<object> targetFunc = (CompilerUtility.IsFunc(_targetObject)) ? (Func<object>)_targetObject : null;

            if (targetFunc == null && accessorFunc == null)
            {
                IList list = _accessor as IList;
                int accessorCount = list.Count;
                return () =>
                {
                    Type et = (_elemType != typeof(object)) ? _elemType : _targetObject.GetType().GetElementType();
                    IList arr = System.Array.CreateInstance(et, accessorCount);
                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        arr[i] = ((IList)_targetObject)[(int)list[i]];
                    }
                    return (T)arr;
                };
            }
            else if (targetFunc == null && accessorFunc != null)
            {
                return () =>
                {
                    IList list = accessorFunc() as IList;
                    int accessorCount = list.Count;
                    Type et = (_elemType != typeof(object)) ? _elemType : _targetObject.GetType().GetElementType();
                    IList arr = System.Array.CreateInstance(et, accessorCount);
                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        arr[i] = ((IList)_targetObject)[(int)list[i]];
                    }
                    return (T)arr;
                };
            }
            else if (targetFunc != null && accessorFunc == null)
            {
                IList list = _accessor as IList;
                int accessorCount = list.Count;
                return () =>
                {
                    object targetObject = targetFunc();
                    Type et = (_elemType != typeof(object)) ? _elemType : targetObject.GetType().GetElementType();
                    IList arr = System.Array.CreateInstance(et, accessorCount);
                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        arr[i] = ((IList)targetObject)[(int)list[i]];
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
                    object targetObject = targetFunc();
                    Type et = (_elemType != typeof(object)) ? _elemType : targetObject.GetType().GetElementType();
                    IList arr = System.Array.CreateInstance(et, accessorCount);
                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        arr[i] = ((IList)targetObject)[(int)list[i]];
                    }
                    return (T)arr;
                };
            }
        }

        public override Func<T> CreateModifyFunction<T>(object value, CompilerData cdata, Token.Type operation)
        {
            throw new System.InvalidOperationException("Cannot modify array indexed results");
        }

        public override Func<T> CreateSetFunction<T>(object value, CompilerData cdata)
        {
            Func<object> accessorFunc = CompilerUtility.ForceGetFunction(_accessor, cdata);
            Func<object> targetFunc = CompilerUtility.ForceGetFunction(_targetObject, cdata);
            Func<object> valueFunc = CompilerUtility.ForceGetFunction(value, cdata);

            if (valueFunc == null && targetFunc == null && accessorFunc == null)
            {
                IList list = _accessor as IList;
                int accessorCount = list.Count;
                return () =>
                {
                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        ((IList)_targetObject)[(int)list[i]] = value;
                    }
                    return (T)value;
                };
            }
            else if (valueFunc == null && targetFunc == null && accessorFunc != null)
            {
                return () =>
                {
                    IList list = accessorFunc() as IList;
                    int accessorCount = list.Count;
                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        ((IList)_targetObject)[(int)list[i]] = value;
                    }
                    return (T)value;
                };
            }
            else if (valueFunc == null && targetFunc != null && accessorFunc == null)
            {
                IList list = _accessor as IList;
                int accessorCount = list.Count;
                return () =>
                {
                    IList targetObject = (IList)targetFunc();
                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        targetObject[(int)list[i]] = value;
                    }
                    return (T)value;
                };
            }
            else if (valueFunc == null && targetFunc != null && accessorFunc != null)
            {
                return () =>
                {
                    IList targetObject = (IList)targetFunc();
                    IList list = accessorFunc() as IList;
                    int accessorCount = list.Count;
                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        targetObject[(int)list[i]] = value;
                    }
                    return (T)value;
                };
            }
            else if (valueFunc != null && targetFunc == null && accessorFunc == null)
            {
                IList targetObject = (IList)targetFunc();
                IList list = _accessor as IList;
                int accessorCount = list.Count;
                return () =>
                {
                    T val = (T)valueFunc();
                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        targetObject[(int)list[i]] = val;
                    }
                    return (T)val;
                };
            }
            else if (valueFunc != null && targetFunc == null && accessorFunc != null)
            {
                return () =>
                {
                    T val = (T)valueFunc();
                    IList list = accessorFunc() as IList;
                    int accessorCount = list.Count;
                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        ((IList)_targetObject)[(int)list[i]] = val;
                    }
                    return (T)val;
                };
            }
            else if (valueFunc != null && targetFunc != null && accessorFunc == null)
            {
                IList targetObject = (IList)targetFunc();
                IList list = _accessor as IList;
                int accessorCount = list.Count;
                return () =>
                {
                    T val = (T)valueFunc();
                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        targetObject[(int)list[i]] = val;
                    }
                    return (T)val;
                };
            }
            else
            {
                return () =>
                {
                    IList targetObject = (IList)targetFunc();
                    T val = (T)valueFunc();
                    IList list = accessorFunc() as IList;
                    int accessorCount = list.Count;
                    for (int i = 0; i < accessorCount; i += 1)
                    {
                        targetObject[(int)list[i]] = val;
                    }
                    return (T)val;
                };
            }
        }
    }
}