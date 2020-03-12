using System;

namespace Lexer.Tokens {
    public enum TokenTypeEnum {
        Integer, Real, Boolean, String
    }
    public class TokenType : Token {
        public TokenTypeEnum Value { get; private set; }
        public TokenType(TokenTypeEnum tokenTypeEnum) {
            this.Value = tokenTypeEnum;
        }
        public override string ToString()
        {
            return $"[{nameof(TokenType)}] {Enum.GetName(typeof(TokenTypeEnum), this.Value)}";
        }
    }
}