using System;

namespace Lexer.Tokens {
    public enum TokenRelationalEnum {
        EQ, NE, LT, LE, GT, GE
    }
    public class TokenRelational : Token {
        public TokenRelationalEnum Value { get; private set; }

        public TokenRelational(TokenRelationalEnum tokenRelationalEnum) {
            this.Value = tokenRelationalEnum;
        }

        public override string ToString()
        {
            return $"[{nameof(TokenRelational)}] {Enum.GetName(typeof(TokenRelationalEnum), this.Value)}";
        }
    }
}