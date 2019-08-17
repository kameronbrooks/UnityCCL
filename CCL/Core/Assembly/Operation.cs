namespace CCL.Core
{
    public class Operation
    {
        public delegate object BinaryOperationDelegate(object arg0, object arg1, CompilerData _internal);

        public delegate object CastOperationDelegate(object arg0, CompilerData _internal);

        public delegate object UnaryOperationDelegate(object arg0, CompilerData _internal);
    }
}