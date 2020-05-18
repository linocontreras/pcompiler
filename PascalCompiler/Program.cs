using System;
using System.IO;
using Parsing;
using Lexing;

namespace PascalCompiler
{
    class Program
    {
        static int Main(string[] args)
        {
            TextReader textReader;
            if (args.Length == 0) {
                textReader = Console.In;
            } else {
                textReader = new StreamReader(File.OpenRead(args[0]));
            }

            Lexer lexer = new Lexer(textReader);

            Parser parser = new Parser(lexer);

            try {
                parser.Parse();
                return 0;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return 1;
            }
        }
    }
}
