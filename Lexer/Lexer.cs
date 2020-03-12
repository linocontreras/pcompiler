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

        private int peek;
        public Lexer(TextReader textReader)
        {
            this.textReader = textReader;
            this.peek = this.textReader.Peek();

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
            this.keywords.Add("not", new TokenNot());
            this.keywords.Add("then", new TokenThen());
            this.keywords.Add("else", new TokenElse());
            this.keywords.Add("writeln", new TokenWriteLn());
            this.keywords.Add("readln", new TokenReadLn());

            this.keywords.Add("boolean", new TokenType(TokenTypeEnum.Boolean));
            this.keywords.Add("integer", new TokenType(TokenTypeEnum.Integer));
            this.keywords.Add("real", new TokenType(TokenTypeEnum.Real));
            this.keywords.Add("string", new TokenType(TokenTypeEnum.String));
        }

        private void SetUpOneChar()
        {
            this.oneChars.Add(':', new TokenColon());
            this.oneChars.Add(';', new TokenSemicolon());
            this.oneChars.Add('.', new TokenDot());
            this.oneChars.Add(',', new TokenComma());

            this.oneChars.Add('(', new TokenLParen());
            this.oneChars.Add(')', new TokenRParen());

            this.oneChars.Add('=', new TokenRelational(TokenRelationalEnum.EQ));
        }

        public Token GetNextToken()
        {
            this.ConsumeSpace();

            if (this.peek == -1)
            {
                return new TokenEOF();
            }

            this.oneChars.TryGetValue((char)this.peek, out Token oneChar);

            if (oneChar is Token)
            {
                this.Read();
                return oneChar;
            }

            if (this.peek == '<')
            {
                this.Read();

                if (this.peek == '=')
                {
                    this.Read();

                    return new TokenRelational(TokenRelationalEnum.LE);
                }
                else if (this.peek == '>')
                {
                    this.Read();

                    return new TokenRelational(TokenRelationalEnum.NE);
                }
                else
                {
                    return new TokenRelational(TokenRelationalEnum.LT);
                }
            }

            if (this.peek == '>')
            {
                this.Read();

                if (this.peek == '=')
                {
                    this.Read();

                    return new TokenRelational(TokenRelationalEnum.GE);
                }
                else
                {
                    return new TokenRelational(TokenRelationalEnum.GT);
                }
            }

            if ((char)this.peek == '\'')
            {
                this.Read();
                StringBuilder stringValue = new StringBuilder();

                do
                {
                    stringValue.Append((char)this.Read());
                } while ((char)this.peek != '\'');

                this.Read();
                return new TokenString(stringValue.ToString());
            }

            if (char.IsLetter((char)this.peek))
            {
                StringBuilder sb = new StringBuilder();

                do
                {
                    sb.Append(char.ToLower((char)this.Read()));
                } while (char.IsLetterOrDigit((char)this.peek) || (char)this.peek == '_');

                this.keywords.TryGetValue(sb.ToString(), out Token keyword);
                return keyword ?? new TokenIdentifier(sb.ToString());
            }

            if (char.IsDigit((char)this.peek) || this.peek == '-') {
                int sign = 1;
                if (this.peek == '-') sign *= -1;

                int integer = 0;
                do {
                    integer *= 10;
                    integer += int.Parse($"{this.Read()}") * sign;
                } while(char.IsDigit((char)this.peek));

                return new TokenInteger(integer);
            }

            return new TokenError($"({this.CurrentLine}, {this.CurrentCol}) unexpected: " + (char)this.Read());
        }

        private int Read()
        {
            this.CurrentCol++;
            int res = this.textReader.Read();
            this.peek = this.textReader.Peek();
            return res;
        }

        private void ConsumeSpace()
        {
            while (true)
            {
                switch (this.peek)
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
