namespace Lexing.Tokens {
    public class TokenString : Symbol {
        public string Value { get; private set; }

        public TokenString(string value) : base(SymbolType.String)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return $"[{nameof(TokenString)}] '{this.Value}'";
        }
    }
}