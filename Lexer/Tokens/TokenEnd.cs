namespace Lexer.Tokens {
    public class TokenEnd : Token {
        public override string ToString()
        {
            return $"[{nameof(TokenEnd)}]";
        }
    }
}