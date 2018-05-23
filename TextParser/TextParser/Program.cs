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
            //text.ParseText(@"E:\Repositories\TextParser\TextParser\TextParser\Files\TestText.txt");
            //The line above is a simple file call, the line below is a file call using application properties
            text.ParseText(Properties.Settings.Default.FilePath);
            Console.WriteLine("Текст:\n");
            Console.WriteLine(text.ToString());
            Console.WriteLine("\n==================================================\n");
            Console.WriteLine("Вывести все предложения заданного текста в порядке возрастания количества слов в каждом из них:\n");
            //Console.WriteLine(String.Join(" ", text.SortByWordsCount()));
            Console.WriteLine(text.SortByWordsCount()); //through LINQ
            Console.WriteLine("\n==================================================\n");
            Console.WriteLine("Во всех вопросительных предложениях текста найти и напечатать без повторений слова заданной длины (3 символа):\n");
            Console.WriteLine(String.Join(" ", text.GetInterrogativeSentence(3)));
            Console.WriteLine("\n==================================================\n");
            Console.WriteLine("Из текста удалить все слова заданной длины (4 символа), начинающиеся на согласную букву:\n");
            Console.WriteLine(text.RemoveWordsWithConsonantLetter(4));
            Console.WriteLine("\n==================================================\n");
            Console.WriteLine("В некотором предложении текста (в третьем) слова заданной длины (3 символа) заменить указанной подстрокой, длина которой может не совпадать с длиной слова:\n");
            Console.WriteLine(text.TextChangeOnSubstring(3, 3, "SUBSTRING"));
            Console.WriteLine("\n==================================================");
            Console.ReadKey();
        }
    }
}