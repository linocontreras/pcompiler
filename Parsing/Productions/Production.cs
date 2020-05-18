namespace Parsing {
    using Lexing.Tokens;

    public class Production : Symbol {
        public int Id { get; private set; }
        public Production(int number) : base(SymbolType.Production) {
            this.Id = number;
        }
    }
}