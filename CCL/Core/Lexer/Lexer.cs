using System;
using System.Collections.Generic;
using System.Text;

namespace CCL.Core
{
    public class Lexer : ILexer
    {
        private Assembly _assembly;
        private int _index;
        private string _input;
        private int _lineNumber;

        public Lexer(Assembly assembly = null)
        {
            if (assembly != null)
            {
                _assembly = assembly;
            }
            else
            {
                _assembly = Assembly.main;
            }
        }

        private bool isEOF
        {
            get
            {
                return _index >= _input.Length;
            }
        }

        private char previous
        {
            get
            {
                return _input[_index - 1];
            }
        }

        public Token[] Tokenize(string input)
        {
            List<Token> tokens = new List<Token>();
            _index = 0;
            _input = input;
            _lineNumber = 1;

            while (!isEOF)
            {
                char cur = Peek();
                // Literals
                if (StepIf('\"'))
                {
                    tokens.Add(GetStringLiteral('\"'));
                }
                else if (StepIf('\''))
                {
                    tokens.Add(GetStringLiteral('\''));
                }
                else if (Char.IsDigit(cur))
                {
                    bool isHex = false;
                    // Hex Values
                    if (cur == '0')
                    {
                        if (_input.Length > _index + 1)
                        {
                            if (_input[_index + 1] == 'x')
                            {
                                isHex = true;
                                tokens.Add(GetHex());
                            }
                        }
                    }

                    // Normal Numbers
                    if (!isHex)
                    {
                        tokens.Add(GetNumber());
                    }
                }
                else if (Char.IsLetterOrDigit(cur) || cur == '_')
                {
                    Console.Write(cur);
                    string identifier = GetString();
                    Token token = GetKeywordToken(identifier);
                    if (token != null)
                    {
                        tokens.Add(token);
                    }
                    else
                    {
                        tokens.Add(CreateToken(Token.Type.Identifier, identifier));
                    }
                }
                else if (cur == '\n')
                {
                    _lineNumber += 1;
                    Step();
                }
                else
                {
                    // Try to find operator
                    Token op = GetOperator();
                    if (op != null)
                    {
                        tokens.Add(op);
                    }
                }
            }
            return tokens.ToArray();
        }

        private char Consume(char c)
        {
            if (!isEOF && Peek() == c) Step();
            return c;
        }

        private Token CreateToken(Token.Type type, string text)
        {
            Token token = new Token(text, type);
            token._meta_lineNumber = _lineNumber;
            token._meta_index = _index;
            return token;
        }

        private Token CreateToken(Token.Type type, params char[] chars)
        {
            Token token = new Token(new string(chars), type);
            token._meta_lineNumber = _lineNumber;
            token._meta_index = _index;
            return token;
        }

        private Token GetHex()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Step());
            builder.Append(Step());
            while (!isEOF && (Char.IsDigit(Peek()) || "ABCDEFabcdef".IndexOf(Peek()) >= 0))
            {
                builder.Append(Step());
            }

            return new Token(builder.ToString(), Token.Type.HexLiteral);
        }

        private Token GetKeywordToken(string text)
        {
            if (_assembly.IsTypeName(text))
            {
                return CreateToken(Token.Type.TypeName, text);
            }
            switch (text)
            {
                case "true":
                    return CreateToken(Token.Type.True, text);

                case "false":
                    return CreateToken(Token.Type.False, text);

                case "null":
                    return CreateToken(Token.Type.Null, text);

                case "if":
                    return CreateToken(Token.Type.If, text);

                case "else":
                    return CreateToken(Token.Type.Else, text);

                case "elseif":
                    return CreateToken(Token.Type.ElseIf, text);

                case "var":
                    return CreateToken(Token.Type.Var, text);

                case "for":
                    return CreateToken(Token.Type.For, text);

                case "while":
                    return CreateToken(Token.Type.While, text);

                case "break":
                    return CreateToken(Token.Type.Break, text);

                case "continue":
                    return CreateToken(Token.Type.Continue, text);

                case "return":
                    return CreateToken(Token.Type.Return, text);

                case "require":
                    return CreateToken(Token.Type.Require, text);

                case "helpme":
                    return CreateToken(Token.Type.Help, text);

                default:
                    return null;
            }
        }

        private Token GetNumber()
        {
            StringBuilder builder = new StringBuilder();
            int startIndex = _index;
            int radixIndex = -1;
            while (!isEOF && (Char.IsDigit(Peek()) || Peek() == '.'))
            {
                char cur = Step();
                if (cur == '.')
                {
                    if (Peek() == '.' || (radixIndex > -1 && radixIndex < _index - 1))
                    {
                        StepBack();
                        break;
                    }
                    else
                    {
                        radixIndex = _index;
                    }
                }
            }

            return new Token(_input.Substring(startIndex, _index - startIndex), Token.Type.Numeric);
        }

        private Token GetOperator()
        {
            char cur = Step();
            int tmpIndex = 0;
            switch (cur)
            {
                case '{':
                    return CreateToken(Token.Type.OpenBlock, cur);

                case '}':
                    return CreateToken(Token.Type.CloseBlock, cur);

                case '[':
                    return CreateToken(Token.Type.ArrayIndexBegin, cur);

                case ']':
                    return CreateToken(Token.Type.ClosingArrayIndex, cur);

                case '(':
                    tmpIndex = SaveIndex();
                    string str = GetString();
                    if (_assembly.IsTypeName(str))
                    {
                        if (LookAhead("[]"))
                        {
                            Step(2);
                            str += "[]";
                        }
                        if (Step() == ')') return CreateToken(Token.Type.Cast, str);
                    }
                    RestoreIndex(tmpIndex);
                    return CreateToken(Token.Type.OpenParenthesis, cur);

                case ')':
                    return CreateToken(Token.Type.ClosingParethesis, cur);

                case '.':
                    if (Peek() == '.')
                    {
                        return CreateToken(Token.Type.Interp, cur, Step());
                    }
                    return CreateToken(Token.Type.Dot, cur);

                case ',':
                    return CreateToken(Token.Type.Comma, cur);

                case '%':
                    return CreateToken(Token.Type.Modulo, cur);

                case ';':
                    return CreateToken(Token.Type.EOS, cur);

                case '-':
                    if (Peek() == '=')
                    {
                        return CreateToken(Token.Type.AssignSubtract, cur, Step());
                    }
                    if (Peek() == '-')
                    {
                        return CreateToken(Token.Type.Decrement, cur, Step());
                    }
                    else
                    {
                        return CreateToken(Token.Type.Subtraction, cur);
                    }
                case '+':
                    if (Peek() == '=')
                    {
                        return CreateToken(Token.Type.AssignAdd, cur, Step());
                    }
                    if (Peek() == '+')
                    {
                        return CreateToken(Token.Type.Increment, cur, Step());
                    }
                    else
                    {
                        return CreateToken(Token.Type.Addition, cur);
                    }
                case '*':
                    if (Peek() == '=')
                    {
                        return CreateToken(Token.Type.AssignMult, cur, Step());
                    }
                    if (Peek() == '*')
                    {
                        return CreateToken(Token.Type.Power, cur, Step());
                    }
                    else
                    {
                        return CreateToken(Token.Type.Multiplication, cur);
                    }
                case '/':
                    if (Peek() == '/')
                    {
                        SkipComment();
                        return null;
                    }
                    if (Peek() == '=')
                    {
                        return CreateToken(Token.Type.AssignDiv, cur, Step());
                    }
                    else
                    {
                        return CreateToken(Token.Type.Division, cur);
                    }
                case '>':
                    if (Peek() == '=')
                    {
                        return CreateToken(Token.Type.GreaterThanOrEqualTo, cur, Step());
                    }
                    else if (Peek() == '>')
                    {
                        return CreateToken(Token.Type.BitwiseRightShift, cur, Step());
                    }
                    else
                    {
                        return CreateToken(Token.Type.GreaterThan, cur);
                    }
                case '<':
                    if (Peek() == '=')
                    {
                        return CreateToken(Token.Type.LessThanOrEqualTo, cur, Step());
                    }
                    else if (Peek() == '>')
                    {
                        return CreateToken(Token.Type.Operator, cur, Step());
                    }
                    else if (Peek() == '<')
                    {
                        return CreateToken(Token.Type.BitwiseLeftShift, cur, Step());
                    }
                    else
                    {
                        return CreateToken(Token.Type.LessThan, cur);
                    }
                case '^':
                    return CreateToken(Token.Type.BitwiseXOr, cur);

                case ':':
                    if (Peek() == ':')
                    {
                        return CreateToken(Token.Type.IsSubsetOf, cur, Step());
                    }
                    return CreateToken(Token.Type.Colon);

                case '|':
                    if (Peek() == '|')
                    {
                        return CreateToken(Token.Type.Or, cur, Step());
                    }
                    else
                    {
                        return CreateToken(Token.Type.BitwiseOr, cur);
                    }
                case '&':
                    if (Peek() == '&')
                    {
                        return CreateToken(Token.Type.And, cur, Step());
                    }
                    else
                    {
                        return CreateToken(Token.Type.BitwiseAnd, cur);
                    }
                case '!':
                    if (Peek() == '=')
                    {
                        return CreateToken(Token.Type.NotEquals, cur, Step());
                    }
                    if (StepIf(':'))
                    {
                        if (StepIf(':'))
                        {
                            return CreateToken(Token.Type.IsNotSubsetOf, "!::");
                        }
                    }
                    return CreateToken(Token.Type.Not, cur);

                case '=':
                    if (Peek() == '=')
                    {
                        return CreateToken(Token.Type.Equals, cur, Step());
                    }
                    if (Peek() == '>')
                    {
                        return CreateToken(Token.Type.ArrowOp, cur, Step());
                    }
                    else
                    {
                        return CreateToken(Token.Type.Assign, cur);
                    }
                case '?':
                    if (Peek() == '.')
                    {
                        return CreateToken(Token.Type.IfDot, cur, Step());
                    }
                    else
                    {
                        return CreateToken(Token.Type.Help, cur, cur);
                    }
                default:
                    return null;
            }
        }

        private string GetString()
        {
            StringBuilder builder = new StringBuilder();
            // If characters are alphanumeric
            while (!isEOF && (Char.IsLetterOrDigit(Peek()) || Peek() == '_'))
            {
                builder.Append(Step());
            }
            return builder.ToString();
        }

        private Token GetStringLiteral(char delim)
        {
            StringBuilder builder = new StringBuilder();
            while (!isEOF && Peek() != delim)
            {
                builder.Append(Step());
            }
            Consume(delim);
            return new Token(builder.ToString(), Token.Type.StringLiteral);
        }

        private bool LookAhead(params char[] chars)
        {
            int start = _index;
            for (int i = 0; i < chars.Length; i++)
            {
                if (start + i >= _input.Length) return false;
                if (_input[start + i] != chars[i]) return false;
            }
            return true;
        }

        private bool LookAhead(string str)
        {
            int start = _index;
            for (int i = 0; i < str.Length; i++)
            {
                if (start + i >= _input.Length) return false;
                if (_input[start + i] != str[i]) return false;
            }
            return true;
        }

        private char Peek()
        {
            return _input[_index];
        }

        private void RestoreIndex(int ind)
        {
            _index = ind;
        }

        private int SaveIndex()
        {
            return _index;
        }

        private void SkipComment()
        {
            while (!isEOF && Step() != '\n')
            {
            }
            return;
        }

        private char Step()
        {
            char output = _input[_index];
            _index = _index + 1;
            return output;
        }

        private string Step(int count)
        {
            string output = "";
            for (int i = 0; i < count; i++)
            {
                output += _input[_index];
                _index = _index + 1;
            }
            return output;
        }

        private void StepBack()
        {
            if (_index > 0)
            {
                _index = _index - 1;
            }
        }

        private bool StepIf(char c)
        {
            if (!isEOF && Peek() == c)
            {
                Step();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}