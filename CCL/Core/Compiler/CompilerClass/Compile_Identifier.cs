using System;

namespace CCL.Core
{
    public partial class Compiler
    {
        protected Func<object, object> _contextIdenfierFunction;
        protected object _contextualIdentifierTarget;

        protected void ClearContextualIdentifierHandler()
        {
            _contextualIdentifierTarget = null;
            _contextIdenfierFunction = null;
        }

        protected virtual object CompileContextualIdentifier()
        {
            if (_contextIdenfierFunction != null)
            {
                return _contextIdenfierFunction(_contextualIdentifierTarget);
            }
            return null;
        }

        protected virtual object CompileIdentifier()
        {
            object res = CompileContextualIdentifier();

            if (res != null)
            {
                return res;
            }
            else if (MatchToken(Token.Type.Identifier))
            {
                int id = _cdata._scopeResolver.Resolve(previous.text);
                TypeDef typeDef = null;
                Type type = typeof(object);
                if (id < 0)
                {
                    if (_contextType != null)
                    {
                        type = ReflectionUtility.GetMemberDataType(_contextType, previous.text);
                        typeDef = _assembly.GetTypeDef(type);
                    }

                    return new RunTimeReference(type, previous.text, (Func<object>)(() => { return _cdata._contextInterface.context; }), _contextType);
                }
                type = _cdata._environment[id].type;
                return new CompileTimeReference(type, previous.text, id);
            }
            else
            {
                return CompilePrimitive();
            }
        }

        protected void SetContextualIdentifierHandler(Func<object, object> func, object target)
        {
            _contextualIdentifierTarget = target;
            _contextIdenfierFunction = func;
        }
    }
}