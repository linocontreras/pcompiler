
namespace Lexing.Tokens
{
    using System;
    
    public abstract class Symbol { 
        public SymbolType Type { get; private set; }

        protected Symbol(SymbolType type) {
            this.Type = type;
        }


        public override string ToString()
        {
            return $"[{Enum.GetName(typeof(SymbolType), this.Type)}]";
        }
    }

}