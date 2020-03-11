using System;
using System.IO;
using System.Text;

namespace Lexer
{
    class Lexer {
        private StreamReader streamReader;

        public int CurrentLine { get; private set; } = 1;

        public int CurrentCol { get; private set; } = 1;
        public Lexer(StreamReader streamReader) {
            this.streamReader = streamReader;
        }

        public string nextToken() {
            while((char)this.streamReader.Peek() == ' ' || (char)this.streamReader.Peek() == '\n') {
                this.streamReader.Read();
            }

            if (this.streamReader.Peek() == -1) {
                return "EOF";
            } 

            if (char.IsLetter((char)this.streamReader.Peek())) {
                StringBuilder sb = new StringBuilder();

                do {
                    sb.Append((char)this.streamReader.Read());
                } while(char.IsLetter((char)this.streamReader.Peek()));

                return sb.ToString();
            }

            return $"Error (${this.CurrentLine}, ${this.CurrentCol}) unexpected: " + (char)this.streamReader.Read();
        }

        private void ConsumeSpace() {
            switch ((char)this.streamReader.Peek()) {
                case ' ':
                    this.streamReader.Read();
                    break;
                case '\n':
                    this.streamReader.Read();
                    break;
                default:
                    return;
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Lexer lexer = new Lexer(new StreamReader(Console.OpenStandardInput()));
            string token;
            while((token = lexer.nextToken()) != "EOF") {
                Console.WriteLine(token);
            }
        }
    }
}
