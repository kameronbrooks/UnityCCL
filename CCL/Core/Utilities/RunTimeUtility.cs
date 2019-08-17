using System;
using System.Reflection;

namespace CCL.Core
{
    internal class RunTimeUtility
    {
        private static object[] _argBufferGet = new object[0];

        private static object[] _argBufferSet = new object[1];

        public static object AddToMemberValue(object target, string identifier, CompilerData cdata, object value)
        {
            Type t = target.GetType();
            MethodCache.CachedMember cached = cdata._methodCache.GetCached(t, identifier);
            if (cached == null)
            {
                cached = cdata._methodCache.CacheMember(t, identifier);
            }
            switch (cached.memberType)
            {
                case MemberTypes.Field:
                    FieldInfo field = (FieldInfo)cached.main;
                    if (field.FieldType == typeof(int))
                    {
                        int mod = ((int)field.GetValue(target) + (int)value);
                        field.SetValue(target, mod);
                        return mod;
                    }
                    else if (field.FieldType == typeof(float))
                    {
                        float mod = ((float)field.GetValue(target) + (float)value);
                        field.SetValue(target, mod);
                        return mod;
                    }
                    else if (field.FieldType == typeof(string))
                    {
                        string mod = ((string)field.GetValue(target) + (string)value);
                        field.SetValue(target, mod);
                        return mod;
                    }

                    throw new Exception("Runtime Error: Cannot cast");
                case MemberTypes.Property:
                    PropertyInfo prop = (PropertyInfo)cached.main;
                    if (prop.PropertyType == typeof(int))
                    {
                        int mod = ((int)prop.GetValue(target) + (int)value);
                        prop.SetValue(target, mod);
                        return mod;
                    }
                    else if (prop.PropertyType == typeof(float))
                    {
                        float mod = ((float)prop.GetValue(target) + (float)value);
                        prop.SetValue(target, mod);
                        return mod;
                    }
                    else if (prop.PropertyType == typeof(string))
                    {
                        string mod = ((string)prop.GetValue(target) + (string)value);
                        prop.SetValue(target, mod);
                        return mod;
                    }
                    throw new Exception("Runtime Error: Cannot cast");
                default:
                    throw new Exception("Runtime Error: No Setter for " + cached.name + " " + cached.memberType.ToString());
            }
        }

        public static object DecrementMemberValue(object target, string identifier, CompilerData cdata, object value)
        {
            Type t = target.GetType();
            MethodCache.CachedMember cached = cdata._methodCache.GetCached(t, identifier);
            if (cached == null)
            {
                cached = cdata._methodCache.CacheMember(t, identifier);
            }
            switch (cached.memberType)
            {
                case MemberTypes.Field:
                    FieldInfo field = (FieldInfo)cached.main;
                    if (field.FieldType == typeof(int))
                    {
                        int mod = (int)field.GetValue(target) - 1;
                        field.SetValue(target, mod);
                        return mod;
                    }
                    else if (field.FieldType == typeof(float))
                    {
                        float mod = (float)field.GetValue(target) - 1;
                        field.SetValue(target, mod);
                        return mod;
                    }

                    throw new Exception("Runtime Error: Cannot cast");
                case MemberTypes.Property:
                    PropertyInfo prop = (PropertyInfo)cached.main;
                    if (prop.PropertyType == typeof(int))
                    {
                        int mod = (int)prop.GetValue(target) - 1;
                        prop.SetValue(target, mod);
                        return mod;
                    }
                    else if (prop.PropertyType == typeof(float))
                    {
                        float mod = (float)prop.GetValue(target) - 1;
                        prop.SetValue(target, mod);
                        return mod;
                    }

                    throw new Exception("Runtime Error: Cannot cast");
                default:
                    throw new Exception("Runtime Error: No Setter for " + cached.name + " " + cached.memberType.ToString());
            }
        }

        public static object DivideMemberValueBy(object target, string identifier, CompilerData cdata, object value)
        {
            Type t = target.GetType();
            MethodCache.CachedMember cached = cdata._methodCache.GetCached(t, identifier);
            if (cached == null)
            {
                cached = cdata._methodCache.CacheMember(t, identifier);
            }
            switch (cached.memberType)
            {
                case MemberTypes.Field:
                    FieldInfo field = (FieldInfo)cached.main;
                    if (field.FieldType == typeof(int))
                    {
                        int mod = ((int)field.GetValue(target) / (int)value);
                        field.SetValue(target, mod);
                        return mod;
                    }
                    else if (field.FieldType == typeof(float))
                    {
                        float mod = ((float)field.GetValue(target) / (float)value);
                        field.SetValue(target, mod);
                        return mod;
                    }

                    throw new Exception("Runtime Error: Cannot cast");
                case MemberTypes.Property:
                    PropertyInfo prop = (PropertyInfo)cached.main;
                    if (prop.PropertyType == typeof(int))
                    {
                        int mod = ((int)prop.GetValue(target) / (int)value);
                        prop.SetValue(target, mod);
                        return mod;
                    }
                    else if (prop.PropertyType == typeof(float))
                    {
                        float mod = ((float)prop.GetValue(target) / (float)value);
                        prop.SetValue(target, mod);
                        return mod;
                    }

                    throw new Exception("Runtime Error: Cannot cast");
                default:
                    throw new Exception("Runtime Error: No Setter for " + cached.name + " " + cached.memberType.ToString());
            }
        }

        public static object GetMemberValue(object target, string identifier, CompilerData cdata)
        {
            Type t = ((target as Type) != null) ? (Type)target : target.GetType();
            MethodCache.CachedMember cached = cdata._methodCache.GetCached(t, identifier);
            if (cached == null)
            {
                cached = cdata._methodCache.CacheMember(t, identifier);
            }
            switch (cached.memberType)
            {
                case MemberTypes.Field:
                    return ((FieldInfo)cached.main).GetValue(target);

                case MemberTypes.Property:
                    MethodInfo method = ((PropertyInfo)cached.main).GetMethod;
                    if (method == null) throw new Exception("Property " + t.Name + "." + cached.name + " is writeonly");
                    //object ob = ((target as Type) != null) ? cdata._delegateCache.CallStaticMethod(t, method, _argBufferGet) : cdata._delegateCache.CallMethod(target, method, _argBufferGet);
                    object ob = cdata._delegateCache.CallMethod(target, method, _argBufferGet);
                    return ob;

                case MemberTypes.Method:
                    return ((MethodInfo)cached.main);

                default:
                    throw new Exception("Runtime Error: Unsupported Member Access " + cached.name + " " + cached.memberType.ToString());
            }
        }

        public static object IncrementMemberValue(object target, string identifier, CompilerData cdata, object value)
        {
            Type t = target.GetType();
            MethodCache.CachedMember cached = cdata._methodCache.GetCached(t, identifier);
            if (cached == null)
            {
                cached = cdata._methodCache.CacheMember(t, identifier);
            }
            switch (cached.memberType)
            {
                case MemberTypes.Field:
                    FieldInfo field = (FieldInfo)cached.main;
                    if (field.FieldType == typeof(int))
                    {
                        int mod = (int)field.GetValue(target) + 1;
                        field.SetValue(target, mod);
                        return mod;
                    }
                    else if (field.FieldType == typeof(float))
                    {
                        float mod = (float)field.GetValue(target) + 1;
                        field.SetValue(target, mod);
                        return mod;
                    }

                    throw new Exception("Runtime Error: Cannot cast");
                case MemberTypes.Property:
                    PropertyInfo prop = (PropertyInfo)cached.main;
                    if (prop.PropertyType == typeof(int))
                    {
                        int mod = (int)prop.GetValue(target) + 1;
                        prop.SetValue(target, mod);
                        return mod;
                    }
                    else if (prop.PropertyType == typeof(float))
                    {
                        float mod = (float)prop.GetValue(target) + 1;
                        prop.SetValue(target, mod);
                        return mod;
                    }

                    throw new Exception("Runtime Error: Cannot cast");
                default:
                    throw new Exception("Runtime Error: No Setter for " + cached.name + " " + cached.memberType.ToString());
            }
        }

        public static object MultiplyMemberValueBy(object target, string identifier, CompilerData cdata, object value)
        {
            Type t = target.GetType();
            MethodCache.CachedMember cached = cdata._methodCache.GetCached(t, identifier);
            if (cached == null)
            {
                cached = cdata._methodCache.CacheMember(t, identifier);
            }
            switch (cached.memberType)
            {
                case MemberTypes.Field:
                    FieldInfo field = (FieldInfo)cached.main;
                    if (field.FieldType == typeof(int))
                    {
                        int mod = ((int)field.GetValue(target) * (int)value);
                        field.SetValue(target, mod);
                        return mod;
                    }
                    else if (field.FieldType == typeof(float))
                    {
                        float mod = ((float)field.GetValue(target) * (float)value);
                        field.SetValue(target, mod);
                        return mod;
                    }

                    throw new Exception("Runtime Error: Cannot cast");
                case MemberTypes.Property:
                    PropertyInfo prop = (PropertyInfo)cached.main;
                    if (prop.PropertyType == typeof(int))
                    {
                        int mod = ((int)prop.GetValue(target) * (int)value);
                        prop.SetValue(target, mod);
                        return mod;
                    }
                    else if (prop.PropertyType == typeof(float))
                    {
                        float mod = ((float)prop.GetValue(target) * (float)value);
                        prop.SetValue(target, mod);
                        return mod;
                    }

                    throw new Exception("Runtime Error: Cannot cast");
                default:
                    throw new Exception("Runtime Error: No Setter for " + cached.name + " " + cached.memberType.ToString());
            }
        }

        public static object SetMemberValue(object target, string identifier, CompilerData cdata, object value)
        {
            Type t = target.GetType();
            MethodCache.CachedMember cached = cdata._methodCache.GetCached(t, identifier);
            if (cached == null)
            {
                cached = cdata._methodCache.CacheMember(t, identifier);
            }
            switch (cached.memberType)
            {
                case MemberTypes.Field:
                    ((FieldInfo)cached.main).SetValue(target, value);
                    return value;

                case MemberTypes.Property:
                    MethodInfo method = ((PropertyInfo)cached.main).SetMethod;
                    if (method == null) throw new Exception("Property " + t.Name + "." + cached.name + " is readonly");
                    _argBufferSet[0] = value;
                    cdata._delegateCache.CallMethod(target, method, _argBufferSet);
                    return value;

                default:
                    throw new Exception("Runtime Error: No Setter for " + cached.name + " " + cached.memberType.ToString());
            }
        }

        public static object SubtractFromMemberValue(object target, string identifier, CompilerData cdata, object value)
        {
            Type t = target.GetType();
            MethodCache.CachedMember cached = cdata._methodCache.GetCached(t, identifier);
            if (cached == null)
            {
                cached = cdata._methodCache.CacheMember(t, identifier);
            }
            switch (cached.memberType)
            {
                case MemberTypes.Field:
                    FieldInfo field = (FieldInfo)cached.main;
                    if (field.FieldType == typeof(int))
                    {
                        int mod = ((int)field.GetValue(target) - (int)value);
                        field.SetValue(target, mod);
                        return mod;
                    }
                    else if (field.FieldType == typeof(float))
                    {
                        float mod = ((float)field.GetValue(target) - (float)value);
                        field.SetValue(target, mod);
                        return mod;
                    }

                    throw new Exception("Runtime Error: Cannot cast");
                case MemberTypes.Property:
                    PropertyInfo prop = (PropertyInfo)cached.main;
                    if (prop.PropertyType == typeof(int))
                    {
                        int mod = ((int)prop.GetValue(target) - (int)value);
                        prop.SetValue(target, mod);
                        return mod;
                    }
                    else if (prop.PropertyType == typeof(float))
                    {
                        float mod = ((float)prop.GetValue(target) - (float)value);
                        prop.SetValue(target, mod);
                        return mod;
                    }

                    throw new Exception("Runtime Error: Cannot cast");
                default:
                    throw new Exception("Runtime Error: No Setter for " + cached.name + " " + cached.memberType.ToString());
            }
        }
    }
}