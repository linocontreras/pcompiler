
namespace Lexing.Tokens
{
    public class TokenIdentifier : Token
    {
        public string Value { get; private set; }

        public TokenIdentifier(string value)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return $"[{nameof(TokenIdentifier)}] {this.Value}";
        }
    }
}
