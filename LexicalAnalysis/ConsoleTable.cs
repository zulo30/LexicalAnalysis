using System;

namespace LexicalAnalysis
{
    //TODO: [DOCS] write the corresponding documentation
    /// <summary>
    /// 
    /// </summary>

    public class ConsoleTable
    {
        int TableWidth {set; get; }


        // constructor 
        public ConsoleTable(int tw)
        {
            TableWidth = tw;
        }

        // methods

        //TODO: [DOCS] write the corresponding documentation 
        /// <summary>
        /// 
        /// </summary>

        public void PrintLine()
        {
            Console.WriteLine(new string('-', TableWidth));
        }

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columns"></param>

        public void PrintRow(params string[] columns)
        {
            int width = (TableWidth - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        //TODO: [DOCS] write the corresponding documentation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        
        public string AlignCentre(string text, int width)
        {
            if (string.IsNullOrEmpty(text))
            {
                
                return new string(' ', width);
            }
            else
            {
                text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }

    }
}
