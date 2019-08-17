using System;
using System.Collections.Generic;
using System.Linq;

namespace CCL.Core
{
    public class ScopeResolver
    {
        public const string GLOBAL_ID = "_global";

        private ScopeStack _currentScope;
        private string _currentScopeID;
        private Dictionary<string, ScopeStack> _data;

        private ScopeStack _globalScope;

        private int _index;

        public ScopeResolver()
        {
            _data = new Dictionary<string, ScopeStack>();
            _currentScopeID = GLOBAL_ID;
            _data.Add(_currentScopeID, new ScopeStack());

            _globalScope = _data[GLOBAL_ID];
            SetScopeByID(GLOBAL_ID);
        }

        public string currentScopeID
        {
            get
            {
                return _currentScopeID;
            }
        }

        public int localArgumentCount
        {
            get
            {
                return _currentScope.argumentCount;
            }
            set
            {
                _currentScope.argumentCount = value;
            }
        }

        public int localMaxIndex
        {
            get
            {
                return _currentScope.maxIndex;
            }
        }

        public int localMinIndex
        {
            get
            {
                return _currentScope.minIndex;
            }
        }

        public void Add(string identifier, int index = -1)
        {
            _currentScope.Add(identifier, index);
        }

        public void AddScopeStack(string scopeID)
        {
            _data.Add(scopeID, new ScopeStack());
        }

        public void Pop()
        {
            _currentScope.Pop();
        }

        public void Push()
        {
            _currentScope.Push();
        }

        /// <summary>
        /// Returns the array index of a given identifier
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public int Resolve(string identifier)
        {
            if (_currentScope != _globalScope)
            {
                int index = _currentScope.Resolve(identifier);
                if (index > -1) return index;
            }
            return _globalScope.Resolve(identifier);
        }

        public int ResolveGlobal(string identifier)
        {
            return _globalScope.ResolveGlobal(identifier);
        }

        public void SetScopeByID(string id)
        {
            _currentScopeID = id;
            _currentScope = _data[_currentScopeID];
        }

        public void SetToGlobal()
        {
            SetScopeByID(GLOBAL_ID);
        }

        private class ScopeStack
        {
            private int _argumentCount;
            private Stack<Dictionary<string, int>> _data;
            private int _index;
            private int _maxIndex;
            private int _minIndex;

            public ScopeStack()
            {
                _data = new Stack<Dictionary<string, int>>();
                _data.Push(new Dictionary<string, int>());
                _index = 0;
                _minIndex = Int32.MaxValue;
                _maxIndex = Int32.MinValue;
                _argumentCount = -1;
            }

            public int argumentCount
            {
                get
                {
                    return _argumentCount;
                }
                set
                {
                    if (_argumentCount >= 0) throw new RuntimeException("The argument count for the current scope has already been set");
                    _argumentCount = value;
                }
            }

            public int maxIndex
            {
                get
                {
                    return _maxIndex;
                }
            }

            public int minIndex
            {
                get
                {
                    return _minIndex;
                }
            }

            public void Add(string identifier, int index = -1)
            {
                if (index < 0)
                {
                    index = _index;
                    _index += 1;
                }
                if (index < _minIndex) _minIndex = index;
                if (index > _maxIndex) _maxIndex = index;
                _data.Peek().Add(identifier, index);
            }

            public void Pop()
            {
                _data.Pop();
            }

            public void Push()
            {
                _data.Push(new Dictionary<string, int>());
            }

            public int Resolve(string identifier)
            {
                int index = _data.Count - 1;
                while (index >= 0)
                {
                    if (_data.ElementAt(index).ContainsKey(identifier))
                    {
                        return _data.ElementAt(index)[identifier];
                    }
                    index -= 1;
                }
                return -1;
            }

            public int ResolveGlobal(string identifier)
            {
                int output = -1;

                if (_data.ElementAt(0).TryGetValue(identifier, out output))
                {
                    return output;
                }
                return -1;
            }
        }
    }

    /*
    public class ScopeResolver
    {
        public const string GLOBAL_ID = "_global";

        private Dictionary<string, ScopeStack> _data;
        private class StackFrame
        {
            public int _basePointerIndex;
            public int _size;
            public int _argCount;
        }
        Stack<StackFrame> _stackFrame;

        private class ScopeStack
        {
            Stack<Dictionary<string, int>> _data;
            int _index;
            int _minIndex;
            int _maxIndex;
            int _argumentCount;

            public int minIndex {
                get {
                    return _minIndex;
                }
            }

            public int maxIndex {
                get {
                    return _maxIndex;
                }
            }

            public int argumentCount {
                get {
                    return _argumentCount;
                }
                set
                {
                    if (_argumentCount >= 0) throw new RuntimeException("The argument count for the current scope has already been set");
                    _argumentCount = value;
                }
            }

            public ScopeStack()
            {
                _data = new Stack<Dictionary<string, int>>();
                _data.Push(new Dictionary<string, int>());
                _index = 0;
                _minIndex = Int32.MaxValue;
                _maxIndex = Int32.MinValue;
                _argumentCount = -1;
            }
            public void Push()
            {
                _data.Push(new Dictionary<string, int>());
            }
            public void Pop()
            {
                _data.Pop();
            }

            public void Add(string identifier, int index = -1)
            {
                if (index < 0)
                {
                    index = _index;
                    _index += 1;
                }
                if (index < _minIndex) _minIndex = index;
                if (index > _maxIndex) _maxIndex = index;
                _data.Peek().Add(identifier, index);
            }
            public int Resolve(string identifier)
            {
                int index = _data.Count - 1;
                while (index >= 0)
                {
                    if (_data.ElementAt(index).ContainsKey(identifier))
                    {
                        return _data.ElementAt(index)[identifier];
                    }
                    index -= 1;
                }
                return -1;
            }

            public int ResolveGlobal(string identifier)
            {
                int output = -1;

                if (_data.ElementAt(0).TryGetValue(identifier, out output))
                {
                    return output;
                }
                return -1;
            }
        }
        int _index;
        string _currentScopeID;
        ScopeStack _currentScope;
        ScopeStack _globalScope;

        public string currentScopeID
        {
            get
            {
                return _currentScopeID;
            }
        }

        public ScopeResolver()
        {
            _data = new Dictionary<string, ScopeStack>();
            _currentScopeID = GLOBAL_ID;
            _data.Add(_currentScopeID, new ScopeStack());

            _globalScope = _data[GLOBAL_ID];
            SetScopeByID(GLOBAL_ID);
        }

        public void SetScopeByID(string id)
        {
            _currentScopeID = id;
            _currentScope = _data[_currentScopeID];
        }
        public void AddScopeStack(string scopeID)
        {
            _data.Add(scopeID, new ScopeStack());
        }
        public void SetToGlobal()
        {
            SetScopeByID(GLOBAL_ID);
        }

        public void Push()
        {
            _currentScope.Push();
        }
        public void Pop()
        {
            _currentScope.Pop();
        }

        public void Add(string identifier, int index = -1)
        {
            _currentScope.Add(identifier, index);
        }

        public int ResolveGlobal(string identifier)
        {
            return _globalScope.ResolveGlobal(identifier);
        }

        /// <summary>
        /// Returns the array index of a given identifier
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public int Resolve(string identifier)
        {
            if(_currentScope != _globalScope)
            {
                int index = _currentScope.Resolve(identifier);
                if (index > -1) return index;
            }
            return _globalScope.Resolve(identifier);
        }

        public int localMinIndex {
            get {
                return _currentScope.minIndex;
            }
        }

        public int localMaxIndex {
            get {
                return _currentScope.maxIndex;
            }
        }

        public int localArgumentCount {
            get {
                return _currentScope.argumentCount;
            }
            set
            {
                _currentScope.argumentCount = value;
            }
        }
    }
    */
}