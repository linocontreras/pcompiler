namespace Lexing.Tokens {
    public class TokenConstant : Token {
        public override string ToString()
        {
            return $"[{nameof(TokenConstant)}]";
        }
    }
}