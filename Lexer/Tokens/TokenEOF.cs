
namespace Lexer.Tokens
{
    public class TokenEOF : Token
    {
        public TokenEOF()
        {

        }

        public override string ToString()
        {
            return $"[{nameof(TokenEOF)}]";
        }
    }
}
