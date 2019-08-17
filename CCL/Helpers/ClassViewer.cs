using System;
using System.Collections.Generic;
using System.Reflection;

namespace CCL
{
    public class ClassViewer
    {
        public static MemberView[] ReadClass(Type type)
        {
            MemberInfo[] memberInfoList = type.GetMembers();
            List<MemberView> views = new List<MemberView>();

            for (int i = 0; i < memberInfoList.Length; i++)
            {
                MemberView view = MemberView.Create(memberInfoList[i]);
                if (view != null)
                {
                    views.Add(view);
                }
            }

            return views.ToArray();
        }

        public static MemberView[] ReadClass(Type type, string search)
        {
            MemberInfo[] memberInfoList = type.GetMembers();
            List<MemberView> views = new List<MemberView>();

            for (int i = 0; i < memberInfoList.Length; i++)
            {
                if (!memberInfoList[i].Name.StartsWith(search)) continue;
                MemberView view = MemberView.Create(memberInfoList[i]);
                if (view != null)
                {
                    views.Add(view);
                }
            }

            return views.ToArray();
        }

        public class FieldView : MemberView
        {
            public FieldView(MemberInfo info) : base(info)
            {
                FieldInfo field = (FieldInfo)info;
                returnType = field.FieldType;
            }
        }

        public abstract class MemberView
        {
            public MemberTypes memberType;
            public string name;
            public Type returnType;

            public MemberView(MemberInfo info)
            {
                name = info.Name;
                memberType = info.MemberType;
            }

            public static MemberView Create(MemberInfo info)
            {
                switch (info.MemberType)
                {
                    case MemberTypes.Field:
                        return new FieldView(info);

                    case MemberTypes.Property:
                        return new PropertyView(info);

                    case MemberTypes.Method:
                        return new MethodView(info);

                    default:
                        return null;
                }
            }

            public override string ToString()
            {
                return returnType.Name + " " + name;
            }
        }

        public class MethodView : MemberView
        {
            public ParameterInfo[] arguments;

            public MethodView(MemberInfo info) : base(info)
            {
                MethodInfo method = (MethodInfo)info;

                returnType = method.ReturnType;
                arguments = method.GetParameters();
            }

            public override string ToString()
            {
                string argstr = " (";

                for (int i = 0; i < arguments.Length; i++)
                {
                    if (i > 0) argstr += ", ";
                    argstr += arguments[i].ParameterType.Name + " " + arguments[i].Name;
                }

                argstr += ")";

                return base.ToString() + argstr;
            }
        }

        public class PropertyView : MemberView
        {
            public string accessors = "{ ";

            public PropertyView(MemberInfo info) : base(info)
            {
                PropertyInfo prop = (PropertyInfo)info;
                returnType = prop.PropertyType;

                if (prop.CanRead) accessors += "get;";
                if (prop.CanWrite) accessors += "set;";

                accessors += " }";
            }

            public override string ToString()
            {
                return base.ToString() + " " + accessors;
            }
        }
    }
}