using System;
using System.Reflection;

namespace CCL.Core
{
    public class DelegateUtility
    {
        public static Type CreateFunctionType(Type returnType)
        {
            return typeof(Func<>).MakeGenericType(returnType);
        }

        public static Type GetMethodType(MethodInfo method)
        {
            ParameterInfo[] methodParams = method.GetParameters();
            Type[] methodParamTypes = new Type[methodParams.Length + 1];
            for (int j = 0; j < methodParams.Length; j += 1)
            {
                methodParamTypes[j] = methodParams[j].ParameterType;
            }
            methodParamTypes[methodParamTypes.Length - 1] = method.ReturnType;

            return System.Linq.Expressions.Expression.GetDelegateType(methodParamTypes);
        }

        public static Delegate LambdaToDelegate(object lambda)
        {
            Type t = lambda.GetType();
            MethodInfo method = t.GetMethod(("Invoke"));
            return MethodToInstanceDelegate(lambda, method);
        }

        public static Delegate MethodToInstanceDelegate(object target, MethodInfo method)
        {
            ParameterInfo[] methodParams = method.GetParameters();
            Type[] methodParamTypes = new Type[methodParams.Length + 1];
            for (int j = 0; j < methodParams.Length; j += 1)
            {
                methodParamTypes[j] = methodParams[j].ParameterType;
            }
            methodParamTypes[methodParamTypes.Length - 1] = method.ReturnType;

            Type delegateType = System.Linq.Expressions.Expression.GetDelegateType(methodParamTypes);
            if ((target as Type) != null)
            {
                return Delegate.CreateDelegate(delegateType, null, method);
            }
            return Delegate.CreateDelegate(delegateType, target, method);
        }

        public static Delegate MethodToOpenDelegate(Type type, MethodInfo method)
        {
            ParameterInfo[] methodParams = method.GetParameters();
            Type[] methodParamTypes = new Type[methodParams.Length + 2];

            methodParamTypes[0] = type;
            for (int j = 0; j < methodParams.Length; j += 1)
            {
                methodParamTypes[j + 1] = methodParams[j].ParameterType;
            }
            methodParamTypes[methodParamTypes.Length - 1] = method.ReturnType;

            Type delegateType = System.Linq.Expressions.Expression.GetDelegateType(methodParamTypes);

            return Delegate.CreateDelegate(delegateType, null, method);
        }

        public static Delegate StaticMethodToDelegate(MethodInfo method)
        {
            ParameterInfo[] methodParams = method.GetParameters();
            Type[] methodParamTypes = new Type[methodParams.Length + 1];
            for (int j = 0; j < methodParams.Length; j += 1)
            {
                methodParamTypes[j] = methodParams[j].ParameterType;
            }
            methodParamTypes[methodParamTypes.Length - 1] = method.ReturnType;

            Type delegateType = System.Linq.Expressions.Expression.GetDelegateType(methodParamTypes);

            return Delegate.CreateDelegate(delegateType, null, method);
        }
    }
}