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
      
        public Program(String  data){
            Table = JsonConvert.DeserializeObject<SymbolsTable>(data);
        }

        //main Method
        public static void Main()

        {
            String Json = System.IO.File.ReadAllText("SymbolsTable.json");
            Program Program = new Program(Json);
            SymbolsTable Table = Program.Table;
            Console.Write("Please enter the name of the file you want to read: ");
            string fileName = Console.ReadLine();
            //String[] lines = System.IO.File.ReadAllLines(fileName);

            String text = System.IO.File.ReadAllText(fileName);
            System.Console.WriteLine("Contents of WriteLines2.txt = ");
            List<List<string>> code = Program.FindTokens(text);
            System.Console.WriteLine(Table.SymbolsData.Count);


        }

        public void PrintSymbolsTable( List<List<string>> code ){
            string[] titles = new string[] { "LineNumber", "Symbols","Description", "Type"};

        }

        public void PrintSentences(List<List<string>> code)
        {
            string[] titles = new string[] { " Sentence  ", "NumberOfTokens ", "IsAlgebraic" };

        }



        public int FindLineNumber(){
            return -1;
        }

        /*
         *look for all tokens in the program 
         *
         *
         */

        public List<List<string>>  FindTokens(String[] lineText)
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
            bool ans = (sd != null) && sd.IsIrrevelant;
            return ans;


        }


        /*
         *check if a separator is a sentence separator
         *
         */

        public bool IsAseparator(char a)
        {
           string str = a.ToString();
            string[] s = new string[] { "separador", "operadoraritmetico","operadorrelacional","operadoragrupacion" };
            SymbolsData sd = Table.RetrieveData(str);
            bool ans = ((sd != null) && ConcurType(s,sd))? true:false;
            return ans;
        }

       private bool ConcurType(string[] strs, SymbolsData sd)
        {
            bool ans = false;
            foreach (string s in strs){
                ans |= (string.Compare(sd.Type, s, StringComparison.Ordinal)==0);
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
            for (int i = 0; i < tmp.Length;i++)
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
                {   if (aux.Count > 0)
                    {
                        String str = QueueToString(aux);
                        li.Add(str);
                    }
                    if(!IsASpace(charArray[j]))
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
        private List<List<string>> ParsetokensOFASentences(char[] charArray, Queue aux, List<List<string>> code )
        {
            //bool SentenceCompleted = false;
            int j = 0;
            var li = new List<string>();

            while (j<charArray.Length ){
                if (!IsAseparator(charArray[j]))
                {

                    aux.Enqueue(charArray[j]);
                }
                else
                {
                   
                    if (aux.Count > 0)
                    {
                        String str = QueueToString(aux);
                        if(IsRelevant(str) && !(string.Compare(str, "", StringComparison.Ordinal) == 0))
                        {
                            li.Add(str);
                        }
                     
                    }
                    if (!IsASpace(charArray[j]))
                    {
                        li.Add(charArray[j].ToString());
                    }
                    if (IsASentencesSeparator(charArray[j]))
                    {
                        code.Add(li);
                        li = new List<string>();

                    }
                }
                j++;  
            }

            return code;
        }

        private bool IsRelevant(string str )
        {
            SymbolsData sd = Table.RetrieveData(str);
            bool ans = (sd != null) && sd.IsIrrevelant;
            return !ans;
        }
    }
}      

