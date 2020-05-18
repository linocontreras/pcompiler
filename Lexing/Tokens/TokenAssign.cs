namespace Lexing.Tokens {
    public class TokenAssign : Symbol {
        public TokenAssign() : base(SymbolType.Assign) { }

        public override string ToString()
        {
            return $"[{nameof(TokenAssign)}]";
        }
    }
}