using System;

namespace CCL.Core
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class UnaryOperator : Attribute
    {
        private System.Type _a;
        private Token.Type _op;

        public UnaryOperator(System.Type typeA, Token.Type op)
        {
            this._a = typeA;
            this._op = op;
        }

        public Token.Type op
        {
            get
            {
                return _op;
            }
            set
            {
                _op = value;
            }
        }

        public Type typeA
        {
            get
            {
                return _a;
            }
            set
            {
                _a = value;
            }
        }

        public ulong GetHash()
        {
            return (ulong)(uint)_a.GetHashCode();
        }
    }
}