using System;

namespace LexicalAnalysis
{
    public class ConsoleTable
    {
        int TableWidth {set; get; }



        public ConsoleTable(int tw)
        {
            TableWidth = tw;
        }


        public void PrintLine()
        {
            Console.WriteLine(new string('-', TableWidth));
        }

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
