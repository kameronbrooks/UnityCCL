using System;

namespace CCL.Core
{
    internal class FunctionCall : CompileTimeReference
    {
        private object[] _argBuffer;
        private object[] _argNodeBuffer;
        private CompilerData _cdata;
        private Type _returnType;
        private TypeDef _typeDef;

        public FunctionCall(Type t, string identifier, int envIndex, object[] argumentNodes, CompilerData cdata) : base(t, identifier, envIndex)
        {
            UnityEngine.Debug.Log("Creating Function");
            _returnType = (t != null) ? CompilerUtility.GetReturnType(t) : typeof(object);
            _cdata = cdata;
            _argNodeBuffer = (argumentNodes != null) ? argumentNodes : new object[0];
            _typeDef = _cdata._assembly.GetTypeDef(CompilerUtility.GetReturnType(t));
            _argBuffer = new object[0];  //always zero, on a function call the variables are injected right into the envrionment array

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
            return (Func<T>)(() =>
            {
                string enclosingScope = _cdata._scopeResolver.currentScopeID;
                _cdata._scopeResolver.SetScopeByID(_identifer);
                //CopyArgumentsToBuffer(cdata, out buffer);
                ResolveArguments(_argNodeBuffer, cdata);

                //Func<T> directCast = _cdata._environment[_environmentIndex] as Func<T>;
                //if (directCast != null) return directCast();

                Func<object> callback = _typeDef.ToGenericFunction(_cdata._environment[_environmentIndex].data, _cdata);
                T output = (T)_cdata._delegateCache.CallLambda(_identifer, callback, _argBuffer);
                UnityEngine.Debug.Log(output);
                //CopyFromArgumentsBuffer(cdata, buffer);
                _cdata._scopeResolver.SetScopeByID(enclosingScope);

                return output;
            });
        }

        private void CopyArgumentsToBuffer(CompilerData cdata, out object[] buffer)
        {
            int argIndex = cdata._scopeResolver.localMinIndex;
            int argCount = cdata._scopeResolver.localMaxIndex - argIndex;
            buffer = new object[argCount];

            for (int i = 0; i < argCount; i += 1)
            {
                buffer[i] = cdata._environment[argIndex + i].data;
            }
        }

        private void CopyFromArgumentsBuffer(CompilerData cdata, object[] buffer)
        {
            int argIndex = cdata._scopeResolver.localMinIndex;
            int argCount = cdata._scopeResolver.localMaxIndex - argIndex;
            for (int i = 0; i < argCount; i += 1)
            {
                cdata._environment[argIndex + i].data = buffer[i];
            }
        }

        private void ResolveArguments(object[] nodes, CompilerData cdata)
        {
            int argIndex = cdata._scopeResolver.localMinIndex;
            int argCount = cdata._scopeResolver.localArgumentCount;
            if (argCount != nodes.Length) throw new RuntimeException("Argument Count Mismatch, function expects " + argCount + " arguments. " + nodes.Length + " passed in");

            int i = 0;
            for (i = 0; i < argCount; i++)
            {
                Func<object> func = nodes[i] as Func<object>;
                cdata._environment[argIndex + i].data = (func != null) ? func() : nodes[i];
            }
        }

        private bool ValidateArguments()
        {
            return true;
        }
    }
}