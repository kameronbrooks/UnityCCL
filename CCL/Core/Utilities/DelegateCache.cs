using System;
using System.Collections.Generic;
using System.Reflection;

namespace CCL.Core
{
    public class DelegateCache
    {
        private readonly Type _objectType;
        private Assembly _assembly;
        private Dictionary<ulong, Delegate> _methodMap;

        public DelegateCache(Assembly assembly)
        {
            _assembly = assembly;
            _methodMap = new Dictionary<ulong, Delegate>();
            _objectType = typeof(Object);
        }

        public void Cache(object instance, string identifier, Delegate del)
        {
            ulong hash = Hash(instance, identifier);
            if (!_methodMap.ContainsKey(hash))
            {
                _methodMap.Add(hash, del);
            }
            _methodMap[hash] = del;
        }

        public void Cache(object instance, string identifier, Delegate del, object[] args)
        {
            ulong hash = Hash(instance, identifier);
            hash ^= GetArgTypeHash(args);
            if (!_methodMap.ContainsKey(hash))
            {
                _methodMap.Add(hash, del);
            }
            _methodMap[hash] = del;
        }

        public void Cache(string identifier, Delegate del)
        {
            ulong hash = (ulong)(uint)identifier.GetHashCode();
            if (!_methodMap.ContainsKey(hash))
            {
                _methodMap.Add(hash, del);
            }
            _methodMap[hash] = del;
        }

        public object CallLambda(string identifier, object lambda, object[] args)
        {
            Delegate del = GetMethod(identifier, args);
            if (del == null)
            {
                del = DelegateUtility.LambdaToDelegate(lambda);
                Cache(identifier, del);
            }

            return SmartCall(del, args);
        }

        public object CallMethod(object target, string method, object[] args)
        {
            Type t = target as Type;
            if (t == null) t = target.GetType();
            Delegate del = GetMethod(target, method, args);
            if (del == null)
            {
                //del = DelegateUtility.MethodToInstanceDelegate(target, t.GetMethod(method));
                MethodInfo info = ReflectionUtility.FindMethod(t, method, args);
                if (info.IsStatic)
                {
                    TypeDef typeDef = _assembly.GetTypeDef(t);
                    if (typeDef == null || !typeDef.allowStaticMembers)
                    {
                        throw new Exception("Static member access is not allowed on type " + t.Name);
                    }
                }
                del = DelegateUtility.MethodToInstanceDelegate(target, ReflectionUtility.FindMethod(t, method, args));
                Cache(target, method, del, args);
            }

            return SmartCall(del, args);
        }

        public object CallMethod(object target, MethodInfo method, object[] args)
        {
            Delegate del = GetMethod(target, method.Name, args);
            if (del == null)
            {
                del = DelegateUtility.MethodToInstanceDelegate(target, method);
                Cache(target, method.Name, del, args);
            }

            return SmartCall(del, args);
        }

        public Delegate GetMethod(object instance, string identifier, object[] args)
        {
            ulong hash = Hash(instance, identifier);
            hash ^= GetArgTypeHash(args);
            Delegate del = null;
            _methodMap.TryGetValue(hash, out del);
            return del;
        }

        public Delegate GetMethod(string identifier, object[] args)
        {
            ulong hash = (ulong)(uint)identifier.GetHashCode();
            hash ^= GetArgTypeHash(args);
            Delegate del = null;
            _methodMap.TryGetValue(hash, out del);
            return del;
        }

        public object SmartCall(Delegate del, object[] args)
        {
            DelegateSignature.Generator func = _assembly.GetDelegateSignature(del);
            if (func != null)
            {
                return func(del, args);
            }
            return del.Method.Invoke(del.Target, args);
        }

        private ulong GetArgTypeHash(object[] args)
        {
            ulong hash = 0;
            int l = args.Length;
            for (int i = 0; i < l; i += 1)
            {
                hash += (ulong)(uint)((args[i] != null ? args[i].GetHashCode() : _objectType.GetHashCode())) << i;
            }
            return hash;
        }

        private ulong Hash(object instance, string identifier)
        {
            return ((ulong)(uint)instance.GetHashCode()) | (((ulong)(uint)identifier.GetHashCode()) << 32);
        }
    }
}