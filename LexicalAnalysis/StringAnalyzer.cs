using System;
using System.Collections.Generic;

namespace LexicalAnalysis
{


    //TODO: [DOCS] write the corresponding documentation
    /// <summary>
    /// 
    /// </summary>

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



        // fields 
        public bool IsAccepted;
        private string Order;
        private int TapePosition;
        private List<int> errors;
        private List<char> AnalyzedString;
        private List<List<char>> GeneratedStrings;
        private char EntryToken;

        // constructor
        public StringAnalyzer(List<char> AnalyzedString)
        {
            TapePosition = 0;
            errors = new List<int>();
            this.AnalyzedString = AnalyzedString;
            GeneratedStrings = new List<List<char>>();
            for (int i = 0; i < 3; i++)
                GeneratedStrings.Add(new List<char>());
            EntryToken = FirstToken();
            IsAccepted = true;
            Expression();
           

        }

        // methods 

        public void KeepLog(){}

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>
        
        private void Expression()
        {
            if (EntryToken != '\0')
            {
                Term();
                ExpressionPrime();
            }
        }

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>

        private void Term()
        {
            if (EntryToken != '\0')
            {
                Factor();
                TermPrime();
            }
        }

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>
        
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

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>
        
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

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>
        
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

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>

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


        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>
        
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

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>

        internal bool IsNumeric(char expression)
        {
            List<char> number = new List<char>(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' });
            return number.Contains(expression);
        }

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>

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

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>

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

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        private char NextToken()
        {
            TapePosition++;
            return AnalyzedString[TapePosition - 1];
        }

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        private char FirstToken()
        {
            TapePosition++;
            return AnalyzedString[0];
        }

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>

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

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>

        private void WriteToken()
        {
            //Console.WriteLine("Token: " + AnalyzedString[TapePosition - 1]);
        }

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>

        internal bool IsAOperator(char expression)
        {
            List<char> operators = new List<char>(new char[] { '*', '/', '-', '+', '(', ')' });
            return operators.Contains(expression);
        }

}




}


