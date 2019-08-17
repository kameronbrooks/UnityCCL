namespace CCL
{
    internal interface ILexer
    {
        Token[] Tokenize(string input);
    }
}