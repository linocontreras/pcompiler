using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Lexing.Tokens;

namespace Lexing
{
  public class Lexer
  {
    private TextReader textReader;

    private Dictionary<string, Symbol> keywords;

    private Dictionary<char, Symbol> oneChars;

    public int CurrentLine { get; private set; } = 1;

    public int CurrentCol { get; private set; } = 1;

    private int peek;

    private Symbol peekToken;

    public Symbol PeekToken() {
      if (peekToken == null) {
        this.peekToken = this.GetNextToken();
      }

      return this.peekToken;
    }
    public Lexer(TextReader textReader)
    {
      this.textReader = textReader;
      this.peek = this.textReader.Peek();

      this.keywords = new Dictionary<string, Symbol>();
      this.oneChars = new Dictionary<char, Symbol>();
      this.SetUpKeywords();
      this.SetUpOneChar();
    }

    private void SetUpKeywords()
    {
      this.keywords.Add("program", new TokenKeyword(SymbolType.Program));
      this.keywords.Add("constant", new TokenKeyword(SymbolType.Constant));
      this.keywords.Add("var", new TokenKeyword(SymbolType.Var));
      this.keywords.Add("begin", new TokenKeyword(SymbolType.Begin));
      this.keywords.Add("end", new TokenKeyword(SymbolType.End));
      this.keywords.Add("function", new TokenKeyword(SymbolType.Function));
      this.keywords.Add("while", new TokenKeyword(SymbolType.While));
      this.keywords.Add("do", new TokenKeyword(SymbolType.Do));
      this.keywords.Add("repeat", new TokenKeyword(SymbolType.Repeat));
      this.keywords.Add("until", new TokenKeyword(SymbolType.Until));
      this.keywords.Add("for", new TokenKeyword(SymbolType.For));
      this.keywords.Add("to", new TokenKeyword(SymbolType.To));
      this.keywords.Add("downto", new TokenKeyword(SymbolType.DownTo));
      this.keywords.Add("if", new TokenKeyword(SymbolType.If));
      this.keywords.Add("not", new TokenKeyword(SymbolType.Not));
      this.keywords.Add("then", new TokenKeyword(SymbolType.Then));
      this.keywords.Add("else", new TokenKeyword(SymbolType.Else));
      this.keywords.Add("writeln", new TokenKeyword(SymbolType.WriteLn));
      this.keywords.Add("readln", new TokenKeyword(SymbolType.ReadLn));
      this.keywords.Add("procedure", new TokenKeyword(SymbolType.Procedure));

      this.keywords.Add("boolean", new TokenType(TokenTypeEnum.Boolean));
      this.keywords.Add("integer", new TokenType(TokenTypeEnum.Integer));
      this.keywords.Add("real", new TokenType(TokenTypeEnum.Real));
      this.keywords.Add("string", new TokenType(TokenTypeEnum.String));

      this.keywords.Add("or", new TokenAddOperator(TokenAddOperatorEnum.Or));

      this.keywords.Add("div", new TokenMultOperator(TokenMultOperatorEnum.Div));
      this.keywords.Add("mod", new TokenMultOperator(TokenMultOperatorEnum.Mod));
      this.keywords.Add("and", new TokenMultOperator(TokenMultOperatorEnum.And));
    }

    private void SetUpOneChar()
    {
      this.oneChars.Add(';', new TokenPunctuation(SymbolType.SemiColon));
      this.oneChars.Add('.', new TokenPunctuation(SymbolType.Dot));
      this.oneChars.Add(',', new TokenPunctuation(SymbolType.Comma));

      this.oneChars.Add('(', new TokenPunctuation(SymbolType.LParen));
      this.oneChars.Add(')', new TokenPunctuation(SymbolType.RParen));

      this.oneChars.Add('=', new TokenRelational(TokenRelationalEnum.EQ));

      this.oneChars.Add('+', new TokenAddOperator(TokenAddOperatorEnum.Plus));
      this.oneChars.Add('-', new TokenAddOperator(TokenAddOperatorEnum.Minus));

      this.oneChars.Add('*', new TokenMultOperator(TokenMultOperatorEnum.Star));
      this.oneChars.Add('/', new TokenMultOperator(TokenMultOperatorEnum.Slash));
    }

    public Symbol GetNextToken()
    {
      if (this.peekToken != null) {
        Symbol result = this.peekToken;
        this.peekToken = null;
        return result;
      }

      this.ConsumeSpace();

      if (this.peek == -1)
      {
        return new TokenEOF();
      }

      this.oneChars.TryGetValue((char)this.peek, out Symbol oneChar);

      if (oneChar is Symbol)
      {
        this.Read();
        return oneChar;
      }

      if (this.peek == ':') {
        this.Read();

        if (this.peek == '=') {
          this.Read();
          return new TokenAssign();
        }

        return new TokenPunctuation(SymbolType.Colon);
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
        } while (char.IsLetterOrDigit((char)this.peek));

        this.keywords.TryGetValue(sb.ToString(), out Symbol keyword);
        return keyword ?? new TokenIdentifier(sb.ToString());
      }

      if (char.IsDigit((char)this.peek) || this.peek == '-')
      {
        int sign = 1;
        if (this.peek == '-')
        {
          this.Read();
          sign *= -1;
        }

        int integer = 0;
        do
        {
          integer *= 10;
          integer += int.Parse($"{(char)this.Read()}") * sign;
        } while (char.IsDigit((char)this.peek));
        
        if (this.peek != 'e' && this.peek != 'E' && this.peek != '.')
        {
          return new TokenInteger(integer);
        }
        else
        {
          if (this.peek == '.')
          {
            this.Read();

            if (char.IsDigit((char)this.peek))
            {
              int fraction = 0;

              do
              {
                fraction *= 10;
                fraction += int.Parse($"{(char)this.Read()}");
              } while (char.IsDigit((char)this.peek));

              if (this.peek != 'e' && this.peek != 'E')
              {
                return new TokenReal(integer, fraction, 0);
              }
              else
              {
                this.Read();

                int signExp = 1;
                if (this.peek == '-')
                {
                  this.Read();
                  signExp *= -1;
                }
                else if (this.peek == '+') this.Read();

                int exp = 0;

                do
                {
                  exp *= 10;
                  exp += int.Parse($"{(char)this.Read()}") * signExp;
                } while (char.IsDigit((char)this.peek));

                return new TokenReal(integer, fraction, exp);
              }
            }
          }
          else if (this.peek == 'e' || this.peek == 'E')
          {
            this.Read();

            int signExp = 1;
            if (this.peek == '-')
            {
              this.Read();
              signExp *= -1;
            }
            else if (this.peek == '+') this.Read();

            if (char.IsDigit((char)this.peek))
            {
              int exp = 0;

              do
              {
                exp *= 10;
                exp += int.Parse($"{(char)this.Read()}") * signExp;
              } while (char.IsDigit((char)this.peek));

              return new TokenReal(integer, 0, exp);
            }
          }
        }
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
