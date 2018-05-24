using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TextParser.Classes;
using TextParser.Interfaces;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace TextParser.Classes
{
    [DataContract]
    public class TextHandler : ITextHandler
    {
        [DataMember]
        public List<ISentenceHandler> Sentences { get; set; }
        
        public TextHandler()
        {
            Sentences = new List<ISentenceHandler>();
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
                SentenceHandler temtSentences = new SentenceHandler();
                //The line above is commented out so that we can create not only instances of the SentenceHandler class
                foreach (string sentence in senstences)
                {
                    Sentences.Add(SentenceHandler.ParseSentence(sentence));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("\n==================================================\n");
                Environment.Exit(1);
            }
        }

        //This method sortes sentences by the number of words (through classic methods).
        //public List<ISentenceHandler> SortByWordsCount()
        //{
        //    return SentencesList.OrderBy(x => x.Count).ToList<ISentenceHandler>();
        //}

        //This method sortes sentences by the number of words (through LINQ).
        public string SortByWordsCount()
        {
            string result;
            result = String.Join(" ", (from sentence in Sentences
                                       orderby sentence.Count
                                       select sentence));
            return result;
        }

        //This method returns non-repeated words of a given length from interrogative sentences
        public string GetInterrogativeSentence(int wordLenght)
        {
            List<string> result = new List<string>();
            foreach (var sentence in Sentences.Where(x => x.SentenceIsInterrogative))
            {
                foreach (var word in sentence.Words.Where(x => x.Count == wordLenght))
                {
                    if (!result.Contains(word.ToString()))
                    { result.Add(word.ToString()); }
                }
            }
            return String.Join(" ", result);
        }

        //This method removes words of a given length (through classic methods).
        //public TextHandler RemoveWordsWithConsonantLetter(int wordLenght)
        //{
        //    TextHandler result = new TextHandler();

        //    foreach (SentenceHandler sentence in Sentences)
        //    {
        //        result.Sentences.Add(new SentenceHandler(sentence.Words.Where(x => !(x.Count == wordLenght && x.FirstLetterIsConsonant)), sentence.Separator));
        //    }
        //    return result;
        //}

        //This method removes words of a given length (through LINQ).
        public TextHandler RemoveWordsWithConsonantLetter(int wordLenght)
        {
            TextHandler result = new TextHandler();
            foreach (SentenceHandler sentence in Sentences)
            {
                result.Sentences.Add(new SentenceHandler
                   ((from word in sentence.Words
                     where !(word.Count == wordLenght && word.FirstLetterIsConsonant == true)
                     select word), sentence.Separator));
            }
            return result;
        }
        //This method replaces words of a given length with a substring.
        public TextHandler TextChangeOnSubstring(int sentenceNumber, int wordLenght, string subString)
        {
            WordHandler tempWord = new WordHandler(subString);

            TextHandler result = new TextHandler();

            for (int index = 0; index < Sentences.Count; index++)
            {
                if (index == sentenceNumber - 1)
                {
                    result.Sentences.Add(new SentenceHandler(Sentences[index].Words.Select(x => x.Count == wordLenght ? tempWord : x), Sentences[index].Separator));
                }
                else
                {
                    result.Sentences.Add(new SentenceHandler(Sentences[index].Words, Sentences[index].Separator));
                }
            }
            return result;
        }

        //This method counts the number of words in the sentence.
        public int Count
        { get { return Sentences.Count(); } }

        //Override the method ToString to easy output
        public override string ToString()
        {
            return String.Join(" ", Sentences);
        }
    }
}