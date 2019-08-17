using System;
using System.Collections.Generic;
using System.Reflection;

namespace CCL.Core
{
    public class Assembly
    {
        private static Assembly _mainAssembly;

        private Dictionary<Token.Type, Dictionary<ulong, Operation.BinaryOperationDelegate>> _binaryOperators;

        private Dictionary<Type, DelegateSignature.Generator> _delegateSignatures;

        private List<Type> _loadedLibraries;

        private Dictionary<string, TypeDef> _typeMap;

        private Dictionary<Token.Type, Dictionary<ulong, Operation.UnaryOperationDelegate>> _unaryOperators;

        public Assembly()
        {
            _typeMap = new Dictionary<string, TypeDef>();
            _binaryOperators = new Dictionary<Token.Type, Dictionary<ulong, Operation.BinaryOperationDelegate>>();
            _unaryOperators = new Dictionary<Token.Type, Dictionary<ulong, Operation.UnaryOperationDelegate>>();
            _delegateSignatures = new Dictionary<Type, DelegateSignature.Generator>();
            _loadedLibraries = new List<Type>();
        }

        public static Assembly main
        {
            get
            {
                if (_mainAssembly == null) _mainAssembly = new Assembly();
                return _mainAssembly;
            }
        }

        public bool isLoaded
        {
            get
            {
                return _loadedLibraries.Count > 0;
            }
        }

        public void AddDelegateSignature(Type t, DelegateSignature.Generator generator)
        {
            _delegateSignatures.Add(t, generator);
        }

        public void AddType(TypeDef typedef)
        {
            _typeMap.Add(typedef.name, typedef);

            string[] alias = typedef.alias;
            if (alias != null)
            {
                for (int i = 0; i < alias.Length; i += 1)
                {
                    _typeMap.Add(alias[i], typedef);
                }
            }
        }

        public void AddTypes(TypeDef[] typedefs)
        {
            foreach (TypeDef typedef in typedefs)
            {
                AddType(typedef);
            }
        }

        public void AddTypes(List<TypeDef> typedefs)
        {
            foreach (TypeDef typedef in typedefs)
            {
                AddType(typedef);
            }
        }

        public Operation.BinaryOperationDelegate GetBinaryOperation(Token.Type op, Type t0, Type t1)
        {
            //Console.WriteLine(op.ToString());
            Dictionary<ulong, Operation.BinaryOperationDelegate> operatorMap = null;
            if (_binaryOperators.TryGetValue(op, out operatorMap))
            {
                //Console.WriteLine("Getting Hash " + Hashing.Hash(t0, t1));
                Operation.BinaryOperationDelegate del = null;
                if (operatorMap.TryGetValue(Hashing.Hash(t0, t1), out del))
                {
                    return del;
                }
            }
            return null;
        }

        public DelegateSignature.Generator GetDelegateSignature(Type t)
        {
            DelegateSignature.Generator generator = null;
            _delegateSignatures.TryGetValue(t, out generator);
            return generator;
        }

        public DelegateSignature.Generator GetDelegateSignature(Delegate del)
        {
            DelegateSignature.Generator generator = null;
            _delegateSignatures.TryGetValue(del.GetType(), out generator);
            return generator;
        }

        public Type[] GetLoadedLibraries()
        {
            return _loadedLibraries.ToArray();
        }

        public TypeDef GetTypeDef(string name)
        {
            TypeDef typedef = null;
            _typeMap.TryGetValue(name, out typedef);
            return typedef;
        }

        public TypeDef GetTypeDef(Type type, bool matchBase = true)
        {
            if (type == null) throw new ArgumentNullException("No type was passed to assembly");
            TypeDef output = GetTypeDef(type.Name);
            if (matchBase)
            {
                if (type.IsArray)
                {
                    if (output == null) output = GetTypeDef(type.GetElementType().Name);
                }
            }

            if (output == null) output = GetTypeDef("object");
            return output;
        }

        public TypeDef[] GetTypeDefs()
        {
            List<TypeDef> list = new List<TypeDef>();
            var values = _typeMap.Values;
            foreach (var val in values)
            {
                list.Add(val);
            }
            return list.ToArray();
        }

        public string[] GetTypeNames()
        {
            List<string> list = new List<string>();
            var keys = _typeMap.Keys;
            foreach (var key in keys)
            {
                list.Add(key);
            }
            return list.ToArray();
        }

        public Operation.UnaryOperationDelegate GetUnaryOperation(Token.Type op, Type t0)
        {
            Dictionary<ulong, Operation.UnaryOperationDelegate> operatorMap = null;
            if (_unaryOperators.TryGetValue(op, out operatorMap))
            {
                Operation.UnaryOperationDelegate del = null;
                if (operatorMap.TryGetValue((ulong)(uint)t0.GetHashCode(), out del))
                {
                    return del;
                }
            }
            return null;
        }

        public bool IsReservedWord(string word)
        {
            if (IsTypeName(word)) return true;
            return false;
        }

        public bool IsTypeName(string word)
        {
            return GetTypeDef(word) != null;
        }

        public void LoadLibrary(Type library)
        {
            ExtractTypesFromClass(library);
            ExtractUnaryOpsFromClass(library);
            ExtractBinaryOpsFromClass(library);
            ExtractDelegateSignatures(library);
            _loadedLibraries.Add(library);
        }

        public void Unload()
        {
            _typeMap = new Dictionary<string, TypeDef>();
            _binaryOperators = new Dictionary<Token.Type, Dictionary<ulong, Operation.BinaryOperationDelegate>>();
            _unaryOperators = new Dictionary<Token.Type, Dictionary<ulong, Operation.UnaryOperationDelegate>>();
            _delegateSignatures = new Dictionary<Type, DelegateSignature.Generator>();
            _loadedLibraries = new List<Type>();
        }

        private void ExtractBinaryOpsFromClass(Type type)
        {
            MethodInfo[] methods = type.GetMethods();
            foreach (MethodInfo method in methods)
            {
                BinaryOperator binaryOp = method.GetCustomAttribute<BinaryOperator>();
                if (binaryOp == null) continue;
                if (!_binaryOperators.ContainsKey(binaryOp.op))
                {
                    _binaryOperators.Add(binaryOp.op, new Dictionary<ulong, Operation.BinaryOperationDelegate>());
                }
                _binaryOperators[binaryOp.op].Add(binaryOp.GetHash(), (Operation.BinaryOperationDelegate)method.CreateDelegate(typeof(Operation.BinaryOperationDelegate)));
            }
        }

        private void ExtractDelegateSignatures(Type type)
        {
            MethodInfo[] methods = type.GetMethods();
            foreach (MethodInfo method in methods)
            {
                DelegateSignature signature = method.GetCustomAttribute<DelegateSignature>();
                if (signature == null) continue;

                _delegateSignatures.Add(signature.type, (DelegateSignature.Generator)method.CreateDelegate(typeof(DelegateSignature.Generator)));
            }
        }

        private void ExtractTypesFromClass(Type type)
        {
            Type[] types = type.GetNestedTypes();
            foreach (Type t in types)
            {
                //Console.WriteLine("Loading Type: " + t.Name);
                if (t.IsSubclassOf(typeof(TypeDef)))
                {
                    ConstructorInfo ctor = t.GetConstructor(new Type[0]);
                    TypeDef typedef = (TypeDef)ctor.Invoke(null);
                    AddType(typedef);
                }
            }
        }

        private void ExtractUnaryOpsFromClass(Type type)
        {
            MethodInfo[] methods = type.GetMethods();
            foreach (MethodInfo method in methods)
            {
                UnaryOperator unaryOp = method.GetCustomAttribute<UnaryOperator>();
                if (unaryOp == null) continue;

                if (!_unaryOperators.ContainsKey(unaryOp.op))
                {
                    _unaryOperators.Add(unaryOp.op, new Dictionary<ulong, Operation.UnaryOperationDelegate>());
                }
                _unaryOperators[unaryOp.op].Add(unaryOp.GetHash(), (Operation.UnaryOperationDelegate)method.CreateDelegate(typeof(Operation.UnaryOperationDelegate)));
            }
        }
    }
}