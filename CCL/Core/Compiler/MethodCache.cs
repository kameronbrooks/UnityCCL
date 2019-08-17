using System;
using System.Collections.Generic;
using System.Reflection;

namespace CCL.Core
{
    public class MethodCache
    {
        private Dictionary<ulong, CachedMember> _map;

        public MethodCache()
        {
            _map = new Dictionary<ulong, CachedMember>();
        }

        public CachedMember CacheMember(Type type, string identifier)
        {
            MemberInfo[] memberInfo = type.GetMember(identifier);
            if (memberInfo.Length < 1) throw new RuntimeException("RunTime Exeption: The type " + type.Name + " does not contain the member " + identifier);

            CachedMember cached = new CachedMember(identifier, memberInfo);

            ulong hashCode = Hash(type, identifier);
            if (!_map.ContainsKey(hashCode))
            {
                _map.Add(hashCode, cached);
            }
            else
            {
                _map[hashCode] = cached;
            }
            return cached;
        }

        public CachedMember GetCached(Type type, string identifier)
        {
            CachedMember cached = null;
            ulong hashCode = Hash(type, identifier);
            _map.TryGetValue(hashCode, out cached);

            return cached;
        }

        private ulong Hash(Type t, string identifier)
        {
            return ((ulong)(uint)t.GetHashCode()) | (((ulong)(uint)identifier.GetHashCode()) << 32);
        }

        public class CachedMember
        {
            public readonly int count;
            public readonly MemberInfo[] memberInfo;
            public readonly MemberTypes memberType;
            public readonly string name;
            public MemberInfo main;

            public CachedMember(string name, MemberInfo[] memberInfo)
            {
                this.name = name;
                this.memberInfo = memberInfo;
                this.count = memberInfo.Length;
                if (count == 0) return;

                memberType = memberInfo[0].MemberType;
                main = memberInfo[0];
            }
        }
    }
}