using System;

namespace Lexing.Tokens {
    public enum TokenMultOperatorEnum {
        Star, Slash, Div, Mod, And
    }
    public class TokenMultOperator : Symbol {
        public TokenMultOperatorEnum Value { get; private set; }
        public TokenMultOperator(TokenMultOperatorEnum tokenTypeEnum) : base(SymbolType.MultOperator) {
            this.Value = tokenTypeEnum;
        }
        public override string ToString()
        {
            return $"[{nameof(TokenMultOperator)}] {Enum.GetName(typeof(TokenMultOperatorEnum), this.Value)}";
        }
    }
}