using System;

namespace CCL.Core
{
    public class CompiledScript
    {
        private CompilerData _cdata;
        private Func<object> _function;

        public CompiledScript()
        {
            _function = null;
        }

        public CompiledScript(Func<object> function, CompilerData cdata)
        {
            _function = function;
            _cdata = cdata;
        }

        public ContextInterface contextInterface
        {
            get
            {
                return _cdata._contextInterface;
            }
        }

        public object Execute()
        {
            return _function();
        }

        public Func<T> GetFunction<T>(string identifier = "_")
        {
            if (identifier == "_")
            {
                return _function as Func<T>;
            }
            else
            {
                return GetVariable(identifier) as Func<T>;
            }
        }

        public object GetVariable(string identifier)
        {
            if (_cdata._environment == null) throw new NullReferenceException("There is no environmnt defined");

            int index = _cdata._scopeResolver.ResolveGlobal(identifier);

            if (index < 0) throw new NullReferenceException("The variable " + identifier + " is not defined in script");

            return _cdata._environment[index];
        }

        public object Invoke()
        {
            if (_cdata._contextInterface == null) throw new Exception("CCL requires a context object");
            try
            {
                return _function();
            }
            catch (Exception e)
            {
                throw _cdata.CreateException(e.Message);
            }
        }

        public void SetContext(object context)
        {
            if (!_cdata.IsRequiredType(context))
            {
                string tName = (context != null) ? context.GetType().Name : "Null";
                throw new RuntimeException("Context of type " + tName + " does not match the required script type of " + _cdata._requiredTypeName);
            }
            _cdata._contextInterface.context = context;
        }

        public void SetVariable(string identifier, object val)
        {
            if (_cdata._environment == null) throw new NullReferenceException("There is no environmnt defined");

            int index = _cdata._scopeResolver.ResolveGlobal(identifier);

            if (index < 0) throw new NullReferenceException("The variable " + identifier + " is not defined in script");

            Type dataType = _cdata._environment[index].type;
            if (dataType != typeof(object) && val.GetType() != dataType) throw new Exception("The datatype of the passed in variable does not match must be type " + dataType.Name);

            _cdata._environment[index].data = val;
        }
    }
}