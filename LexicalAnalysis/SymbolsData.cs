using System;
using System.Runtime.Serialization;

namespace LexicalAnalysis
{
    public class SymbolsData:IComparable
    {
        public int Number{ get; set; }
        public string Name{ get; set; }
        public string Symbol { get; set; }
        public string[] Type { get; set; }
        public string Description { get; set; }
        public bool IsEnding { get; set; }
        public bool IsIrrelevant { get; set; }
        public bool IsChar { get; set; }
        public bool IsContext { get; set; }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (obj as SymbolsData != null)
                return string.Compare(this.Symbol, (obj as SymbolsData).Symbol, StringComparison.Ordinal);
            else
                throw new ArgumentException("Object is not a symbol");
        }

      
    

    }

   
}
