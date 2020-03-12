namespace Lexer.Tokens {
    public class TokenAssign : Token {
        public override string ToString()
        {
            return $"[{nameof(TokenAssign)}]";
        }
    }
}