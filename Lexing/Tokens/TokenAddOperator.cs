using System;

namespace Lexing.Tokens {
    public enum TokenAddOperatorEnum {
        Plus, Minus, Or
    }
    public class TokenAddOperator : Symbol {
        public TokenAddOperatorEnum Value { get; private set; }

        public TokenAddOperator(TokenAddOperatorEnum tokenTypeEnum) : base(SymbolType.AddOperator)
        {
            this.Value = tokenTypeEnum;
        }

        public override string ToString()
        {
            return $"[{nameof(TokenAddOperator)}] {Enum.GetName(typeof(TokenAddOperatorEnum), this.Value)}";
        }
    }
}