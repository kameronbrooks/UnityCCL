namespace CCL
{
    internal class Hashing
    {
        public static ulong Hash(object ob0, object ob1)
        {
            if (ob1 == null && ob0 == null)
            {
                return (ulong)0;
            }
            else if (ob1 == null && ob0 != null)
            {
                return ((ulong)(uint)ob0.GetHashCode());
            }
            else if (ob1 != null && ob0 == null)
            {
                return ((ulong)(uint)0) | (((ulong)(uint)ob1.GetHashCode()) << 32);
            }
            return ((ulong)(uint)ob0.GetHashCode()) | (((ulong)(uint)ob1.GetHashCode()) << 32);
        }
    }
}