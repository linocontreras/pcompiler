namespace Parsing
{
    using System;
    using System.Collections.Generic;
    using Lexing;
    using Lexing.Tokens;

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
            this.SetUpProductions();
            this.SetUpActions();
        }

        private void SetUpActions() {
            this.actions[(0, SymbolType.Program)] = (Actions.Shift, 3);
            this.actions[(1, SymbolType.EOF)] = (Actions.Accept, 0);
            this.actions[(2, SymbolType.SemiColon)] = (Actions.Shift, 4);
            this.actions[(3, SymbolType.Identifier)] = (Actions.Shift, 5);
            this.actions[(4, SymbolType.Dot)] = (Actions.Reduce, 8);
            this.actions[(4, SymbolType.Constant)] = (Actions.Shift, 8);
            this.actions[(4, SymbolType.Var)] = (Actions.Reduce, 8);
            this.actions[(4, SymbolType.Begin)] = (Actions.Reduce, 8);
            this.actions[(5, SymbolType.SemiColon)] = (Actions.Reduce, 4);
            this.actions[(5, SymbolType.LParen)] = (Actions.Shift, 10);
            this.actions[(6, SymbolType.Dot)] = (Actions.Shift, 11);
            this.actions[(7, SymbolType.Var)] = (Actions.Shift, 13);
            this.actions[(7, SymbolType.Begin)] = (Actions.Reduce, 15);
            this.actions[(8, SymbolType.Identifier)] = (Actions.Shift, 15);
            this.actions[(9, SymbolType.SemiColon)] = (Actions.Reduce, 2);
            this.actions[(10, SymbolType.Identifier)] = (Actions.Shift, 18);
            this.actions[(11, SymbolType.EOF)] = (Actions.Reduce, 1);
            this.actions[(12, SymbolType.Begin)] = (Actions.Shift, 20);
            this.actions[(13, SymbolType.Identifier)] = (Actions.Shift, 18);
            this.actions[(14, SymbolType.SemiColon)] = (Actions.Shift, 23);
            this.actions[(15, SymbolType.Assign)] = (Actions.Shift, 24);
            this.actions[(16, SymbolType.RParen)] = (Actions.Shift, 25);
            this.actions[(17, SymbolType.RParen)] = (Actions.Reduce, 5);
            this.actions[(18, SymbolType.RParen)] = (Actions.Reduce, 22);
            this.actions[(18, SymbolType.Colon)] = (Actions.Reduce, 22);
            this.actions[(18, SymbolType.Comma)] = (Actions.Shift, 27);
            this.actions[(19, SymbolType.Dot)] = (Actions.Reduce, 6);
            this.actions[(20, SymbolType.SemiColon)] = (Actions.Reduce, 30);
            this.actions[(20, SymbolType.Identifier)] = (Actions.Shift, 27);
            this.actions[(20, SymbolType.Begin)] = (Actions.Shift, 40);
            this.actions[(20, SymbolType.End)] = (Actions.Reduce, 30);
            this.actions[(20, SymbolType.WriteLn)] = (Actions.Shift, 45);
            this.actions[(20, SymbolType.ReadLn)] = (Actions.Shift, 46);
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
                Symbol symbol = this.lexer.PeekToken();
                if (actions.TryGetValue((status, symbol.Type), out var action))
                {
                    switch (action.Item1)
                    {
                        case Actions.Shift:
                            this.symbols.Push(symbol);
                            this.stack.Push(action.Item2);
                            this.lexer.GetNextToken();
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
                    throw new Exception($"Syntax error near line {this.lexer.CurrentLine}. Unexpected {symbol}");
                }
            }
        }

        private void Reduce(int production)
        {
            int remove = this.productions[production];

            while (remove --> 0) {
                this.stack.Pop();
                this.symbols.Pop();
            }

            Production prod = new Production(production);
            this.symbols.Push(prod);

            if (this.goTos.TryGetValue((this.stack.Peek(), production), out int state)) {
                this.stack.Push(state);
            } else {
                throw new Exception($"Syntax error near line {this.lexer.CurrentLine}. Unexpected {prod}");
            }
        }
    }
}
