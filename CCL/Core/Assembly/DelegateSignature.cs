using System;

namespace CCL.Core
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class DelegateSignature : Attribute
    {
        private Type _type;

        public DelegateSignature(Type t)
        {
            _type = t;
        }

        public delegate object Generator(object func, object[] args);

        public Type type
        {
            get { return _type; }
            set
            {
                _type = value;
            }
        }
    }
}