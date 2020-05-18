
namespace Lexing.Tokens
{
    public class TokenError : Symbol
    {
        public string Value { get; private set; }

        public TokenError(string value) : base(SymbolType.Error)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return $"[{nameof(TokenError)}] {this.Value}";
        }
    }

}