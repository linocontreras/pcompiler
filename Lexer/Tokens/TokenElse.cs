namespace Lexer.Tokens {
    public class TokenElse : Token {
        public override string ToString()
        {
            return $"[{nameof(TokenElse)}]";
        }
    }
}