using System;
using System.Collections.Generic;

namespace LexicalAnalysis
{
    public class StringAnalyzer
    {
        //main 
        //public static void Main()
        //{
        //    string str = "9-267";
        //    List<char> lst = new List<char>(str.ToCharArray());
        //    StringAnalyzer sa = new StringAnalyzer(lst);
        //    Console.WriteLine("Program ended");

        //}



        // fileds 
        public bool IsAccepted;
        private string Order;
        private int TapePosition;
        private List<int> errors;
        private List<char> AnalyzedString;
        private char EntryToken;

        // constructor
        public StringAnalyzer(List<char> AnalyzedString)
        {
            TapePosition = 0;
            errors = new List<int>();
            this.AnalyzedString = AnalyzedString;
            EntryToken = FirstToken();
            IsAccepted = true;
            Expression();
           

        }

        // methods 

        public void KeepLog(){}

        private void Expression()
        {
            if (EntryToken != '\0')
            {
                Term();
                ExpressionPrime();
            }
        }

        private void Term()
        {
            if (EntryToken != '\0')
            {
                Factor();
                TermPrime();
            }
        }

        private void TermPrime()
        {
            if (EntryToken != '\0')
            {
                if (EntryToken == '*')
                {
                    MakeMatch('*');
                    Factor();
                    TermPrime();
                }
                else
                {
                    if (EntryToken == '/')
                    {
                        MakeMatch('/');
                        Factor();
                        TermPrime();
                    }
                    else
                    {
                        //No hacer nada: Epsilon
                        //ShowError("Se esperaba un * / ");
                    }
                }
            }
        }

        private void Factor()
        {
            if (EntryToken != '\0')
            {
                if (IsNumeric(EntryToken))
                {
                    Number();
                }
                else
                {

                    if (EntryToken == '(')
                    {
                        MakeMatch('(');
                        Expression();
                        MakeMatch(')');
                    }
                    else
                    {
                        ShowError("Se esperaba un digito");
                    }
                }
            }
        }

        private void ExpressionPrime()
        {
            if (EntryToken != '\0')
            {
                if (EntryToken == '+')
                {
                    MakeMatch('+');
                    Term();
                    ExpressionPrime();

                }
                else
                {
                    if (EntryToken == '-')
                    {
                        MakeMatch('-');
                        Term();
                        ExpressionPrime();

                    }
                    else
                    {
                        //No hacer nada: Epsilon
                    }
                }
            }
        }

        private void Number()
        {
            if (EntryToken != '\0')
            {
                if (IsNumeric(EntryToken))
                {
                    Digit();
                    NumberPrime();
                }
                else
                {
                    //No hacer nada: Epsilon
                }
            }
        }

        private void NumberPrime()
        {
            if (EntryToken != '\0')
            {
                if (IsNumeric(EntryToken))
                {
                    Digit();
                    NumberPrime();
                }
                else
                {
                    //No hacer nada: Epsilon
                    if (EntryToken != '\0' && !IsAOperator(EntryToken))
                    {
                        ShowError("Se esperaba un digito");
                    }
                }
            }
        }

        internal bool IsNumeric(char expression)
        {
            List<char> number = new List<char>(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' });
            return number.Contains(expression);
        }

        private void Digit()
        {   
            if (IsNumeric(EntryToken))
            {
                MakeMatch(EntryToken);
            }
            else
            {
                ShowError("Se esperaba un digito");
               
            }
        }

        private void MakeMatch(char t)
        {

            if (t != EntryToken)
            {
                ShowError("Se esperaba un " + t);
            }
            else
            {
                WriteToken();
                if (TapePosition < AnalyzedString.Count)
                {

                    EntryToken = NextToken();
                }
                else
                    EntryToken = '\0';
            }
        }

        private char NextToken()
        {
            TapePosition++;
            return AnalyzedString[TapePosition - 1];
        }

        private char FirstToken()
        {
            TapePosition++;
            return AnalyzedString[0];
        }

        private void ShowError(string msg)
        {
            if (IsAccepted) IsAccepted = false;
            errors.Add(TapePosition);
            WriteToken();
            Console.WriteLine("ERROR en token: " + EntryToken + " " + msg);
            if (TapePosition < AnalyzedString.Count)
            {
                EntryToken = NextToken();
                Expression();
            }
        }

        private void WriteToken()
        {
            Console.WriteLine("Token: " + AnalyzedString[TapePosition - 1]);
        }

        internal bool IsAOperator(char expression)
        {
            List<char> operators = new List<char>(new char[] { '*', '/', '-', '+', '(', ')' });
            return operators.Contains(expression);
        }

}




}


