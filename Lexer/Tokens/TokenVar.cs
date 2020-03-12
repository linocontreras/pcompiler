namespace Lexer.Tokens {
    public class TokenVar : Token {
        public override string ToString()
        {
            return $"[{nameof(TokenVar)}]";
        }
    }
}