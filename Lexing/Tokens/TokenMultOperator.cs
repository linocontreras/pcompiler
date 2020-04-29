using System;

namespace Lexing.Tokens {
    public enum TokenMultOperatorEnum {
        Star, Slash, Div, Mod, And
    }
    public class TokenMultOperator : Token {
        public TokenMultOperatorEnum Value { get; private set; }
        public TokenMultOperator(TokenMultOperatorEnum tokenTypeEnum) {
            this.Value = tokenTypeEnum;
        }
        public override string ToString()
        {
            return $"[{nameof(TokenMultOperator)}] {Enum.GetName(typeof(TokenMultOperatorEnum), this.Value)}";
        }
    }
}