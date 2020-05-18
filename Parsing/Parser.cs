using System;
using System.Collections.Generic;
using Lexing;
using Lexing.Tokens;

namespace Syntaxer
{
    enum Actions { Shift, Reduce, Accept }
    public class Parser
    {
        private Dictionary<(int, SymbolType), (Actions, int)> actions = new Dictionary<(int, SymbolType), (Actions, int)>();

        private Dictionary<(int, int), int> goTos = new Dictionary<(int, int), int>();
        private Dictionary<int, int> productions = new Dictionary<int, int>();

        private Stack<int> stack = new Stack<int>(new int[] { 0 });

        private Stack<Symbol> symbols = new Stack<Symbol>();
        private Lexer lexer;
        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
        }

        private void SetUpProductions() {
            this.productions[0] = 1;
            this.productions[1] = 4;
            this.productions[2] = 3;
            this.productions[3] = 4;
            this.productions[4] = 0;
            this.productions[5] = 1;
            this.productions[6] = 3;
            this.productions[7] = 4;
            this.productions[8] = 0;
            this.productions[9] = 3;
            this.productions[10] = 0;
            this.productions[11] = 4;
            this.productions[12] = 0;
            this.productions[13] = 3;
            this.productions[14] = 0;
            this.productions[15] = 3;
            this.productions[16] = 4;
            this.productions[17] = 2;
            this.productions[18] = 3;
            this.productions[19] = 0;
            this.productions[20] = 2;
            this.productions[21] = 3;
            this.productions[22] = 0;
            this.productions[23] = 1;
            this.productions[24] = 1;
            this.productions[25] = 1;
            this.productions[26] = 1;
            this.productions[27] = 0;
            this.productions[28] = 3;
            this.productions[29] = 1;
            this.productions[30] = 1;
            this.productions[31] = 4;
            this.productions[32] = 1;
            this.productions[33] = 0;
            this.productions[34] = 2;
            this.productions[35] = 3;
            this.productions[36] = 0;
            this.productions[37] = 1;
            this.productions[38] = 1;
            this.productions[39] = 1;
            this.productions[40] = 2;
            this.productions[41] = 0;
            this.productions[42] = 3;
            this.productions[43] = 1;
            this.productions[44] = 1;
            this.productions[45] = 1;
            this.productions[46] = 4;
            this.productions[47] = 1;
            this.productions[48] = 1;
            this.productions[49] = 1;
            this.productions[50] = 4;
            this.productions[51] = 4;
            this.productions[52] = 8;
            this.productions[53] = 1;
            this.productions[54] = 1;
            this.productions[55] = 5;
            this.productions[56] = 2;
            this.productions[57] = 0;
            this.productions[58] = 2;
            this.productions[59] = 2;
            this.productions[60] = 0;
            this.productions[61] = 3;
            this.productions[62] = 1;
            this.productions[63] = 0;
            this.productions[64] = 3;
            this.productions[65] = 0;
            this.productions[66] = 2;
            this.productions[67] = 3;
            this.productions[68] = 0;
            this.productions[69] = 1;
            this.productions[70] = 1;
            this.productions[71] = 1;
            this.productions[72] = 3;
            this.productions[73] = 2;
        }

        public bool Parse()
        {
            while (true)
            {
                int status = this.stack.Peek();
                Lexing.Tokens.Symbol token = this.lexer.PeekToken();
                if (actions.TryGetValue((status, token.Type), out var action))
                {
                    switch (action.Item1)
                    {
                        case Actions.Shift:
                            this.symbols.Push(token);
                            this.stack.Push(action.Item2);
                            break;

                        case Actions.Reduce:
                            this.Reduce(action.Item2);
                            break;

                        case Actions.Accept:
                            return true;
                    }
                }
                else
                {
                    throw new Exception("Syntax error. Unexpected " + token);
                }
            }
        }

        private void Reduce(int production)
        {
            int remove = this.productions[production];

            while (remove --> 0) {
                this.stack.Pop();
            }
        }

        private void GoTo(int state)
        {

        }
    }
}
