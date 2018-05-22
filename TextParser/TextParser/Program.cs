using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextParser.Classes;

namespace TextParser
{
    class Program
    {
        static void Main(string[] args)
        {
            TextHandler text = new TextHandler();
            text.ParseText(@"G:\Repositories\TextParser\TextParser\TextParser\Files\TestText.txt");
            Console.WriteLine("Текст:\n");
            Console.WriteLine(text.ToString());
            Console.WriteLine("\n==================================================");
            Console.ReadKey();
        }
    }
}
