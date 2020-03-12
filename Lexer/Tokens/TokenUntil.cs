namespace Lexer.Tokens {
    public class TokenUntil : Token {
        public override string ToString()
        {
            return $"[{nameof(TokenUntil)}]";
        }
    }
}