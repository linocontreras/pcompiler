using System;
using System.Collections.Generic;
using Lexing;
using Lexing.Tokens;

namespace Syntaxer
{
    enum Actions { Shift, Reduce, Accept }
    public class Parser
    {
        private delegate void Action(int stack);
        private Dictionary<(int, Type), (Actions, int)> actions = new Dictionary<(int, Type), (Actions, int)>();

        private Dictionary<(int, Type), int> goTos = new Dictionary<(int, Type), int>();
        private Dictionary<int, int> productions = new Dictionary<int, int>();

        private Stack<int> stack = new Stack<int>(new int[] { 0 });

        private Stack<Symbol> symbols = new Stack<Symbol>();
        private Lexer lexer;
        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
        }

        public bool Parse()
        {
            while (true)
            {
                int status = this.stack.Peek();
                Lexing.Tokens.Symbol token = this.lexer.PeekToken();
                if (actions.TryGetValue((status, token), out var action))
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

    class SymbolComparer {
        public bool Equals((int, Symbol) a, (int, Symbol) b) {
            return a.Item1 == b.Item1 && b.Item2 is b.Item2.GetType();
        }
    }
}
