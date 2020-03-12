namespace Lexer.Tokens {
    public class TokenRepeat : Token {
        public override string ToString()
        {
            return $"[{nameof(TokenRepeat)}]";
        }
    }
}