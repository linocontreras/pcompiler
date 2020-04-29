namespace Lexing.Tokens {
    public class TokenBegin : Token {
        public override string ToString()
        {
            return $"[{nameof(TokenBegin)}]";
        }
    }
}