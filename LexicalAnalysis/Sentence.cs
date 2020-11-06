using System;
using System.Collections.Generic;

namespace LexicalAnalysis
{
    //TODO: [DOCS] write the corresponding documentation
    /// <summary>
    /// 
    /// </summary>
    public class Sentence
    {
       
        public List<string> Tokens { set; get; }
        public int Line { set; get; }
        public bool IsAlgebraic { set; get;}

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="line"></param>

        public Sentence(List<string> tokens,int line)
        {
            this.Tokens = tokens;
            this.Line = line;
            this.IsAlgebraic = false;
        }

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>

        public Sentence() => this.Tokens = new List<string>();

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>

        public void Add(string str)
        {
            Tokens.Add(str);
        }

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public string PrintSentence(){
            string str = "";
            foreach(string tok in Tokens){
                str += tok+" ";
            }
            return str;

        }

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public List<char> ToCharList()
        {
            string str = "";
            foreach (string t in Tokens){ str += t; }
            return new List<char>(str.ToCharArray());

        }
    }
}
