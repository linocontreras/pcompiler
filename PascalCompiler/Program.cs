using System;
using System.Text;
using System.IO;
using Lexing;
using Lexing.Tokens;

namespace PascalCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            TextReader textReader;
            if (args.Length == 0) {
                textReader = Console.In;
            } else {
                textReader = new StreamReader(File.OpenRead(args[0]));
            }

            Lexer lexer = new Lexer(textReader);

            Token token;

            do {
                token = lexer.GetNextToken();
                Console.WriteLine(token);
            } while (!(token is TokenEOF));

        }
    }
}
