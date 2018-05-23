using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextParser.Interfaces;

namespace TextParser.Classes
{
    public class WordHandler : IWordHandler
    {
        //Static field for determining consonants
        private static char[] consonantLetters = new char[] { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Z',
                                                              'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z' };

        //Properties
        public string Word { get; set; }
        public List<char> CharactersList { get; set; }

        //Constructor
        public WordHandler(string word)
        {
            Word = word;

            CharactersList = new List<char>();
            foreach (char character in Word)
            {
                CharactersList.Add(character);
            }
        }

        //This method counts the number of words in the sentence and implements the interface ICountElements
        public int Count
        { get { return CharactersList.Count; } }

        //Override the method ToString to easy output
        public override string ToString()
        {
            return Word;
        }

        //This method determines whether the first letter of a word is a consonant
        public bool FirstLetterIsConsonant
        {
            get { return consonantLetters.Contains(CharactersList.First()); }
        }
    }
}