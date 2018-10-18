using System;
using System.Collections.Generic;

namespace LexicalAnalysis
{
    public class SymbolsTable
    {
        public List<SymbolsData> SymbolsData { get; set; }


        public  SymbolsData RetrieveData(string k )
        {
            var i = FindPosition(k);
            SymbolsData ans = i == -1 ? RetrieveIdentifierData() : SymbolsData[i];
            return ans;
        }

        public SymbolsData RetrieveIdentifierData()
        {
            SymbolsData ans = null;
            int i = 0;
            while (i < SymbolsData.Count && !(string.Compare(SymbolsData[i].Name, "Identificador", StringComparison.Ordinal) == 0)) { i += 1;}
            ans = (i == SymbolsData.Count) ? null : SymbolsData[i];
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
