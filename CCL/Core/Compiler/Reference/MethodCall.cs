using System;
using System.Collections.Generic;
using System.Reflection;

namespace CCL.Core
{
    internal class MethodCall : RunTimeReference
    {
        private object[] _argBuffer;
        private object[] _argNodeBuffer;
        private CompilerData _cdata;
        private MethodInfo _method;
        private Type _returnType;

        public MethodCall(Type t, string identifier, object target, object[] argumentNodes, CompilerData cdata, Type targetType) : base(t, identifier, target, targetType)
        {
            _returnType = (t != null) ? CompilerUtility.GetReturnType(t) : typeof(object);
            _cdata = cdata;
            _argNodeBuffer = (argumentNodes != null) ? argumentNodes : new object[0];
            //_method = targetType.GetMethod(identifier);
            _argBuffer = new object[_argNodeBuffer.Length];

            if (!ValidateArguments()) throw new Exception("Arguments suppied do not match the spefied signaure: " + _identifer + _type.ToString());
        }

        public MethodCall(MethodInfo methodInfo, object target, object[] argumentNodes, CompilerData cdata) : base(DelegateUtility.GetMethodType(methodInfo), methodInfo.Name, target, methodInfo.DeclaringType)
        {
            _method = methodInfo;
            _returnType = CompilerUtility.GetReturnType(DelegateUtility.GetMethodType(methodInfo));
            _cdata = cdata;
            _argNodeBuffer = (argumentNodes != null) ? argumentNodes : new object[0];
            _argBuffer = new object[_argNodeBuffer.Length];

            if (!ValidateArguments()) throw new Exception("Arguments suppied do not match the spefied signaure: " + _identifer + _type.ToString());
        }

        public object[] argBuffer
        {
            get { return _argBuffer; }
            set
            {
                _argBuffer = value;
            }
        }

        public object[] argNodeBuffer
        {
            get { return _argNodeBuffer; }
            set
            {
                _argNodeBuffer = value;
            }
        }

        public override Type type
        {
            get
            {
                return _returnType;
            }
        }

        public override Func<T> CreateGetFunction<T>(CompilerData cdata)
        {
            if (CompilerUtility.IsFunc(_targetObject))
            {
                Func<object> targetFunc = _targetObject as Func<object>;
                return (Func<T>)(() =>
                {
                    ResolveArguments(_argNodeBuffer, ref _argBuffer, cdata);
                    object targ = targetFunc();
                    if (targ.GetType() == typeof(System.Type)) throw new Exception("Methods in the type System.Type cannot be accessed");
                    return (T)_cdata._delegateCache.CallMethod(targetFunc(), _identifer, _argBuffer);
                });
            }
            else
            {
                return (Func<T>)(() =>
                {
                    ResolveArguments(_argNodeBuffer, ref _argBuffer, cdata);
                    return (T)_cdata._delegateCache.CallMethod(_targetObject, _identifer, _argBuffer);
                });
            }
        }

        private MethodInfo FindMethod(Type targetType, string identifier, object[] argumentNodes)
        {
            List<Type> typeList = new List<Type>();
            for (int i = 0; i < argumentNodes.Length; i += 1)
            {
                typeList.Add(CompilerUtility.GetReturnType(argumentNodes[i]));
            }
            return ReflectionUtility.FindMethod(targetType, identifier, typeList.ToArray());
        }

        private void ResolveArguments(object[] nodes, ref object[] buffer, CompilerData cdata)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                Func<object> func = nodes[i] as Func<object>;
                buffer[i] = (func != null) ? func() : nodes[i];
            }
        }

        private bool ValidateArguments()
        {
            return true;
        }
    }
}