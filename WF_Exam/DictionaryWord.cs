using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF_Exam
{
    /// <summary>
    /// The <c>DictionaryWord</c> class provides properties and methods
    /// for making actions with Words in dictionary
    /// </summary>
    [Serializable]
    public class DictionaryWord
    {
        /// <value>
        /// The <c>Word</c> contains word of the Dictionary
        /// </value>
        public string Word { get; set; }
        /// <value>
        /// The <c>Translations</c> contains translations of the Dictionary
        /// </value>
        public List<string> Translation { get; set; } = new List<string>();
        public DictionaryWord() { }
        /// <summary>
        /// Constuctor with parameters.
        /// </summary>
        /// <param name="word">
        /// Used to indicate a word.
        /// A <see cref="string"/>
        /// type representing an object of type String whose value is text
        /// </param>
        /// <param name="translation">
        /// Used to indicate a translations.
        /// A <see cref="List<>"/>
        /// type representing an object of type List<> whose value is text
        /// </param>
        /// <returns> method doesn't return a value.
        /// </returns>
        /// See<see cref="MyDictionary(string, List)"/>.
        public DictionaryWord(string word, List<string> translation)
        {
            Word = word;
            Translation = translation;
        }
        public override string ToString()
        {
            return $" {Word} - {string.Join(",", Translation)}";
        }
    }
}
