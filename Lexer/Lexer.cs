using System;
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

        private Dictionary<char, Token> oneChars;

        public int CurrentLine { get; private set; } = 1;

        public int CurrentCol { get; private set; } = 1;
        public Lexer(TextReader textReader)
        {
            this.textReader = textReader;
            this.keywords = new Dictionary<string, Token>();
            this.oneChars = new Dictionary<char, Token>();
            this.SetUpKeywords();
            this.SetUpOneChar();
        }

        private void SetUpKeywords()
        {
            this.keywords.Add("program", new TokenProgram());
            this.keywords.Add("constant", new TokenConstant());
            this.keywords.Add("var", new TokenVar());
            this.keywords.Add("begin", new TokenBegin());
            this.keywords.Add("end", new TokenEnd());
            this.keywords.Add("function", new TokenFunction());
            this.keywords.Add("while", new TokenWhile());
            this.keywords.Add("do", new TokenDo());
            this.keywords.Add("repeat", new TokenRepeat());
            this.keywords.Add("until", new TokenUntil());
            this.keywords.Add("for", new TokenFor());
            this.keywords.Add("to", new TokenTo());
            this.keywords.Add("downto", new TokenDownTo());
            this.keywords.Add("if", new TokenIf());
            this.keywords.Add("then", new TokenThen());
            this.keywords.Add("else", new TokenElse());
            this.keywords.Add("writeln", new TokenWriteLn());
            this.keywords.Add("readln", new TokenReadLn());
        }

        private void SetUpOneChar()
        {
            this.oneChars.Add(';', new TokenSemicolon());
            this.oneChars.Add('.', new TokenDot());
            this.oneChars.Add(',', new TokenComma());

            this.oneChars.Add('(', new TokenLParen());
            this.oneChars.Add(')', new TokenRParen());
        }

        public Token GetNextToken()
        {
            this.ConsumeSpace();

            if (this.Peek() == -1)
            {
                return new TokenEOF();
            }

            this.oneChars.TryGetValue((char)this.Peek(), out Token oneChar);

            if (oneChar is Token)
            {
                this.Read();
                return oneChar;
            }

            if ((char)this.Peek() == '\'')
            {
                this.Read();
                StringBuilder stringValue = new StringBuilder();

                do {
                    stringValue.Append((char)this.Read());
                } while((char)this.Peek() != '\'');

                this.Read();
                return new TokenString(stringValue.ToString());
            }

            if (char.IsLetter((char)this.Peek()))
            {
                StringBuilder sb = new StringBuilder();

                do
                {
                    sb.Append(char.ToLower((char)this.Read()));
                } while (char.IsLetterOrDigit((char)this.Peek()) || (char)this.Peek() == '_');

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
