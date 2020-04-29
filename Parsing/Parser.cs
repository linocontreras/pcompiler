using System.Collections.Generic;
using Lexing;
using Lexing.Tokens;

namespace Syntaxer
{
    public class Parser
    {   
        private Lexer lexer;
        public Parser(Lexer lexer) {
            this.lexer = lexer;
        }

        public void Parse() {
            Token peak = this.lexer.PeekToken();
        }
    }
}
