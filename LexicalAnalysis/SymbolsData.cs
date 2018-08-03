using System;
using System.Runtime.Serialization;

namespace LexicalAnalysis
{
    public class SymbolsData:IComparable
    {
        public string Symbol { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool IsEnding { get; set; }
        public bool IsIrrevelant { get; set; }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            SymbolsData other = obj as SymbolsData;
            if (other!= null)
                return string.Compare(this.Symbol, other.Symbol, StringComparison.Ordinal);
            else
                throw new ArgumentException("Object is not a symbol");
        }

      
    

    }

   
}
