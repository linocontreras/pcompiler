namespace Lexing.Tokens {
    public class TokenReal : Symbol {
        public double Value { get; private set; }

        public TokenReal(int integer, int fraction, int exp)
        {
            this.Value = double.Parse($"{integer}.{fraction}e{exp}");
        }

        public override string ToString()
        {
            return $"[{nameof(TokenReal)}] {this.Value}";
        }
    }
}