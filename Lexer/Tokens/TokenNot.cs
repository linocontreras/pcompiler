namespace Lexer.Tokens {
    public class TokenNot : Token {
        public override string ToString()
        {
            return $"[{nameof(TokenNot)}]";
        }
    }
}