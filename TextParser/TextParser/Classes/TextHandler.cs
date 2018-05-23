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

        //This method sortes sentences by the number of words.
        public List<ISentenceHandler> SortByWordsCount()
        {
            return SentencesList.OrderBy(x => x.Count).ToList();
            //List<SentenceHandler> result = new List<SentenceHandler>();
            //result = (from sentence in SentencesList orderby sentence.Count select sentence).ToList<SentenceHandler>();
            //return result;
        }

        //This method returns non-repeated words of a given length from interrogative sentences
        public string GetInterrogativeSentence(int wordLenght)
        {
            List<string> result = new List<string>();
            foreach (var sentence in SentencesList.Where(x => x.SentenceIsInterrogative))
            {
                foreach (var word in sentence.WordsList.Where(x => x.Count == wordLenght))
                {
                    if (!result.Contains(word.ToString()))
                    { result.Add(word.ToString()); }
                }
                //          result = (from word in sentence.WordsList
                //                    where word.Count == wordLenght
                //                    select word
                //                ).Distinct().ToList();
            }
            return String.Join(" ", result);
        }
        //This method removes words of a given length.s
        public TextHandler RemoveWordsWithConsonantLetter(int wordLenght)
        {
            TextHandler result = new TextHandler();

            foreach (SentenceHandler sentence in SentencesList)
            {
                result.SentencesList.Add(new SentenceHandler(sentence.WordsList.Where(x => !(x.Count == wordLenght && x.FirstLetterIsConsonant)), sentence.Separator));
            }
            return result;
        }

        //This method replaces words of a given length with a substring.
        public TextHandler TextChangeOnSubstring(int sentenceNumber, int wordLenght, string subString)
        {
            WordHandler tempWord = new WordHandler(subString);

            TextHandler result = new TextHandler();

            for (int index = 0; index < SentencesList.Count; index++)
            {
                if (index == sentenceNumber - 1)
                {
                    result.SentencesList.Add(new SentenceHandler(SentencesList[index].WordsList.Select(x => x.Count == wordLenght ? tempWord : x), SentencesList[index].Separator));
                }
                else
                {
                    result.SentencesList.Add(new SentenceHandler(SentencesList[index].WordsList, SentencesList[index].Separator));
                }
            }
            return result;
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