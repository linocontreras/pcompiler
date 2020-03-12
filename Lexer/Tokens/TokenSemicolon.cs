namespace Lexer.Tokens {
    public class TokenSemicolon : Token {
        public override string ToString()
        {
            return $"[{nameof(TokenSemicolon)}]";
        }
    }
}