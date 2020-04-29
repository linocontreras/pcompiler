namespace Lexing.Tokens {
    public class TokenNot : Token {
        public override string ToString()
        {
            return $"[{nameof(TokenNot)}]";
        }
    }
}