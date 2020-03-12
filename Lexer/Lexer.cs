using System;
using System.IO;
using System.Text;
using Lexer.Tokens;

namespace Lexer
{
    class Lexer {
        private TextReader textReader;

        public int CurrentLine { get; private set; } = 1;

        public int CurrentCol { get; private set; } = 1;
        public Lexer(TextReader textReader) {
            this.textReader = textReader;
        }

        public Token GetNextToken() {
            this.ConsumeSpace();

            if (this.textReader.Peek() == -1) {
                return new TokenEOF();
            }

            if (char.IsLetter((char)this.Peek())) {
                StringBuilder sb = new StringBuilder();

                do {
                    sb.Append((char)this.Read());
                } while(char.IsLetter((char)this.Peek()));

                return new TokenIdentifier(sb.ToString());
            }

            return new TokenError($"({this.CurrentLine}, {this.CurrentCol}) unexpected: " + (char)this.Read());
        }

        private int Peek() {
            return this.textReader.Peek();
        }

        private int Read() {
            this.CurrentCol++;
            return this.textReader.Read();
        }

        private void ConsumeSpace() {
            while (true) {
                switch (this.Peek()) {
                    case ' ':
                    case '\t':
                        this.Read();
                        break;
                    case '\n':
                        this.CurrentLine++;
                        this.CurrentCol = 0;
                        this.Read();
                        break;
                    default:
                        return;
                }
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Lexer lexer = new Lexer(Console.In);

            Token token;

            do {
                token = lexer.GetNextToken();
                Console.WriteLine(token);
            } while (!(token is TokenEOF));

        }
    }
}
