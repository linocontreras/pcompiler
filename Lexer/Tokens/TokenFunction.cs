namespace Lexer.Tokens {
    public class TokenFunction : Token {
        public override string ToString()
        {
            return $"[{nameof(TokenFunction)}]";
        }
    }
}