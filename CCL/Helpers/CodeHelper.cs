using System;
using System.Collections.Generic;
using System.Reflection;

namespace CCL
{
    public class CodeHelper
    {
        public Dictionary<string, System.Type> _variables;
        private static string[] emptyList = new string[0];
        private Core.Assembly _assembly;
        private string _code;
        private System.Type _contextType;

        public CodeHelper()
        {
            _variables = new Dictionary<string, System.Type>();
        }

        public Core.Assembly assembly
        {
            get
            {
                if (_assembly == null) _assembly = Core.Assembly.main;
                return _assembly;
            }
            set
            {
                _assembly = value;
            }
        }

        public string code
        {
            get
            {
                return _code;
            }

            set
            {
                _code = value;
            }
        }

        public Type contextType
        {
            get
            {
                return _contextType;
            }

            set
            {
                _contextType = value;
            }
        }

        public void FindVariables(string code)
        {
            _variables.Clear();
            _code = code;
            int index = 0;

            while (!IsEOF(index))
            {
                bool isArray = false;

                CullOther(ref index);
                string ident1 = ParseIdentifier(ref index);

                if (assembly.IsTypeName(ident1))
                {
                    CullWhitespace(ref index);
                    if (IsEOF(index)) return;
                    if (_code[index] == '.' || _code[index] == ')')
                    {
                        continue;
                    }
                    if (_code[index] == '[')
                    {
                        isArray = true;
                        CullOther(ref index);
                    }

                    string ident2 = ParseIdentifier(ref index);
                    if (ident2 == "") continue;
                    Core.TypeDef typeDef = assembly.GetTypeDef(ident1);
                    System.Type t = (isArray) ? Array.CreateInstance(typeDef.type, 0).GetType() : typeDef.type;

                    if (!IsEOF(index) && _code[index] == '(')
                    {
                        t = typeof(Func<>).MakeGenericType(t);
                    }

                    _variables.Add(ident2, t);

                    CullStatement(ref index);
                }
            }
        }

        public string[] ParseIdentifierChain(string code)
        {
            _code = code;
            int index = 0;
            List<string> output = new List<string>();

            while (!IsEOF(index))
            {
                string ident = ParseIdentifier(ref index, true);
                output.Add(ident);
                CullWhitespace(ref index);
                if (IsEOF(index)) break;
                if (_code[index] != '.') throw new Exception("Unexpected char in identifier chain: " + _code[index]);
                index++;
            }
            if (_code[_code.Length - 1] == '.')
            {
                output.Add("");
            }

            return output.ToArray();
        }

        public string[] Predict(string input)
        {
            if (_contextType == null) throw new Exception("Code Helper does not have a contexttype!");
            if (input == null || input == "") return emptyList;

            if (CheckForNumeric(input)) return emptyList;

            string[] elements = ParseIdentifierChain(input);
            List<string> output = new List<string>();
            if (elements.Length == 1)
            {
                //UnityEngine.Debug.Log("Elem 0 " + elements[0]);
                PredictTypeFromAssembly(elements[0], output);
                PredictVariableName(elements[0], output);
                PredictMember(elements[0], contextType, output);
                PredictStaticMember(elements[0], contextType, output);
                return output.ToArray();
            }
            else
            {
                IdentifierHit parentType = GetType(elements, elements.Length - 1);
                if (parentType.isStatic)
                {
                    PredictStaticMember(elements[elements.Length - 1], parentType.type, output);
                }
                else
                {
                    PredictMember(elements[elements.Length - 1], parentType.type, output);
                }
            }

            return output.ToArray();
        }

        public bool PredictMember(string input, System.Type type, List<string> list)
        {
            MemberInfo[] members = type.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

            for (int i = 0; i < members.Length; i += 1)
            {
                string memberName = members[i].Name;
                if (memberName == "GetType" ||
                    memberName.StartsWith("op_") ||
                    memberName.StartsWith("get_") ||
                    memberName.StartsWith("set_") ||
                    memberName.StartsWith(".")) continue;
                if (input == "" || memberName.StartsWith(input))
                {
                    list.Add(memberName);
                }
            }
            return members.Length > 0;
        }

        public bool PredictStaticMember(string input, System.Type type, List<string> list)
        {
            MemberInfo[] members = type.GetMembers(BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly);

            for (int i = 0; i < members.Length; i += 1)
            {
                string memberName = members[i].Name;
                if (memberName == "GetType" ||
                    memberName.StartsWith("op_") ||
                    memberName.StartsWith("get_") ||
                    memberName.StartsWith("set_") ||
                    memberName.StartsWith(".")) continue;
                if (input == "" || memberName.StartsWith(input))
                {
                    list.Add(memberName);
                }
            }
            return members.Length > 0;
        }

        public bool PredictTypeFromAssembly(string input, List<string> list)
        {
            string[] typenames = assembly.GetTypeNames();
            bool foundMatch = false;
            for (int i = 0; i < typenames.Length; i += 1)
            {
                if (typenames[i].StartsWith(input))
                {
                    list.Add(typenames[i]);
                    foundMatch = true;
                }
            }
            return foundMatch;
        }

        public bool PredictVariableName(string input, List<string> list)
        {
            var varnames = _variables.Keys;
            bool foundMatch = false;
            foreach (var varname in varnames)
            {
                if (varname.StartsWith(input))
                {
                    list.Add(varname);
                    foundMatch = true;
                }
            }
            return foundMatch;
        }

        private static Type GetMemberDataType(Type classType, string identifier)
        {
            MemberInfo[] members = classType.GetMember(identifier);
            if (members.Length < 1) return null;

            switch (members[0].MemberType)
            {
                case MemberTypes.Property:
                    return ((PropertyInfo)members[0]).PropertyType;

                case MemberTypes.Field:
                    return ((FieldInfo)members[0]).FieldType;

                case MemberTypes.Method:
                    return Core.DelegateUtility.GetMethodType(members[0] as MethodInfo);

                default:
                    return typeof(object);
            }
        }

        private bool CheckForNumeric(string input)
        {
            int intVal = 0;
            if (int.TryParse(input, out intVal))
            {
                return true;
            }
            float floatVal = 0;
            if (float.TryParse(input, out floatVal))
            {
                return true;
            }

            return false;
        }

        private void CullOther(ref int index)
        {
            while (!IsEOF(index) && !IsIdentiferChar(_code[index]))
            {
                index++;
            }
        }

        private string CullParenthesis(ref int index)
        {
            if (IsEOF(index) || _code[index] != '(') return "";
            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            while (!IsEOF(index) && _code[index] != ')')
            {
                builder.Append(_code[index]);
                index++;
            }
            index++;
            return builder.ToString();
        }

        private string CullSquareBrackets(ref int index)
        {
            if (IsEOF(index) || _code[index] != '[') return "";
            System.Text.StringBuilder builder = new System.Text.StringBuilder();

            while (!IsEOF(index) && _code[index] != ']')
            {
                builder.Append(_code[index]);
                index++;
            }
            index++;
            return builder.ToString();
        }

        private void CullStatement(ref int index)
        {
            while (!IsEOF(index) && _code[index] != ';')
            {
                index++;
            }
        }

        private void CullWhitespace(ref int index)
        {
            while (!IsEOF(index) && char.IsWhiteSpace(_code[index]))
            {
                index++;
            }
        }

        private MemberInfo GetMember(System.Type type, string identifier)
        {
            MemberInfo[] members = type.GetMembers();
            for (int i = 0; i < members.Length; i += 1)
            {
                if (members[i].Name == identifier) return members[i];
            }
            return null;
        }

        private IdentifierHit GetType(string identifier, System.Type parent, bool allowVariables = false)
        {
            int arrayAccessIndex = identifier.IndexOf('[');
            if (arrayAccessIndex > -1)
            {
                identifier = identifier.Replace("[]", "");
            }
            int functionCallIndex = identifier.IndexOf('(');
            if (functionCallIndex > -1)
            {
                identifier = identifier.Replace("()", "");
            }
            IdentifierHit hit = new IdentifierHit() { name = identifier, isStatic = false };
            System.Type type = null;
            if (assembly.IsTypeName(identifier))
            {
                // Cannot call or access a static type name
                if (arrayAccessIndex > -1 || functionCallIndex > -1) return null;
                hit.type = assembly.GetTypeDef(identifier).type;
                hit.isStatic = true;
            }
            else if ((type = GetMemberDataType(parent, identifier)) != null)
            {
                if (functionCallIndex > -1)
                {
                    type = Core.CompilerUtility.GetReturnType(type);
                }
                if (arrayAccessIndex > -1)
                {
                    type = type.GetElementType();
                }
                hit.type = type;
            }
            else if (allowVariables && _variables.TryGetValue(identifier, out type))
            {
                if (functionCallIndex > -1)
                {
                    type = Core.CompilerUtility.GetReturnType(type);
                }
                if (arrayAccessIndex > -1)
                {
                    type = type.GetElementType();
                }
                hit.type = type;
            }
            return hit;
        }

        private IdentifierHit GetType(string[] elements, int count)
        {
            int i = 1;
            IdentifierHit currentType = GetType(elements[0], contextType, true);

            while (i < elements.Length && i < count)
            {
                if (currentType == null || currentType.type == null) throw new System.Exception("Current Type is null, cannot trace path");
                currentType = GetType(elements[i], currentType.type);
                i++;
            }
            return currentType;
        }

        private bool IsEOF(int index)
        {
            return index >= _code.Length;
        }

        private bool IsIdentiferChar(char input)
        {
            return char.IsLetterOrDigit(input) || input == '_';
        }

        private string ParseIdentifier(ref int index, bool allowCalls = false)
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            while (!IsEOF(index) && IsIdentiferChar(_code[index]))
            {
                builder.Append(_code[index]);
                index++;
            }
            if (allowCalls)
            {
                CullWhitespace(ref index);
                if (CullParenthesis(ref index) != "")
                {
                    builder.Append("()");
                }
                CullWhitespace(ref index);
                if (CullSquareBrackets(ref index) != "")
                {
                    builder.Append("[]");
                }
            }
            return builder.ToString();
        }

        private char Peek(int index)
        {
            return _code[index];
        }

        private class IdentifierHit
        {
            public bool isStatic;
            public string name;
            public System.Type type;
        }
    }
}