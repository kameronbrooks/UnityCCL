using System;

namespace CCL.Core
{
    internal class CompileTimeReference : Reference
    {
        protected int _environmentIndex;

        public CompileTimeReference(Type t, string identifier, int envIndex) : base(t, identifier)
        {
            _environmentIndex = envIndex;
        }

        public int environmentIndex
        {
            get { return _environmentIndex; }
            set
            {
                _environmentIndex = value;
            }
        }

        public override Func<T> CreateGetFunction<T>(CompilerData cdata)
        {
            return () =>
            {
                return (T)cdata._environment[_environmentIndex].data;
            };
        }

        public override Func<T> CreateModifyFunction<T>(object value, CompilerData cdata, Token.Type operation)
        {
            Type rtype = CompilerUtility.GetReturnType(value);
            Operation.BinaryOperationDelegate del = cdata._assembly.GetBinaryOperation(operation, type, rtype);

            // This section was made for runtime type inference
            // May cause some weird issues, definitely check this out if there are problems
            if (del == null && type != typeof(Object) && rtype == typeof(Object)) del = cdata._assembly.GetBinaryOperation(operation, type, type);

            if (del == null && type == typeof(Object) && rtype != typeof(Object)) del = cdata._assembly.GetBinaryOperation(operation, rtype, rtype);

            if (del == null) throw new CompilationException("Cannot perform the operation " + operation.ToString() + " on " + type.Name + " and " + rtype.Name);

            object func = del(this, value, cdata);
            Type returnType = CompilerUtility.GetReturnType(func);
            TypeDef returnTypeDef = cdata._assembly.GetTypeDef(returnType);

            return () =>
            {
                object ob = returnTypeDef.CallFunction(func);
                cdata._environment[_environmentIndex].data = ob;
                return (T)ob;
            };
        }

        public override Func<T> CreateSetFunction<T>(object value, CompilerData cdata)
        {
            if (CompilerUtility.IsFunc(value))
            {
                Func<T> valueFunc = (Func<T>)value;
                return () =>
                {
                    T ob = valueFunc();
                    //Type checking
                    Type refType = cdata._environment[_environmentIndex].type;
                    if (refType != typeof(Object) && refType != ob.GetType())
                    {
                        throw new RuntimeException("data type does not match: " + refType.Name + " is not " + ob.GetType().Name);
                    }

                    //Assign data
                    cdata._environment[_environmentIndex].data = ob;
                    return (T)(cdata._environment[_environmentIndex].data);
                };
            }
            else
            {
                T val = (T)value;
                return () =>
                {
                    //Type checking
                    Type refType = cdata._environment[_environmentIndex].type;
                    if (refType != typeof(Object) && refType != val.GetType()) throw new RuntimeException("data type does not match");

                    //Assign data
                    cdata._environment[_environmentIndex].data = val;
                    return val;
                };
            }
        }

        /*
        protected override Func<T> CreateModifyFunction_Add<T>(object value, CompilerData cdata)
        {
            Type type = cdata._environment[_environmentIndex].data.GetType();

            if (type == typeof(int))
            {
                if (CompilerUtility.IsFunc(value))
                {
                    Func<int> valueFunc = (Func<int>)value;
                    return () =>
                    {
                        int ob = (int)(cdata._environment[_environmentIndex].data) + valueFunc();
                        cdata._environment[_environmentIndex].data = ob;
                        return (T)(object)ob;
                    };
                }
                else
                {
                    int val = (int)value;
                    return () =>
                    {
                        cdata._environment[_environmentIndex].data = (int)(cdata._environment[_environmentIndex].data) + val;
                        return (T)(object)cdata._environment[_environmentIndex].data;
                    };
                }
            }
            else if (type == typeof(float))
            {
                if (CompilerUtility.IsFunc(value))
                {
                    Func<float> valueFunc = (Func<float>)value;
                    return () =>
                    {
                        float ob = (float)(cdata._environment[_environmentIndex].data) + valueFunc();
                        cdata._environment[_environmentIndex].data = ob;
                        return (T)(object)ob;
                    };
                }
                else
                {
                    float val = (float)value;
                    return () =>
                    {
                        cdata._environment[_environmentIndex].data = (float)(cdata._environment[_environmentIndex].data) + val;
                        return (T)(object)cdata._environment[_environmentIndex].data;
                    };
                }
            }
            else if (type == typeof(string))
            {
                if (CompilerUtility.IsFunc(value))
                {
                    Func<string> valueFunc = (Func<string>)value;
                    return () =>
                    {
                        string ob = (string)(cdata._environment[_environmentIndex].data) + valueFunc();
                        cdata._environment[_environmentIndex].data = ob;
                        return (T)(object)ob;
                    };
                }
                else
                {
                    string val = (string)value;
                    return () =>
                    {
                        cdata._environment[_environmentIndex].data = (string)(cdata._environment[_environmentIndex].data) + val;
                        return (T)(object)cdata._environment[_environmentIndex].data;
                    };
                }
            }
            throw new Exception("Native += is not defined for class type, must create externaly by using custom operator: " + type.Name);
        }

        protected override Func<T> CreateModifyFunction_Subtract<T>(object value, CompilerData cdata)
        {
            Type type = cdata._environment[_environmentIndex].data.GetType();

            if (type == typeof(int))
            {
                if (CompilerUtility.IsFunc(value))
                {
                    Func<int> valueFunc = (Func<int>)value;
                    return () =>
                    {
                        int ob = (int)(cdata._environment[_environmentIndex].data) - valueFunc();
                        cdata._environment[_environmentIndex].data = ob;
                        return (T)(object)ob;
                    };
                }
                else
                {
                    int val = (int)value;
                    return () =>
                    {
                        cdata._environment[_environmentIndex].data = (int)(cdata._environment[_environmentIndex].data) - val;
                        return (T)(object)cdata._environment[_environmentIndex].data;
                    };
                }
            }
            else if (type == typeof(float))
            {
                if (CompilerUtility.IsFunc(value))
                {
                    Func<float> valueFunc = (Func<float>)value;
                    return () =>
                    {
                        float ob = (float)(cdata._environment[_environmentIndex].data) - valueFunc();
                        cdata._environment[_environmentIndex].data = ob;
                        return (T)(object)ob;
                    };
                }
                else
                {
                    float val = (float)value;
                    return () =>
                    {
                        cdata._environment[_environmentIndex].data = (float)(cdata._environment[_environmentIndex].data) - val;
                        return (T)(object)cdata._environment[_environmentIndex].data;
                    };
                }
            }
            throw new Exception("Native += is not defined for class type, must create externaly by using custom operator: " + type.Name);
        }

        protected override Func<T> CreateModifyFunction_Multiply<T>(object value, CompilerData cdata)
        {
            Type type = cdata._environment[_environmentIndex].data.GetType();

            if (type == typeof(int))
            {
                if (CompilerUtility.IsFunc(value))
                {
                    Func<int> valueFunc = (Func<int>)value;
                    return () =>
                    {
                        int ob = (int)(cdata._environment[_environmentIndex].data) * valueFunc();
                        cdata._environment[_environmentIndex].data = ob;
                        return (T)(object)ob;
                    };
                }
                else
                {
                    int val = (int)value;
                    return () =>
                    {
                        cdata._environment[_environmentIndex].data = (int)(cdata._environment[_environmentIndex].data) * val;
                        return (T)(object)cdata._environment[_environmentIndex].data;
                    };
                }
            }
            else if (type == typeof(float))
            {
                if (CompilerUtility.IsFunc(value))
                {
                    Func<float> valueFunc = (Func<float>)value;
                    return () =>
                    {
                        float ob = (float)(cdata._environment[_environmentIndex].data) * valueFunc();
                        cdata._environment[_environmentIndex].data = ob;
                        return (T)(object)ob;
                    };
                }
                else
                {
                    float val = (float)value;
                    return () =>
                    {
                        cdata._environment[_environmentIndex].data = (float)(cdata._environment[_environmentIndex].data) * val;
                        return (T)(object)cdata._environment[_environmentIndex].data;
                    };
                }
            }
            throw new Exception("Native += is not defined for class type, must create externaly by using custom operator: " + type.Name);
        }

        protected override Func<T> CreateModifyFunction_Divide<T>(object value, CompilerData cdata)
        {
            Type type = cdata._environment[_environmentIndex].data.GetType();

            if (type == typeof(int))
            {
                if (CompilerUtility.IsFunc(value))
                {
                    Func<int> valueFunc = (Func<int>)value;
                    return () =>
                    {
                        int ob = (int)(cdata._environment[_environmentIndex].data) / valueFunc();
                        cdata._environment[_environmentIndex].data = ob;
                        return (T)(object)ob;
                    };
                }
                else
                {
                    int val = (int)value;
                    return () =>
                    {
                        cdata._environment[_environmentIndex].data = (int)(cdata._environment[_environmentIndex].data) / val;
                        return (T)(object)cdata._environment[_environmentIndex].data;
                    };
                }
            }
            else if (type == typeof(float))
            {
                if (CompilerUtility.IsFunc(value))
                {
                    Func<float> valueFunc = (Func<float>)value;
                    return () =>
                    {
                        float ob = (float)(cdata._environment[_environmentIndex].data) / valueFunc();
                        cdata._environment[_environmentIndex].data = ob;
                        return (T)(object)ob;
                    };
                }
                else
                {
                    float val = (float)value;
                    return () =>
                    {
                        cdata._environment[_environmentIndex].data = (float)(cdata._environment[_environmentIndex].data) / val;
                        return (T)(object)cdata._environment[_environmentIndex].data;
                    };
                }
            }
            throw new Exception("Native += is not defined for class type, must create externaly by using custom operator: " + type.Name);
        }

        protected override Func<T> CreateModifyFunction_Increment<T>(CompilerData cdata)
        {
            Type type = cdata._environment[_environmentIndex].data.GetType();

            if (type == typeof(int))
            {
                return () =>
                {
                    int ob = (int)(cdata._environment[_environmentIndex].data) + 1;
                    cdata._environment[_environmentIndex].data = ob;
                    return (T)(object)ob;
                };
            }
            else if (type == typeof(float))
            {
                return () =>
                {
                    float ob = (float)(cdata._environment[_environmentIndex].data) + 1;
                    cdata._environment[_environmentIndex].data = ob;
                    return (T)(object)ob;
                };
            }
            throw new Exception("Native += is not defined for class type, must create externaly by using custom operator: " + type.Name);
        }

        protected override Func<T> CreateModifyFunction_Decrement<T>(CompilerData cdata)
        {
            Type type = cdata._environment[_environmentIndex].data.GetType();

            if (type == typeof(int))
            {
                return () =>
                {
                    int ob = (int)(cdata._environment[_environmentIndex].data) - 1;
                    cdata._environment[_environmentIndex].data = ob;
                    return (T)(object)ob;
                };
            }
            else if (type == typeof(float))
            {
                return () =>
                {
                    float ob = (float)(cdata._environment[_environmentIndex].data) - 1;
                    cdata._environment[_environmentIndex].data = ob;
                    return (T)(object)ob;
                };
            }
            throw new Exception("Native += is not defined for class type, must create externaly by using custom operator: " + type.Name);
        }
        */
    }
}