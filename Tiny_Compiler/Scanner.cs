using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

public enum Token_Class
{
    T_Number, T_String_Literal, T_Identifier,

    //Operators
    T_PlusOP, T_MinusOP, T_MulitplyOP, T_DivideOP, T_AssignOP, T_EqualOP, T_NotEqualOP,
    T_AndOP, T_OrOP, T_GreaterThanOP, T_SmallerThanOP, T_NotOP, 
    T_Semicolon, T_Dot, T_Comma, T_RightBracket, T_LeftBracket, T_LeftBrace, T_RightBrace, T_RightParentheses, T_LeftParentheses,


    //Reserved words
    T_Int, T_Float, T_String, T_Read, T_Write, T_Repeat, T_Until, T_If, T_ElseIf, T_Else, T_Then, T_Return, T_End
}
namespace Tiny_Compiler
{
    

    public class Token
    {
       public string lex;
       public Token_Class token_type;
    }

    public class Scanner
    {
        public List<Token> Tokens = new List<Token>();

        Dictionary<string, Token_Class> ReservedWords = new Dictionary<string, Token_Class>();
        Dictionary<string, Token_Class> Operators = new Dictionary<string, Token_Class>();

        public Scanner()
        {
            ReservedWords.Add("int", Token_Class.T_Int);
            ReservedWords.Add("float", Token_Class.T_Float);
            ReservedWords.Add("string", Token_Class.T_String);
            ReservedWords.Add("read", Token_Class.T_Read);
            ReservedWords.Add("write", Token_Class.T_Write);
            ReservedWords.Add("repeat", Token_Class.T_Repeat);
            ReservedWords.Add("until", Token_Class.T_Until);
            ReservedWords.Add("if", Token_Class.T_If);
            ReservedWords.Add("elseif", Token_Class.T_ElseIf);
            ReservedWords.Add("else", Token_Class.T_Else);
            ReservedWords.Add("then", Token_Class.T_Then);
            ReservedWords.Add("return", Token_Class.T_Return);
            ReservedWords.Add("end", Token_Class.T_End);


            //Operators.Add(".", Token_Class.T_Dot);
            Operators.Add(";", Token_Class.T_Semicolon);
            Operators.Add(",", Token_Class.T_Comma);
            Operators.Add("(", Token_Class.T_LeftParentheses);
            Operators.Add(")", Token_Class.T_RightParentheses);
            Operators.Add("[", Token_Class.T_LeftBracket);
            Operators.Add("]", Token_Class.T_RightBracket);
            Operators.Add("{", Token_Class.T_LeftBrace);
            Operators.Add("}", Token_Class.T_RightBrace);
            Operators.Add(">", Token_Class.T_GreaterThanOP);
            Operators.Add("<", Token_Class.T_SmallerThanOP);
            Operators.Add("<>", Token_Class.T_NotEqualOP);
            Operators.Add("!", Token_Class.T_NotOP);
            Operators.Add("-", Token_Class.T_MinusOP);
            Operators.Add("–", Token_Class.T_MinusOP);
            Operators.Add("+", Token_Class.T_PlusOP);
            Operators.Add("*", Token_Class.T_MulitplyOP);
            Operators.Add("/", Token_Class.T_DivideOP);
            Operators.Add(":=", Token_Class.T_EqualOP);
            Operators.Add("=", Token_Class.T_AssignOP);



        }

        public void StartScanning(string SourceCode)
        {
            for (int i = 0; i < SourceCode.Length; i++)
            {
                int j = i;
                char CurrentChar = SourceCode[i];
                string CurrentLexeme = CurrentChar.ToString();

                if (CurrentChar == ' ' || CurrentChar == '\r' || CurrentChar == '\n') //Whitespace
                    continue;

                if (CurrentChar >= 'A' && CurrentChar <= 'z') //Identifier lexeme
                {
                    j++;
                    while (j < SourceCode.Length)
                    {
                        if (SourceCode[j] >= '0' && SourceCode[j] <= '9' || SourceCode[j] >= 'A' && SourceCode[j] <= 'z')
                        {
                            CurrentLexeme += SourceCode[j].ToString();
                        }
                        else { break; }
                        j++;
                    }
                    FindTokenClass(CurrentLexeme);
                    i = j - 1;
                }

                else if (CurrentChar >= '0' && CurrentChar <= '9' || CurrentChar == '+' || CurrentChar == '-') //Number lexeme
                {
                    j++;
                    while (j < SourceCode.Length)
                    {
                        if (SourceCode[j] >= '0' && SourceCode[j] <= '9' || SourceCode[j] == '.')
                        {
                            CurrentLexeme += SourceCode[j].ToString();
                        }
                        else { break; }
                        j++;
                    }
                    FindTokenClass(CurrentLexeme);
                    i = j - 1;
                }
                else if (CurrentChar == '"') //String literal lexeme
                {
                    j++;
                    while (j < SourceCode.Length)
                    {
                        CurrentLexeme += SourceCode[j].ToString();
                        j++;
                        if (SourceCode[j - 1] == '"')
                        {
                            break;
                        }
             
                    }
                    FindTokenClass(CurrentLexeme.Trim());
                    i = j - 1;
                }
                else if (CurrentChar == '/') //Comment lexeme to disregard
                {
                    j++;
                    if (j < SourceCode.Length && SourceCode[j] == '*')
                    {
                        CurrentLexeme += SourceCode[j].ToString();
                        j++;
                        while (j < SourceCode.Length && SourceCode[j] != '*')
                        {
                            CurrentLexeme += SourceCode[j].ToString();
                            j++;
                        }
                        CurrentLexeme += "*";
                        j++;
                        if (j < SourceCode.Length && SourceCode[j] == '/')
                        {
                            CurrentLexeme += SourceCode[j].ToString();
                            j++;
                        }
                    }
                    else
                    {
                        //Division Operator:
                        FindTokenClass(CurrentLexeme);
                    }
                    
               
                    //FindTokenClass(CurrentLexeme);
                    i = j - 1;
                   
                }

                else //Operators lexeme
                {
                    //To handle assignment operator, because it is the only OP with two characters
                    if (CurrentChar == ':')
                    {
                        j++;
                        if (j< SourceCode.Length && SourceCode[j] == '=')
                        {
                            CurrentLexeme += SourceCode[j];
                        }
                        
                    }
                    else if (CurrentChar == '<')
                    {
                        j++;
                        if (j < SourceCode.Length && SourceCode[j] == '>' || SourceCode[j] == '=')
                        {
                            CurrentLexeme += SourceCode[j];
                        }

                    }
                    else if (CurrentChar == '>')
                    {
                        j++;
                        if (j < SourceCode.Length && SourceCode[j] == '=')
                        {
                            CurrentLexeme += SourceCode[j];
                        }

                    }

                    FindTokenClass(CurrentLexeme);
                    i = j;
                }
            }

            Tiny_Compiler.TokenStream = Tokens;
        }
        void FindTokenClass(string Lex)
        {
            Token_Class TC;
            Token Tok = new Token();
            Tok.lex = Lex;

            //Is it a string literal?
            if(isStringLiteral(Lex))
            {
                Tok.token_type = Token_Class.T_String_Literal;
                Tokens.Add(Tok);
            }
            //Is it a reserved word?
            else if (ReservedWords.ContainsKey(Lex))
            {
                Tok.token_type = ReservedWords[Lex];
                Tokens.Add(Tok);
            }
            //Is it an identifier?
            else if (isIdentifier(Lex))
            {
                Tok.token_type = Token_Class.T_Identifier;
                Tokens.Add(Tok);
            }
            //Is it a Constant?
            else if (isNumber(Lex))
            {
                Tok.token_type = Token_Class.T_Number;
                Tokens.Add(Tok);
            }
            //Is it an operator?
            else if (Operators.ContainsKey(Lex))
            {
                Tok.token_type = Operators[Lex];
                Tokens.Add(Tok);
            }
            //Is it an undefined?
            else
            {
                Errors.Error_List.Add(Lex);
            }
        }



        bool isIdentifier(string lex)
        {
            // Check if the lex is an identifier or not.

            if ((lex[0] >= 'A' && lex[0] <= 'z') || lex[0] == '_') 
            {
                for (int i = 1; i < lex.Length; i++)
                {
                    if ((lex[i] >= '0' && lex[i] <= '9' || (lex[i] >= 'A' && lex[i] <= 'z'))) continue;
                    else return false;
                }
                return true;
            }
            else 
            {
                return false;
            }
    

        }
        bool isNumber(string lex)
        {
            bool isValid = false;

            if (lex[0] == '+' || lex[0] == '-' ) // +, - are optional and they are accepted
            {
               lex = lex.Substring(1,lex.Length-1);
            }

            if (lex.Length > 0 && (lex[0] >= '0' && lex[0] <= '9')) //starts with a digit
            {
                isValid = true;
                for (int i = 1; i < lex.Length; i++)
                {
                    if ((lex[i] >= '0' && lex[i] <='9'))
                    {
                        continue;
                    }
                    else if (lex[i] == '.')
                    {
                        for (int j = i+1; j < lex.Length; j++)
                        {
                            if ((lex[j] >= '0' && lex[j] <= '9'))
                            {
                                continue;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else 
            {
                return false;
            }
            
        }

        bool isStringLiteral(string lex)
        {
            // Check if the lex is a String Literal or not.

            if (lex[0] == '"' && lex.Length > 1)
            {
                for (int i = 1; i < lex.Length; i++)
                {
                    if (lex[i] == '"')
                    {
                        for (int j = i + 1; j < lex.Length; j++)
                        { 
                            if(lex[j] == '"') return false;
                        }
                        return true;
                    }
                }
                return false;
            }
            else 
            {
                return false;
            }
            
        }
    }
}
