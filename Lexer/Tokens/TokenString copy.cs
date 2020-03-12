namespace Lexer.Tokens {
    public class TokenInteger : Token {
        public int Value { get; private set; }

        public TokenInteger(int value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return $"[{nameof(TokenInteger)}] {this.Value}";
        }
    }
}