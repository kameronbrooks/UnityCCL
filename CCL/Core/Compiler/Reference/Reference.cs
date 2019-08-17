using System;

namespace CCL.Core
{
    public abstract class Reference
    {
        protected string _identifer;
        protected Type _type;

        public Reference(Type t, string identifer)
        {
            _type = t;
            _identifer = identifer;
        }

        public string identifer
        {
            get { return _identifer; }
        }

        public virtual Type type
        {
            get
            {
                return _type;
            }
        }

        public abstract Func<T> CreateGetFunction<T>(CompilerData cdata);

        public virtual Func<object> CreateGetFunction(CompilerData cdata)
        {
            return CreateGetFunction<object>(cdata);
        }

        public virtual Func<T> CreateModifyFunction<T>(object value, CompilerData cdata, Token.Type operation)
        {
            throw new System.NotImplementedException();
        }

        public abstract Func<T> CreateSetFunction<T>(object value, CompilerData cdata);

        public virtual Func<object> CreateSetFunction(object value, CompilerData cdata)
        {
            return CreateSetFunction<object>(value, cdata);
        }
    }
}