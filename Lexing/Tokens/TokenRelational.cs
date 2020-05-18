using System;

namespace Lexing.Tokens {
    public enum TokenRelationalEnum {
        EQ, NE, LT, LE, GT, GE
    }
    public class TokenRelational : Symbol {
        public TokenRelationalEnum Value { get; private set; }

        public TokenRelational(TokenRelationalEnum tokenRelationalEnum) : base(SymbolType.Relational) {
            this.Value = tokenRelationalEnum;
        }

        public override string ToString()
        {
            return $"[{nameof(TokenRelational)}] {Enum.GetName(typeof(TokenRelationalEnum), this.Value)}";
        }
    }
}