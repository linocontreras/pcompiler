namespace Lexer.Tokens {
    public class TokenWriteLn : Token {
        public override string ToString()
        {
            return $"[{nameof(TokenWriteLn)}]";
        }
    }
}