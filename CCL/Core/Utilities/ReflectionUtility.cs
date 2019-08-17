using System;
using System.Reflection;

namespace CCL.Core
{
    public class ReflectionUtility
    {
        protected static BindingFlags memberAccessBinding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static;

        public static MethodInfo FindMethod(Type targetType, string identifer, Type[] arguments)
        {
            MemberInfo[] members = targetType.GetMember(identifer, memberAccessBinding);
            if (members.Length < 1) return null;
            if (members.Length == 1) return (MethodInfo)members[0];
            if (members[0].MemberType != MemberTypes.Method) throw new CompilationException("There are no members on class " + targetType.Name + " with name " + identifer);

            for (int i = 0; i < members.Length; i += 1)
            {
                ParameterInfo[] parameters = ((MethodInfo)members[i]).GetParameters();
                int j = 0;
                bool isMatch = true;

                if (arguments.Length == 0 && parameters.Length == 0)
                {
                    return (MethodInfo)members[i];
                }
                while (j < parameters.Length)
                {
                    if (j < arguments.Length)
                    {
                        if (!(parameters[j].ParameterType == arguments[j] || arguments[j].IsSubclassOf(parameters[j].ParameterType)))
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    else
                    {
                        if (!parameters[i].HasDefaultValue)
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    j += 1;
                }
                if (isMatch)
                {
                    return (MethodInfo)members[i];
                }
            }
            return null;
        }

        public static MethodInfo FindMethod(Type targetType, string identifer, object[] arguments)
        {
            MemberInfo[] members = targetType.GetMember(identifer);
            if (members.Length < 1) return null;
            if (members.Length == 1) return (MethodInfo)members[0];
            if (members[0].MemberType != MemberTypes.Method) throw new CompilationException("There are no members on class " + targetType.Name + " with name " + identifer);

            for (int i = 0; i < members.Length; i += 1)
            {
                ParameterInfo[] parameters = ((MethodInfo)members[i]).GetParameters();
                int j = 0;
                bool isMatch = true;

                if (arguments.Length == 0 && parameters.Length == 0)
                {
                    return (MethodInfo)members[i];
                }
                while (j < parameters.Length)
                {
                    UnityEngine.Debug.Log(parameters[j].ParameterType.Name + " == " + arguments[j].GetType().Name);
                    if (j < arguments.Length)
                    {
                        if (!(parameters[j].ParameterType == arguments[j].GetType() || arguments[j].GetType().IsSubclassOf(parameters[j].ParameterType)))
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    else
                    {
                        if (!parameters[i].HasDefaultValue)
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    j += 1;
                }
                if (isMatch)
                {
                    return (MethodInfo)members[i];
                }
            }
            return null;
        }

        public static Type GetMemberDataType(Type classType, string identifier)
        {
            MemberInfo[] members = classType.GetMember(identifier);
            if (members.Length < 1) throw new CompilationException("There is no member " + identifier + " on type " + classType.Name);

            switch (members[0].MemberType)
            {
                case MemberTypes.Property:
                    return ((PropertyInfo)members[0]).PropertyType;

                case MemberTypes.Field:
                    return ((FieldInfo)members[0]).FieldType;

                case MemberTypes.Method:
                    return DelegateUtility.GetMethodType(members[0] as MethodInfo);

                default:
                    return typeof(object);
            }
        }

        public static string PrintMember(MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Property:
                    return GetStringFromProperty((PropertyInfo)member);

                case MemberTypes.Field:
                    return GetStringFromField((FieldInfo)member);

                case MemberTypes.Method:
                    return GetStringFromMethod(member as MethodInfo);

                default:
                    return "";
            }
        }

        public static string PrintMembers(Type type, BindingFlags accessFlags = BindingFlags.Public | BindingFlags.Instance)
        {
            MemberInfo[] members = type.GetMembers(accessFlags);
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            for (int i = 0; i < members.Length; i += 1)
            {
                MemberInfo member = members[i];
                str.AppendLine(PrintMember(member));
            }
            string output = str.ToString();
            return output != "" ? output : type.ToString() + " (no data)";
        }

        public static string PrintMembers(object ob)
        {
            if (ob == null) return "null";
            if (ob as Type != null) return PrintMembers((Type)ob, BindingFlags.Public | BindingFlags.Static);
            if (ob as MethodInfo != null) return GetStringFromMethod((MethodInfo)ob);
            else return PrintMembers(ob.GetType());
        }

        private static string GetStringFromField(FieldInfo field)
        {
            return field.FieldType.Name + " " + field.Name;
        }

        private static string GetStringFromMethod(MethodInfo method)
        {
            ParameterInfo[] args = method.GetParameters();
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("(");
            for (int i = 0; i < args.Length; i += 1)
            {
                if (i > 0) builder.Append(", ");
                builder.Append(args[i].ParameterType.Name + " " + args[i].Name);
            }

            builder.Append(")");
            string modifiers = "";
            if (method.IsStatic) modifiers += "static ";
            return modifiers + method.ReturnType.Name + " " + method.Name + " " + builder;
        }

        private static string GetStringFromProperty(PropertyInfo prop)
        {
            string propMeta = "{";
            if (prop.GetMethod != null) propMeta += "get; ";
            if (prop.SetMethod != null) propMeta += "set; ";
            propMeta += "}";
            return prop.PropertyType.Name + " " + prop.Name + " " + propMeta;
        }
    }
}