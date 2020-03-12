namespace Lexer.Tokens {
    public class TokenConstant : Token {
        public override string ToString()
        {
            return $"[{nameof(TokenConstant)}]";
        }
    }
}