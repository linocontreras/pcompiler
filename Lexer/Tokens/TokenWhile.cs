namespace Lexer.Tokens {
    public class TokenWhile : Token {
        public override string ToString()
        {
            return $"[{nameof(TokenWhile)}]";
        }
    }
}