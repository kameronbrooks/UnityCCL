using System;

namespace CCL.Core
{
    [System.AttributeUsage(System.AttributeTargets.Method)]
    public class BinaryOperator : Attribute
    {
        private System.Type _a;
        private System.Type _b;
        private Token.Type _op;

        public BinaryOperator(System.Type typeA, Token.Type op, System.Type typeB)
        {
            this._a = typeA;
            this._op = op;
            this._b = typeB;
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

        public Type typeB
        {
            get
            {
                return _b;
            }
            set
            {
                _b = value;
            }
        }

        public ulong GetHash()
        {
            return Hashing.Hash(_a, _b);
            //return ((ulong)((uint)_a.GetHashCode())) | (((ulong)((uint)_b.GetHashCode())) << 32);
        }
    }
}