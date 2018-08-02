using System;
using System.Collections;
using System.Collections.Generic;

namespace LexicalAnalysis
{
    class Program
    {

        SymbolsTable symbolsTable;


        //main Method
        public static void Main()
        {
            Console.Write("Please enter the name of the file you want to read: ");
            string fileName = Console.ReadLine();
            String[] lines = System.IO.File.ReadAllLines(fileName);
            System.Console.WriteLine("Contents of WriteLines2.txt = ");
            List<List<string>> code = findTokens(lines);


        }
        /*
         *look for all tokens in the program 
         *
         */

        public static List<List<string>>  findTokens(String[] lineText)
        {
            List<List<string>> code = new List<List<String>>();

            Queue aux = new Queue();
            for (int i = 0; i < lineText.Length; i++)
            {
                char[] charArray = lineText[i].ToCharArray();
                var li = parsetokensOFASentences(charArray, aux);
                code.Add(li);
            }
            return code;


        }

        /*
         *check if a separetor is a sentence separetor
         *
         */


        private static bool isASentencesSeparetor(char v)
        {
            //throw new NotImplementedException();

            return false; 
        }
        /*
         *check if a separetor is a sentence separetor
         *
         */
        private static bool isASeparetor(char a)
        {
            throw new NotImplementedException();

        }

        /*
         *check if a separetor is a sentence separetor
         *
         */

        private static String QueueToString(Queue q)
        {

            char[] tmp = new char[q.Count];
            for (int i = 0; i < q.Count; i++)
            {
                tmp[i] = (char)q.Dequeue();
            }
            return new string(tmp);
        }
        /*
         *check if a separetor is a sentence separetor
         *
         */

        private static List<String> parsetokensOFASentences(char[] charArray, Queue aux)
        {
            var li = new List<string>();
            int k = 0;
            for (int j = 0; j < charArray.Length; j++)
            {
                if (!isASentencesSeparetor(charArray[k]))
                {
                    if (!isASentencesSeparetor(charArray[k]))
                    {

                    }
                    aux.Enqueue(charArray[k]);
                }
                else
                {
                    String str = QueueToString(aux);
                    li.Add(str);
                    li.Add(charArray[k].ToString());
                }
               
            }
            return li;
        }
    }
}

