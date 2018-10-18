using System;
using System.Collections.Generic;

namespace LexicalAnalysis
{
    public class Sentence
    {

        public List<string> Tokens { set; get; }
        public int Line { set; get; }
        public bool IsAlgebraic { set; get;}
       

        public Sentence(List<string> tokens,int line)
        {
            this.Tokens = tokens;
            this.Line = line;
            this.IsAlgebraic = false;
        }

        public Sentence() => this.Tokens = new List<string>();

        public void Add(string str)
        {
            Tokens.Add(str);
        }

        public string PrintSentence(){
            string str = "";
            foreach(string tok in Tokens){
                str += tok+" ";
            }
            return str;

        }

        public  List<char> ToCharList()
        {
            string str = "";
            foreach (string t in Tokens){ str += t; }
            return new List<char>(str.ToCharArray());

        }
    }
}
