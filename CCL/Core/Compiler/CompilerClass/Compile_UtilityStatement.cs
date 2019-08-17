using System;

namespace CCL.Core
{
    public partial class Compiler
    {
        protected virtual object CompileHelpStatement()
        {
            if (isAtEnd || Peek().type == Token.Type.EOS)
            {
                return (Func<object>)(() =>
                {
                    string meta = ReflectionUtility.PrintMembers(_cdata._contextInterface.context);
                    _cdata._exit(meta);
                    return meta;
                });
            }

            Func<object> func = null;
            object statement = CompileExpressionStatement();
            if (statement == null)
            {
                return (Func<object>)(() =>
                {
                    return null;
                });
            }

            Type returnType = CompilerUtility.GetReturnType(statement);

            TypeDef typedef = _assembly.GetTypeDef(returnType);

            if (CompilerUtility.IsFunc(statement))
            {
                if (returnType != typeof(object)) func = typedef.ToGenericFunction(statement, _cdata);
                else func = (Func<object>)statement;

                return (Func<object>)(() =>
                {
                    object val = ReflectionUtility.PrintMembers(func());
                    _cdata._exit(val);
                    return val;
                });
            }
            else if (CompilerUtility.IsReference(statement))
            {
                func = ((Reference)statement).CreateGetFunction(_cdata);

                return (Func<object>)(() =>
                {
                    object val = ReflectionUtility.PrintMembers(func());
                    _cdata._exit(val);
                    return val;
                });
            }
            else
            {
                return (Func<object>)(() =>
                {
                    _cdata._exit(ReflectionUtility.PrintMembers(statement));
                    return statement;
                });
            }
        }

        protected virtual object CompileRequireStatement()
        {
            if (isAtEnd || Peek().type == Token.Type.EOS)
            {
                throw new CompilationException("Type name expected");
            }
            if (MatchToken(Token.Type.StringLiteral))
            {
                _cdata._requiredTypeName = previous.text;
                Require(Token.Type.EOS, "; expected");
                return null;
            }
            throw new CompilationException("Type name expected");
        }
    }
}