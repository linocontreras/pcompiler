namespace Lexing.Tokens
{
    public class TokenKeyword : Symbol
    {
        public TokenKeyword(SymbolType type) : base(type) {
            
        }

        public override string ToString()
        {
            return $"[Token{nameof(this.Type)}]";
        }
    }
}