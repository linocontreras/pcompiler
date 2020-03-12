using System.Collections.Generic;
using System.IO;
using System.Text;
using Lexer.Tokens;

namespace Lexer
{
    public class Lexer
    {
        private TextReader textReader;

        private Dictionary<string, Token> keywords;

        public int CurrentLine { get; private set; } = 1;

        public int CurrentCol { get; private set; } = 1;
        public Lexer(TextReader textReader)
        {
            this.textReader = textReader;
            this.keywords = new Dictionary<string, Token>();
        }

        private void SetUpKeywords()
        {
            this.keywords.Add("program", new TokenProgram());
            this.keywords.Add("begin", new TokenBegin());
            this.keywords.Add("end", new TokenEnd());
        }

        public Token GetNextToken()
        {
            this.ConsumeSpace();

            if (this.Peek() == -1)
            {
                return new TokenEOF();
            }

            if (this.Peek() == ';')
            {
                this.Read();
                return new TokenSemicolon();
            }

            if (this.Peek() == '.')
            {
                this.Read();
                return new TokenDot();
            }

            if (this.Peek() == ',')
            {
                this.Read();
                return new TokenComma();
            }

            if (this.Peek() == '(')
            {
                this.Read();
                return new TokenLParen();
            }

            if (this.Peek() == ')')
            {
                this.Read();
                return new TokenRParen();
            }

            if (char.IsLetter((char)this.Peek()))
            {
                StringBuilder sb = new StringBuilder();

                do
                {
                    sb.Append((char)this.Read());
                } while (char.IsLetter((char)this.Peek()));

                this.keywords.TryGetValue(sb.ToString(), out Token keyword);
                return keyword ?? new TokenIdentifier(sb.ToString());
            }

            return new TokenError($"({this.CurrentLine}, {this.CurrentCol}) unexpected: " + (char)this.Read());
        }

        private int Peek()
        {
            return this.textReader.Peek();
        }

        private int Read()
        {
            this.CurrentCol++;
            return this.textReader.Read();
        }

        private void ConsumeSpace()
        {
            while (true)
            {
                switch (this.Peek())
                {
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
}
