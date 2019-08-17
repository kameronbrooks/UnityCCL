namespace CCL
{
    public class Token
    {
        public int _meta_index;

        public int _meta_lineNumber;

        public string text;

        public Type type;

        public Token(string val, Type valType)
        {
            text = val;
            type = valType;
        }

        public enum Type
        {
            None,
            Ignored,
            Ambiguous,
            Alpha,
            Identifier,
            Numeric,
            Operator,
            StringLiteral,
            HexLiteral,
            OpenParenthesis,
            FunctionCallBegin,
            ClosingParethesis,
            ArrayIndexBegin,
            ClosingArrayIndex,
            EOS,
            OpenBlock,
            CloseBlock,
            Comma,
            If,
            Else,
            ElseIf,
            While,
            For,
            Return,

            Require,
            Help,

            Break,
            Continue,

            NewLine,
            Dot,
            IfDot,

            Assign,
            Negate,

            AssignInc,
            AssignDec,
            AssignAdd,
            AssignSubtract,
            AssignMult,
            AssignDiv,

            Increment,
            Decrement,

            Modulo,
            Addition,
            Subtraction,
            Multiplication,
            Division,
            Power,
            Sqrt,

            BitwiseOr,
            BitwiseAnd,
            BitwiseXOr,
            BitwiseLeftShift,
            BitwiseRightShift,

            Not,
            Equals,
            NotEquals,
            GreaterThan,
            GreaterThanOrEqualTo,
            LessThan,
            LessThanOrEqualTo,

            IsSubsetOf,
            IsNotSubsetOf,
            Interp,

            And,
            Or,

            True,
            False,
            Null,

            Var,
            Dynamic,
            TypeName,

            Cast,
            Colon,

            ArrowOp,
            ThinArrow
        }

        public override string ToString()
        {
            return type.ToString() + ": " + text;
        }
    }
}