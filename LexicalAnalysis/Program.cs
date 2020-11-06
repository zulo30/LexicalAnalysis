using System;
using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json;

using System.Diagnostics.Contracts;

namespace LexicalAnalysis
{
    /// <summary>
    ///     This is main class of the program where all the logic of the application is store
    ///     the program class has an field table of type SymbolsTable <see cref="SymbolsTable"/>
    /// </summary>
    
    class Program
    {
        readonly SymbolsTable Table;

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
        /// <summary>
        ///     The Main Method is infinite loop that runs until you choose to exit the application
        ///     it displays the main menu where yoy can choose the feature you want to use,
        /// </summary>
        
        // Main Method
        public static void Main()
        {
            string Json = System.IO.File.ReadAllText("../../../SymbolsTable.json");
            Program Program = new Program(Json);
            _ = Program.Table;


            //String[] lines = System.IO.File.ReadAllLines(fileName);
            string fileName= "";
            bool fileIsPending = true;
            string text = "";
            while (fileIsPending)
            {
                Program.WriteOnConsoleInfoMsg("Please enter the name of the file you want to read: ");
                fileName = Console.ReadLine();
                
                try {
                  text = System.IO.File.ReadAllText(fileName);
                    fileIsPending = false;
                }
                catch (System.IO.FileNotFoundException)
                {
                    Program.WriteOnConsoleErrorMsg("The file was not found, please try one more time..");
                    Console.WriteLine();
                    fileIsPending = true;
                }
            }

            Program.WriteOnConsoleInfoMsg("Contents of " + fileName + " = ");

            var code = Program.FindTokensBySentences(text);
            Console.WriteLine();

            Console.WriteLine(text);
            var exit = false;
            while (!exit)
            {
                Console.WriteLine();


                PrintMenu();

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
                        Program.ReOrderExpressions(code);
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

        /// <summary>
        ///     it displays the main menu.
        /// </summar>

        static public void PrintMenu()
        {
            Console.WriteLine(@"<<<<<<<<<<<<<<<<<<< MENU >>>>>>>>>>>>>>>");
            Console.WriteLine("\t 1). print the table of symbols");
            Console.WriteLine("\t 2). print the tokens table");
            Console.WriteLine("\t 3). show all the arithmetic expressions");
            Console.WriteLine("\t 4). analyze expression");
            Console.WriteLine("\t 5). re-organize the arithmetic expressions");
            Console.WriteLine("\t 6). read another file");
            Console.WriteLine("\t 7). exit");
        }

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        
        private void ReOrderExpressions(List<Sentence> code)
        {


            PrintOptionsOfArithmeticSentence(code);
            
            WriteOnConsoleInfoMsg("    Enter your choice:       ");
            var isNumeric = int.TryParse(Console.ReadLine(), out int choice);
            while (!isNumeric)
            {
                WriteOnConsoleInfoMsg("please enter a valid choice:  ");
                isNumeric = int.TryParse(Console.ReadLine(), out choice);
            }
            code = LookForAlgebraicSentence(code);

            Console.WriteLine();
            WriteOnConsoleInfoMsg("preorden: ");
            var pre = StringConverter.InfixToPrefix(code[choice-1]);
            Console.WriteLine(pre);
            Console.WriteLine();
            WriteOnConsoleInfoMsg("postorden: ");
            var post = StringConverter.InfixToPostfix(code[choice-1]);
            WriteOnConsoleInfoMsg("postorden: ");
            Console.WriteLine(post);
            Console.WriteLine();

        }

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        
        private void PostOrder()
        {
            //TODO: [FIX] Implemented the method.
            throw new NotImplementedException();
        }


        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>

        private void PreOrder()
        {
            //TODO: [FIX] Implemented the method
            throw new NotImplementedException();
        }

        /// <summary>
        ///     prints the table with all the Arirthmetic expresion found in the
        ///     the given text.
        /// </summary>
        /// <param name="code"> is a list </param>


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


        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        
        private List<Sentence> ReadAnotherFile()
        {
            string fileName = "";
            bool fileIsPending = true;
            string text = "";
            while (fileIsPending)
            {
                WriteOnConsoleInfoMsg("Please enter the name of the file you want to read: ");
                fileName = Console.ReadLine();

                try
                {
                    text = System.IO.File.ReadAllText(fileName);
                    fileIsPending = false;
                }
                catch (System.IO.FileNotFoundException)
                {
                    WriteOnConsoleErrorMsg("The file was not found, please try one more time..");
                    Console.WriteLine();
                    fileIsPending = true;
                }
            }

            System.Console.WriteLine("Contents of " + fileName + " = ");

            var code = FindTokensBySentences(text);
            System.Console.WriteLine();

            System.Console.WriteLine(text);
            return code;
        }

        /// <summary>
        ///  This method write to the console in blue
        /// </summary>
        /// <param name="msg"> is the message that will be printed on the screen </param>
        
        private void WriteOnConsoleInfoMsg(string msg)
        {
            WriteOnConsoleMsg(msg, ConsoleColor.Cyan);

        }

        /// <summary>
        ///  This method write to the console in Red
        /// </summary>
        /// <param name="msg"> is the message that will be printed on the screen </param>

        private void WriteOnConsoleErrorMsg(string msg)
        {
            WriteOnConsoleMsg(msg, ConsoleColor.Red);
        }

        /// <summary>
        ///  This method write to the console in the color pass.
        /// </summary>
        /// <param name="msg"> is the message that will be printed on the screen </param>
        /// <param name="color"> is the color that it will appeared </param>

        private void WriteOnConsoleMsg(string msg ,System.ConsoleColor color )
        {
            Console.ForegroundColor = color;
            Console.Write(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }


        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>

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

        public void PrintOptionsOfArithmeticSentence(List<Sentence> code)
        {
            WriteOnConsoleInfoMsg("Please select which arithmetic expression would you like to convert: ");
            Console.WriteLine();
            code = LookForAlgebraicSentence(code);
            ConsoleTable t = new ConsoleTable(200);
            t.PrintLine();
            Console.ForegroundColor = ConsoleColor.Green;
            t.PrintRow("option", "sentence");
            Console.ForegroundColor = ConsoleColor.White;
            t.PrintLine();
            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].IsAlgebraic)
                { 
                    t.PrintRow((i+1).ToString(), code[i].PrintSentence());
                }
            }

        }

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>


        public void PrintSymbolTable(List<Sentence> code )
        {
     

            ConsoleTable t = new ConsoleTable(200);
            t.PrintLine();
            Console.ForegroundColor = ConsoleColor.Green;
            t.PrintRow("line", "sentence", "token#", "symbol", "types","context", "description");
            Console.ForegroundColor = ConsoleColor.White;
            t.PrintLine();
            t.PrintLine();
            Stack<string> callstack = new Stack<string>();
            callstack.Push("global");
            for (int i = 0; i < code.Count; i++)
            {
                //var cxt = GetTokenContext(callstack, code[i]);
                var l = Convert.ToString(code[i].Line);
                foreach (String lex in code[i].Tokens)
                {
                    var data = Table.RetrieveData(lex);
                    var type = (data != null) ? data.Type[0] : "";
                    var desc = (data != null) ? data.Description : "";
                    var num = (data != null) ? Convert.ToString(data.Number) : "";
                    var cxt = callstack.Peek();
                  

                    t.PrintRow(l, Convert.ToString(i + 1), num, lex, type, cxt, desc);
                    if (data.Type.Length > 1)
                    {
                        for (int j = 1; j < data.Type.Length; j++)
                        {
                         t.PrintRow("","","","", data.Type[j],"","");
                        }
                    }
                    t.PrintLine();
                }
            }
        }

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="callstack"></param>
        /// <param name="sent"></param>
        /// <returns></returns>

        string GetTokenContext(Stack<string> callstack, Sentence sent) {

            List<string> opening = new List<string>(){ "(", "{" };
            List<string> closing = new List<string>() { ")", "}" };

            Stack<string> aux = new Stack<string>();
            for (int i = 0; i < (sent.Tokens.Count - 1); i++)
            {
                var lex = sent.Tokens[i];
                var data = Table.RetrieveData(lex);
                var isCxt = (data != null) && data.IsContext;
               
                if (isCxt)
                {
                    if (!closing.Contains(lex))
                    {
                        aux.Push(lex);
                    }
                    if (closing.Contains(lex))
                    {
                        while (aux.Peek() != GetMatchingGroupingOperator(lex))
                        {
                            aux.Pop();
                        }
                        _ =aux.Pop();
                        var scopeName = aux.Pop();
                        callstack.Push(scopeName);
                    }
                }
            }
            return callstack.Peek();
        }


        private string GetMatchingGroupingOperator(string closingOP)
        {
            var isParenthesis = string.Compare(")", closingOP, StringComparison.Ordinal);
            return (isParenthesis == 0) ? "(" : "{";
        }

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>

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
                _ = Convert.ToString(code[i].Line);
                foreach (String lex in code[i].Tokens)
                {
                    var data = Table.RetrieveData(lex);
                    var tok = (data != null) ? Convert.ToString(data.Name) : "";
                    var num = (data != null) ? Convert.ToString(data.Number) : "";
                    var lexeme= "'"+lex+"'";
                    t.PrintRow(tok, num, lexeme);

                }
            }

        }

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>

        public void PrintSentences(List<List<string>> code)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            string[] titles = { " Sentence  ", "NumberOfTokens ", "IsAlgebraic" };

        }

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public int FindLineNumber()
        {
            return -1;
        }

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lineText"></param>
        /// <returns></returns>
        
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


        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        
        public List<List<string>> FindTokens(String text)
        {
            List<List<string>> code = new List<List<String>>();
            Queue aux = new Queue();
            char[] charArray = text.ToCharArray();
            code = ParsetokensOFASentences(charArray, aux, code);
            return code;

        }


        /// <summary>
        /// This method divides the text in to its tokens
        /// </summary>
        /// <param name="text"> is the string that contains the code that will be analysed</param>
        /// <returns> Returns a list of Sentences. See <see cref="Sentence"/> for more info.</returns>

        public List<Sentence> FindTokensBySentences(String text)
        {
            List<Sentence> code = new List<Sentence>();
            Queue aux = new Queue();
            // convert the string taht contains the code to an array of Chars
            // so that we can can analyze each character individually. 
            char[] charArray = text.ToCharArray();
            code = ParsetokensOFASentences(charArray, aux, code);
            return code;

        }

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        
        private bool IsASentencesSeparator(char a)
        {
            string str = a.ToString();
            SymbolsData sd = Table.RetrieveData(str);
            bool ans = (sd != null) && sd.IsEnding;
            return ans;


        }

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>

        private bool IsASpace(char a)
        {
            string str = a.ToString();
            SymbolsData sd = Table.RetrieveData(str);
            bool ans = (sd != null) && sd.IsIrrelevant;
            return ans;


        }


        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>

        public bool IsAseparator(char a)
        {
            string str = a.ToString();
            string[] s = { "separador", "operadoraritmetico", "operadorrelacional", "operadoragrupacion" };
            SymbolsData sd = Table.RetrieveData(str);
            bool ans = ((sd != null) && ConcurType(s, sd)) ? true : false;
            return ans;
        }

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>

        public bool IsAseparator(string str)
        {
            string[] s = { "separador", "operadoraritmetico", "operadorrelacional", "operadoragrupacion" };
            SymbolsData sd = Table.RetrieveData(str);
            bool ans = ((sd != null) && ConcurType(s, sd)) ? true : false;
            return ans;
        }

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="sd"></param>
        /// <returns></returns>
        
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

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        
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

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="charArray"></param>
        /// <param name="aux"></param>
        /// <returns></returns>
        
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

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="charArray"></param>
        /// <param name="aux"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        
        private List<List<string>> ParsetokensOFASentences(char[] charArray, Queue aux, List<List<string>> code)
        {
            //bool SentenceCompleted = false;
            int j = 0;
            var li = new List<string>();

            while (j < charArray.Length)
            {
                //If the character that will be analyzed is not a separator it will be add to the queue
                if (!IsAseparator(charArray[j]))
                {

                    aux.Enqueue(charArray[j]);
                }
                // If the character that will be analyzed is separator; It can be because it is a single character
                // separator or it can be multiple characte separator. In order to know for whether is a single char
                //  or multiple char  separator we will needed a look-ahead.
                else
                {

                    // first of all, it will extract all ot the content that is currently in store in the auxilar queue
                    // the character that is currently been analyzed is character is part of a multiple character
                    // separator
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
                    //if tha character to analyze is a sentence separator  it will be added and
                    // a new sentence will start 
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

       
        /// <summary>
        ///  This method divide the code in a list of Sentences
        /// </summary>
        /// <param name="charArray">Is the Array Characters that will separated </param>
        /// <param name="aux"> is an auxilary queue that helps with the separation of tokens</param>
        /// <param name="code"></param>
        /// <returns> Returns </returns>
        
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

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        
        private bool IsRelevant(string str)
        {
            SymbolsData sd = Table.RetrieveData(str);
            bool ans = (sd != null) && sd.IsIrrelevant;
            return !ans;
        }

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        
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

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        
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

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        
        public bool IsAlgebraic(string str)
        {

            SymbolsData sd = Table.RetrieveData(str);
            bool ans = (sd != null) && IsAlgebraic(sd);
            return ans;
        }

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sd"></param>
        /// <returns></returns>
        
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

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        
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
