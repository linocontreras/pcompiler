namespace Lexing.Tokens {
    public class TokenString : Token {
        public string Value { get; private set; }

        public TokenString(string value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return $"[{nameof(TokenString)}] '{this.Value}'";
        }
    }
}