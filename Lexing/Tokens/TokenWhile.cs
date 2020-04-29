namespace Lexing.Tokens {
    public class TokenWhile : Token {
        public override string ToString()
        {
            return $"[{nameof(TokenWhile)}]";
        }
    }
}