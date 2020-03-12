namespace Lexer.Tokens {
    public class TokenProgram : Token {
        public override string ToString()
        {
            return $"[{nameof(TokenProgram)}]";
        }
    }
}