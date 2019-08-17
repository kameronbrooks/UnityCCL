using System;
using System.Collections.Generic;

namespace CCL.Core
{
    public class CompilerData
    {
        public Assembly _assembly;

        public int _compilerFlags;

        public ContextInterface _contextInterface;

        public int _currentLine;

        public DelegateCache _delegateCache;

        public List<MemoryObject> _environment;

        public MethodCache _methodCache;

        public string _requiredTypeName;

        public object _returnValue;

        public int _runtimeFlags;

        public ScopeResolver _scopeResolver;

        public CompilerData(Assembly assembly)
        {
            _environment = new List<MemoryObject>();
            _scopeResolver = new ScopeResolver();
            _contextInterface = new ContextInterface();
            _methodCache = new MethodCache();
            _delegateCache = new DelegateCache(assembly);
            _runtimeFlags = 0;
            _compilerFlags = 0;
            _assembly = assembly;
            _requiredTypeName = "";
        }

        public enum ControlInstruction
        {
            Exit = 1,
            Break = 2,
            Continue = 4,
            Next = 8
        }

        public object _break()
        {
            _runtimeFlags |= (int)ControlInstruction.Break;
            return null;
        }

        public object _continue()
        {
            _runtimeFlags |= (int)ControlInstruction.Continue;
            return null;
        }

        public object _exit(object returnVal)
        {
            _runtimeFlags |= (int)ControlInstruction.Exit;
            _returnValue = returnVal;
            return null;
        }

        public object _next()
        {
            _runtimeFlags |= (int)ControlInstruction.Next;
            return null;
        }

        public object _resetFlags()
        {
            _runtimeFlags = 0;
            return null;
        }

        public int AddMemoryObject(object val, byte flags = 0)
        {
            _environment.Add(new MemoryObject(val, flags));
            return _environment.Count - 1;
        }

        public int AddMemoryObject(Type t, object val, byte flags = 0)
        {
            _environment.Add(new MemoryObject(t, val, flags));
            return _environment.Count - 1;
        }

        public System.Exception CreateException(string message)
        {
            return RuntimeException.Create(message, _currentLine);
        }

        public bool IsRequiredType(object context)
        {
            if (_requiredTypeName == "") return true;
            if (context == null) return false;
            Type t = context.GetType();
            return t.Name == _requiredTypeName;
        }

        public class MemoryObject
        {
            readonly public Type type;

            public object data;

            private byte flags;

            public MemoryObject(Type type, object data, byte flags)
            {
                this.type = type;
                this.data = data;
                this.flags = flags;
            }

            public MemoryObject(object data, byte flags)
            {
                Type t = (data != null) ? data.GetType() : typeof(object);
                this.type = t;
                this.data = data;
                this.flags = flags;
            }

            public enum Flag : byte { Const = 1 };

            public bool IsFlagSet(Flag flag)
            {
                return (flags & (byte)flag) != 0;
            }

            public void SetFlag(Flag flag)
            {
                this.flags |= (byte)flag;
            }
        }
    }
}