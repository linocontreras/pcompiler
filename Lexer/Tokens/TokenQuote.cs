namespace Lexer.Tokens {
    public class TokenQuote : Token {
        public override string ToString()
        {
            return $"[{nameof(TokenQuote)}]";
        }
    }
}