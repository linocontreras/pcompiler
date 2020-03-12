namespace Lexer.Tokens {
    public class TokenReal : Token {
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