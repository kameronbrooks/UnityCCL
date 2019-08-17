using System;

namespace CCL.Core
{
    internal class CompilationException : Exception
    {
        public static int line = 1;

        public CompilationException() : base()
        {
        }

        public CompilationException(string msg) : base(msg)
        {
        }

        public static RuntimeException Create(string msg, int line)
        {
            return new RuntimeException("CCL Compilation Error: [line " + line + "] " + msg);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}