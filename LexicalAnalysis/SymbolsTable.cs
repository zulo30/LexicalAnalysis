using System;
using System.Collections.Generic;

namespace LexicalAnalysis
{
    public class SymbolsTable
    {
        public List<SymbolsData> SymbolsData { get; set; }


        public  SymbolsData RetrieveData(string k )
        {
            int i = FindPosition(k);
            SymbolsData ans = (i == -1) ? null : SymbolsData[i];
            return ans;
        }

        public int FindPosition(string k)
        {
            int i = 0;
            while (i < SymbolsData.Count && !(string.Compare(SymbolsData[i].Symbol, k, StringComparison.Ordinal) == 0)){ i += 1; }

            var ans = (i == SymbolsData.Count) ? -1 : i;
            return ans;
        }

       
    }
}
