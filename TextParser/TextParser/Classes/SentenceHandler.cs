using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TextParser.Interfaces;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace TextParser.Classes
{
    [DataContract]
    public class SentenceHandler : ISentenceHandler
    {
        [DataMember]
        private string Sentence { get; set; }
        [DataMember]
        public char Separator { get; private set; }
        [DataMember]
        public List<IWordHandler> Words { get; private set; }

        public SentenceHandler()
        {
            Words = new List<IWordHandler>();
        }

        public SentenceHandler(string sentence, char separator) : this()
        {
            Sentence = sentence;
            Separator = separator;
        }
        public SentenceHandler(IEnumerable<IWordHandler> words, char separator)
        {
            Words = new List<IWordHandler>(words);
            Separator = separator;
        }

        //This method determines whether this sentence is interrogative
        public bool SentenceIsInterrogative
        {
            get { return Sentence.EndsWith("?"); }
        }

        //This method handles the raw sentence. This method is static, in order to apply it without creating an instance (In the method ParseText from TextHandler class)
        //We can remove the "static" and uncomment one line in the ParseText method (TextHandler class)
        static public SentenceHandler ParseSentence(string sentence)
        {
            string tempSentence = Regex.Replace(sentence.Trim(), "[^a-zA-Z0-9 ]", "");
            string[] words = tempSentence.Split(' ');
            SentenceHandler result = new SentenceHandler(sentence.Trim(), sentence.LastOrDefault());
            foreach (string word in words)
            {
                result.Words.Add(new WordHandler(word));
            }
            return result;
        }

        //This method counts the number of words in the sentence and implements the interface ICountElements
        public int Count
        { get { return Words.Count(); } }

        //Override the method ToString for easy output
        public override string ToString()
        {
            return String.Join(" ", Words) + Separator;
        }

    }
}