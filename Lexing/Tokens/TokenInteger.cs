namespace Lexing.Tokens {
    public class TokenInteger : Symbol {
        public int Value { get; private set; }

        public TokenInteger(int value) : base(SymbolType.Integer)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return $"[{nameof(TokenInteger)}] {this.Value}";
        }
    }
}