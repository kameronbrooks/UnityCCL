using System;

namespace CCL.Core
{
    public partial class Compiler
    {
        protected virtual object CompileAddition()
        {
            return CompileDefaultBinaryOperator(CompileMultiplication, Token.Type.Addition, Token.Type.Subtraction);
        }

        protected virtual object CompileAssignment()
        {
            return CompileDefaultBinaryOperator(
                CompileBooleanLogic,
                Token.Type.Assign,
                Token.Type.AssignAdd,
                Token.Type.AssignSubtract,
                Token.Type.AssignMult,
                Token.Type.AssignDiv
                );
        }

        protected virtual object CompileBitwise()
        {
            return CompileDefaultBinaryOperator(CompileBitwiseShift, Token.Type.BitwiseAnd, Token.Type.BitwiseOr, Token.Type.BitwiseXOr);
        }

        protected virtual object CompileBitwiseShift()
        {
            return CompileDefaultBinaryOperator(CompileAddition, Token.Type.BitwiseLeftShift, Token.Type.BitwiseRightShift);
        }

        protected virtual object CompileBooleanLogic()
        {
            return CompileDefaultBinaryOperator(
                CompileEquality,
                Token.Type.And,
                Token.Type.Or
                );
        }

        protected virtual object CompileCast()
        {
            if (MatchToken(Token.Type.Cast))
            {
                Token op = previous;
                object right = CompileCast();
                Type rightType = CompilerUtility.GetReturnType(right);

                bool isArray = op.text.Contains("[]");

                if (isArray)
                {
                    TypeDef typeDef = _assembly.GetTypeDef(op.text.Substring(0, op.text.Length - 2));
                    if (typeDef == null) throw new Exception("Compilation Error: The specified type " + op.text + " was not found in the Assembly");

                    return typeDef.CastArray(right, _cdata);
                }
                else
                {
                    TypeDef typeDef = _assembly.GetTypeDef(op.text);
                    if (typeDef == null) throw new Exception("Compilation Error: The specified type " + op.text + " was not found in the Assembly");

                    return typeDef.Cast(right, _cdata);
                }
            }
            return CompilePostUnary();
        }

        protected virtual object CompileComparison()
        {
            return CompileDefaultBinaryOperator(
                CompileIsSubsetOf,
                Token.Type.GreaterThan,
                Token.Type.GreaterThanOrEqualTo,
                Token.Type.LessThan,
                Token.Type.LessThanOrEqualTo
                );
        }

        protected virtual object CompileEquality()
        {
            return CompileDefaultBinaryOperator(
                CompileComparison,
                Token.Type.Equals,
                Token.Type.NotEquals
                );
        }

        protected virtual object CompileExponent()
        {
            return CompileDefaultBinaryOperator(CompileUnary, Token.Type.Power);
        }

        protected virtual object CompileExpression()
        {
            return CompileAssignment();
        }

        protected virtual object CompileExpressionStatement()
        {
            if (Peek().type == Token.Type.EOS) return null;

            object stmt = CompileExpression();
            Require(Token.Type.EOS, "; Expected");
            return stmt;
        }

        protected virtual object CompileInterp()
        {
            return CompileDefaultBinaryOperator(
                CompileBitwise,
                Token.Type.Interp
                );
        }

        protected virtual object CompileIsSubsetOf()
        {
            return CompileDefaultBinaryOperator(
                CompileInterp,
                Token.Type.IsSubsetOf,
                Token.Type.IsNotSubsetOf
                );
        }

        protected virtual object CompileMultiplication()
        {
            return CompileDefaultBinaryOperator(CompileExponent, Token.Type.Multiplication, Token.Type.Division, Token.Type.Modulo);
        }

        protected virtual object CompilePostUnary()
        {
            object left = CompileFunctionCall();
            object output = null;

            if (MatchToken(Token.Type.Increment, Token.Type.Decrement))
            {
                Token op = previous;
                Type leftType = CompilerUtility.GetReturnType(left);

                Operation.UnaryOperationDelegate del = _assembly.GetUnaryOperation(op.type, leftType);
                if (del == null) throw new Exception("Cannot perform the operation " + op.text + " on " + leftType.Name);

                return del.Invoke(left, _cdata);
            }

            return (output != null) ? (object)output : left;
        }

        protected virtual object CompileUnary()
        {
            if (MatchToken(Token.Type.Not, Token.Type.Subtraction))
            {
                Token op = previous;
                object right = CompileUnary();
                Type rightType = CompilerUtility.GetReturnType(right);

                Operation.UnaryOperationDelegate del = _assembly.GetUnaryOperation(op.type, rightType);
                if (del == null) throw new Exception("Cannot perform the operation " + op.text + " on " + rightType.Name);

                return del.Invoke(right, _cdata);
            }
            return CompileCast();
        }
    }
}