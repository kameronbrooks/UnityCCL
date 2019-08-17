namespace CCL.Core
{
    public class ContextInterface
    {
        private object _context;

        public object context
        {
            get { return _context; }
            set
            {
                _context = value;
            }
        }
    }
}