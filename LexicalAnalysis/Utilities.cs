using System;
using System.Collections.Generic;

namespace LexicalAnalysis
{
    public class Utilities
    {
        public Utilities()
        {
        }

        static public string ConvertCharListToString(List<char> list) 
        {
            string ans = "";
            foreach(char c in list)
            {
                ans+=c;
            }
            return ans;
        }
    }
}
