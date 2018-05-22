using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TextParser.Classes;
using TextParser.Interfaces;

namespace TextParser.Classes
{
    public class TextHandler : ITextHandler
    {
        //Property
        public List<ISentenceHandler> SentencesList { get; set; }

        //Constructor
        public TextHandler()
        {
            SentencesList = new List<ISentenceHandler>();
        }

        //This method checks the availability of the file and handles raw text
        public void ParseText(string path)
        {
            try
            {
                StringBuilder tempText = new StringBuilder();
                using (StreamReader sr = new StreamReader(path))
                {
                    string line = sr.ReadToEnd().Replace("\n", String.Empty).Replace("\r", String.Empty).Replace("\t", String.Empty);
                    tempText.Append(line);
                }

                string[] senstences = Regex.Split(tempText.ToString(), @"(?<=[.?!])");
                //SentenceHandler temtSentences = new SentenceHandler();
                //The line above is commented out so that we can create not only instances of the SentenceHandler class
                foreach (string sentence in senstences)
                {
                    SentencesList.Add(SentenceHandler.ParseSentence(sentence));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("\n==================================================\n");
            }
        }

        //This method counts the number of words in the sentence.
        public int Count
        { get { return SentencesList.Count(); } }

        //Override the method ToString for easy output
        public override string ToString()
        {
            return String.Join(" ", SentencesList);
        }
    }
}