using System;

namespace CCL.Core
{
    internal class RuntimeException : Exception
    {
        public RuntimeException() : base()
        {
        }

        public RuntimeException(string msg) : base(msg)
        {
        }

        public static RuntimeException Create(string msg, int line)
        {
            return new RuntimeException("CCL Run-Time Error: [line " + line + "] " + msg);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}