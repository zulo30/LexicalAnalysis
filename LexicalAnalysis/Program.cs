using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics.Contracts;

namespace LexicalAnalysis
{
    class Program
    {
        SymbolsTable Table;

        // constructor
        public Program(String data)
        {
            Table = JsonConvert.DeserializeObject<SymbolsTable>(data);
            var i = 0;
            foreach (SymbolsData symb in Table.SymbolsData)
            {
                symb.Number = i;
                i++;
            }
        }

        // Main Method
        public static void Main()
        {
            String Json = System.IO.File.ReadAllText("SymbolsTable.json");
            Program Program = new Program(Json);
            SymbolsTable Table = Program.Table;
            Program.WriteOnConsoleInfoMsg("Please enter the name of the file you want to read: ");
            string fileName = Console.ReadLine();
            //String[] lines = System.IO.File.ReadAllLines(fileName);

            String text = System.IO.File.ReadAllText(fileName);
            Program.WriteOnConsoleInfoMsg("Contents of " + fileName + " = ");

            var code = Program.FindTokensBySentences(text);
            System.Console.WriteLine();

            System.Console.WriteLine(text);
            var exit = false;
            while (!exit)
            {
                System.Console.WriteLine();
                Console.WriteLine("<<<<<<<<<<<<<<<<<<< MENU >>>>>>>>>>>>>>>");
                Console.WriteLine("\t 1). print the table of symbols");
                Console.WriteLine("\t 2). print the tokens table");
                Console.WriteLine("\t 3). show all the arithmetic expressions");
                Console.WriteLine("\t 4). analyze expression");
                Console.WriteLine("\t 5). re-organize the arithmetic expressions");
                Console.WriteLine("\t 6). read another file");
                Console.WriteLine("\t 7). exit");

                Console.WriteLine();
                Program.WriteOnConsoleInfoMsg("    Enter your choice:       ");


                var isNumeric = int.TryParse(Console.ReadLine(), out int choice);
                while (!isNumeric)
                {
                    Program.WriteOnConsoleInfoMsg("please enter a valid choice:  ");
                    isNumeric = int.TryParse(Console.ReadLine(), out choice);
                }
                switch (choice)
                {
                    case 1:
                        Program.PrintSymbolTable(code);
                        break;
                    case 2:
                        Program.PrintTokensTable(code);
                        break;
                    case 3:
                        Program.PrintArithmeticSentence(code);
                        break;
                    case 4:
                        Program.ExpressionAnalyzer(code);
                        break;
                    case 5:
                        Program.ReOrderExpressions();
                        break;
                    case 6:
                        code = Program.ReadAnotherFile();
                        break;
                    case 7:
                        exit = true;
                        break;
                    default:
                        Program.WriteOnConsoleInfoMsg("please enter a valid choice. ");
                        break;
                }
            }



        }


        // Methods

        private void ReOrderExpressions(){

            Console.WriteLine("<<<<<<<<<<<<<<<<<<< MENU >>>>>>>>>>>>>>>");
            Console.WriteLine("\t 1). Pre - Order");
            Console.WriteLine("\t 2). Post - Order");
            Console.WriteLine();
            WriteOnConsoleInfoMsg("    Enter your choice:       ");
            var isNumeric = int.TryParse(Console.ReadLine(), out int choice);
            while (!isNumeric)
            {
                WriteOnConsoleInfoMsg("please enter a valid choice:  ");
                isNumeric = int.TryParse(Console.ReadLine(), out choice);
            }
            switch (choice)
            {
                case 1:
                    PreOrder();
                    break;
                case 2:
                    PostOrder();
                    break;
                default:
                    WriteOnConsoleInfoMsg("please enter a valid choice. ");
                    break;
            }
        }

        private void PostOrder()
        {
            throw new NotImplementedException();
        }

        private void PreOrder()
        {
            throw new NotImplementedException();
        }

        private void ExpressionAnalyzer( List<Sentence> code)
        {
            ConsoleTable t = new ConsoleTable(50);
            t.PrintLine();
            List<List<char>> arit = GetArithmeticSentences(code);
            foreach (List<char> li in arit)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                t.PrintRow(Utilities.ConvertCharListToString(li));
                Console.ForegroundColor = ConsoleColor.White;
                StringAnalyzer sa = new StringAnalyzer(li);
                if(sa.IsAccepted)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" \t \t \t Is Accepted");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" \t \t \t Is Not Accepted");
                }
                Console.ForegroundColor = ConsoleColor.White;
                t.PrintLine();
            }
        }

        private List<Sentence> ReadAnotherFile()
        {
            WriteOnConsoleInfoMsg("Please enter the name of the file you want to read: ");
            string fileName = Console.ReadLine();
            //String[] lines = System.IO.File.ReadAllLines(fileName);


            String text = System.IO.File.ReadAllText(fileName);
            System.Console.WriteLine("Contents of " + fileName + " = ");

            var code = FindTokensBySentences(text);
            System.Console.WriteLine();

            System.Console.WriteLine(text);
            return code;
        }

        private void WriteOnConsoleInfoMsg(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(msg);
            Console.ForegroundColor = ConsoleColor.White;

        }

        public void PrintArithmeticSentence(List<Sentence> code)
        {
            code = LookForAlgebraicSentence(code);
            ConsoleTable t = new ConsoleTable(200);
            t.PrintLine();
            Console.ForegroundColor = ConsoleColor.Green;
            t.PrintRow("line","sentence#" ,"sentence");
            Console.ForegroundColor = ConsoleColor.White;
            t.PrintLine();
            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].IsAlgebraic)
                {
                    var l = Convert.ToString(code[i].Line);

                    t.PrintRow(l, Convert.ToString(i + 1), code[i].PrintSentence());
                }
            }

        }

        public void PrintSymbolTable(List<Sentence> code )
        {
     

            ConsoleTable t = new ConsoleTable(200);
            t.PrintLine();
            Console.ForegroundColor = ConsoleColor.Green;
            t.PrintRow("line", "sentence", "token#", "lexeme", "type", "description");
            Console.ForegroundColor = ConsoleColor.White;
            t.PrintLine();
            t.PrintLine();
            for (int i = 0; i < code.Count; i++)
            {
                var l = Convert.ToString(code[i].Line);
                foreach (String lex in code[i].Tokens)
                {
                    var data = Table.RetrieveData(lex);
                    var type = (data != null) ? data.Type[0] : "";
                    var desc = (data != null) ? data.Description : "";
                    var num = (data != null) ? Convert.ToString(data.Number) : "";
                    t.PrintRow(l, Convert.ToString(i + 1), num, lex, type, desc);
                    /*if (data != null){
                        for (i = 1; i < data.Type.Length; i++)
                        {
                            t.PrintRow("", "", "", "", data.Type[i], "");
                        }
                    }*/
                }
            }
        }

        public void PrintTokensTable(List<Sentence> code)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            ConsoleTable t = new ConsoleTable(100);
            t.PrintLine();
            Console.ForegroundColor = ConsoleColor.Green;
            t.PrintRow("token", "token#", "lexeme");
            Console.ForegroundColor = ConsoleColor.White;
            t.PrintLine();
            t.PrintLine();
            for (int i = 0; i < code.Count; i++)
            {
                var l = Convert.ToString(code[i].Line);
                foreach (String lex in code[i].Tokens)
                {
                    var data = Table.RetrieveData(lex);
                    var tok = (data != null) ? Convert.ToString(data.Name) : "";
                    var num = (data != null) ? Convert.ToString(data.Number) : "";
                    var lexeme= "'"+lex+"'";
                    t.PrintRow(tok,num,lexeme);
          
                }
            }

        }

        public void PrintSentences(List<List<string>> code)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            string[] titles = { " Sentence  ", "NumberOfTokens ", "IsAlgebraic" };

        }

        public int FindLineNumber()
        {
            return -1;
        }

        /*
         *look for all tokens in the program 
         *
         *
         */

        public List<List<string>> FindTokens(String[] lineText)
        {
            List<List<string>> code = new List<List<String>>();

            Queue aux = new Queue();
            for (int i = 0; i < lineText.Length; i++)
            {
                char[] charArray = lineText[i].ToCharArray();
                var li = Parsetokens(charArray, aux);
                code.Add(li);
            }
            return code;


        }

        /*
        *look for all tokens in the program 
        *
        */

        public List<List<string>> FindTokens(String text)
        {
            List<List<string>> code = new List<List<String>>();
            Queue aux = new Queue();
            char[] charArray = text.ToCharArray();
            code = ParsetokensOFASentences(charArray, aux, code);
            return code;

        }


        public List<Sentence> FindTokensBySentences(String text)
        {
            List<Sentence> code = new List<Sentence>();
            Queue aux = new Queue();
            char[] charArray = text.ToCharArray();
            code = ParsetokensOFASentences(charArray, aux, code);
            return code;

        }

        /*
         *check if a separator is a sentence separator
         *
         */

        private bool IsASentencesSeparator(char a)
        {
            string str = a.ToString();
            SymbolsData sd = Table.RetrieveData(str);
            bool ans = (sd != null) && sd.IsEnding;
            return ans;


        }

        private bool IsASpace(char a)
        {
            string str = a.ToString();
            SymbolsData sd = Table.RetrieveData(str);
            bool ans = (sd != null) && sd.IsIrrelevant;
            return ans;


        }


        /*
         *check if a separator is a sentence separator
         *
         */

        public bool IsAseparator(char a)
        {
            string str = a.ToString();
            string[] s = { "separador", "operadoraritmetico", "operadorrelacional", "operadoragrupacion" };
            SymbolsData sd = Table.RetrieveData(str);
            bool ans = ((sd != null) && ConcurType(s, sd)) ? true : false;
            return ans;
        }

        public bool IsAseparator(string str)
        {
            string[] s = { "separador", "operadoraritmetico", "operadorrelacional", "operadoragrupacion" };
            SymbolsData sd = Table.RetrieveData(str);
            bool ans = ((sd != null) && ConcurType(s, sd)) ? true : false;
            return ans;
        }

        /*
       *check if a separator is a sentence separator
       *
       */
        private bool ConcurType(string[] strs, SymbolsData sd)
        {
            bool ans = false;
            String[] types = sd.Type;
            foreach (string s in strs)
            {
                foreach (string t in types)
                {
                    ans |= (string.Compare(t, s, StringComparison.Ordinal) == 0);
                }
            }
            return ans;
        }

        /*
         *check if a separator is a sentence separator
         *
         */

        private static String QueueToString(Queue q)
        {
            Contract.Ensures(Contract.Result<string>() != null);

            var tmp = q.ToArray();
            var chars = new char[tmp.Length];
            for (int i = 0; i < tmp.Length; i++)
            {
                chars[i] = Convert.ToChar(tmp[i]);
            }
            var ans = new string(chars);
            q.Clear();
            q.TrimToSize();
            return ans;

        }

        /*
         *check if a separator is a sentence separator
         *
         */

        private List<String> Parsetokens(char[] charArray, Queue aux)
        {
            var li = new List<string>();

            for (int j = 0; j < charArray.Length; j++)
            {
                if (!IsAseparator(charArray[j]))
                {

                    aux.Enqueue(charArray[j]);
                }
                else
                {
                    if (aux.Count > 0)
                    {
                        String str = QueueToString(aux);
                        li.Add(str);
                    }
                    if (!IsASpace(charArray[j]))
                    {
                        li.Add(charArray[j].ToString());
                    }
                }

            }
            return li;
        }

        /*
         *check if a separator is a sentence separator
         *
         */
        private List<List<string>> ParsetokensOFASentences(char[] charArray, Queue aux, List<List<string>> code)
        {
            //bool SentenceCompleted = false;
            int j = 0;
            var li = new List<string>();

            while (j < charArray.Length)
            {
                if (!IsAseparator(charArray[j]))
                {

                    aux.Enqueue(charArray[j]);
                }
                else
                {

                    if (aux.Count > 0)
                    {
                        String str = QueueToString(aux);
                        if (IsRelevant(str) && !(string.Compare(str, " ", StringComparison.Ordinal) == 0))
                        {
                            li.Add(str);
                        }

                    }
                    if (!IsASpace(charArray[j]) && IsRelevant(charArray[j].ToString()))
                    {
                        string temp = Convert.ToString(charArray[j]) + Convert.ToString(charArray[j + 1]);
                        if (IsAseparator(temp))
                        {
                            li.Add(temp);
                            j++;
                        }
                        else
                        {
                            li.Add(charArray[j].ToString());
                        }

                    }
                    if (IsASentencesSeparator(charArray[j]) && li.Count != 0)
                    {
                        code.Add(li);
                        li = new List<string>();

                    }
                }
                j++;
            }

            return code;
        }

        private List<Sentence> ParsetokensOFASentences(char[] charArray, Queue aux, List<Sentence> code)
        {
            //bool SentenceCompleted = false;
            int j = 0;
            int sent = 1;
            var li = new Sentence();

            while (j < charArray.Length)
            {

                if (!IsAseparator(charArray[j]))
                {

                    aux.Enqueue(charArray[j]);
                }
                else
                {

                    if (aux.Count > 0)
                    {
                        String str = QueueToString(aux);
                        if (IsRelevant(str) && !(string.Compare(str, " ", StringComparison.Ordinal) == 0))
                        {
                            li.Add(str);
                        }

                    }
                    if (!IsASpace(charArray[j]) && IsRelevant(charArray[j].ToString()))
                    {

                        string temp = Convert.ToString(charArray[j]) + Convert.ToString(charArray[j + 1]);
                        if (IsAseparator(temp))
                        {
                            li.Add(temp);
                            j++;
                        }
                        else
                        {
                            li.Add(charArray[j].ToString());
                        }
                    }
                    if (IsASentencesSeparator(charArray[j]) && li.Tokens.Count != 0)
                    {

                        li.Line = sent;
                        code.Add(li);
                        li = new Sentence();

                    }
                }
                if (charArray[j] == '\n')
                {
                    sent++;
                }
                j++;

            }

            return code;
        }

        /*
         *check if a separator is a sentence separator
         *
         */
        private bool IsRelevant(string str)
        {
            SymbolsData sd = Table.RetrieveData(str);
            bool ans = (sd != null) && sd.IsIrrelevant;
            return !ans;
        }

        /*
         *check if a separator is a sentence separator
         *
         */
        public bool IsASpecialCharacter(string str)
        {
            bool ans = false;
            string[] specialChar = { "\n", "\t", "\"" };
            foreach (string s in specialChar)
            {
                ans |= (string.Compare(str, s, StringComparison.Ordinal) == 0);
            }
            return ans;
        }

        public List<Sentence> LookForAlgebraicSentence(List<Sentence> code)
        {
         
            foreach(Sentence s in code)
            {
                bool isAlg = false;
                foreach (String tok in s.Tokens){
                    isAlg |= IsAlgebraic(tok);
                }
                s.IsAlgebraic = isAlg;
            }

            return code;
        }

        public bool IsAlgebraic(string str)
        {

            SymbolsData sd = Table.RetrieveData(str);
            bool ans = (sd != null) && IsAlgebraic(sd);
            return ans;
        }

        public bool IsAlgebraic(SymbolsData sd)
        {
            var types = sd.Type;
            bool ans = false;
            if (types != null)
            {
                foreach (string t in types)
                {
                    ans |= (string.Compare("operadoraritmetico", t, StringComparison.Ordinal) == 0);
                }
            }
            return ans;
        }

        public List<List<char>> GetArithmeticSentences(List<Sentence> code )
        {
            List<List<char>> arithmeticSentences = new List<List<char>>();
            foreach (Sentence s in code )
            {
                if (s.IsAlgebraic) 
                {
                    arithmeticSentences.Add(s.ToCharList());
                }
            }
            return arithmeticSentences;

        }

    }
}
