using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WF_Exam
{
    /// <summary>
    /// The <c>MyDictionary</c> class provides properties and methods
    /// for making saving actions in Dictionary
    /// </summary>
    [Serializable]
    public class MyDictionary
    {
        /// <value>
        /// The <c>DictionaryType</c> contains name of the Dictionary
        /// </value>
        public string DictionaryType { get; set; }
        /// <value>
        /// The <c>Words</c> contains List of <c>DictionaryWord</c>
        /// which has word and translations of it.
        /// </value>
        public List<DictionaryWord> Words { get; set; } = new List<DictionaryWord>();
        /// <summary>
        /// Saving history of checked words in text file.
        /// </summary>
        /// <param name="path">
        /// Used to indicate a path to the file.
        /// A <see cref="string"/>
        /// type representing an object of type String whose value is text
        /// </param>
        /// <param name="result">
        /// Used to indicate a newword.
        /// A <see cref="DictionaryWord"/>
        /// type representing an object of type DictionaryWord whose value is word with translations
        /// </param>
        /// <returns> method doesn't return a value.
        /// </returns>
        /// See<see cref="MyDictionary.SaveHistory(string, DictionaryWord)"/> to save word with translations.
        public void SaveHistory(string path, DictionaryWord result)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path + "History.txt", true, System.Text.Encoding.UTF8))
                {
                    sw.WriteLine(result);
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        /// <summary>
        /// Saving checked words to the file for print, file will be deleted after print.
        /// </summary>
        /// <param name="path">
        /// Used to indicate a path to the file.
        /// A <see cref="string"/>
        /// type representing an object of type String whose value is text
        /// </param>
        /// <param name="result">
        /// Used to indicate a newword.
        /// A <see cref="DictionaryWord"/>
        /// type representing an object of type DictionaryWord whose value is word with translations
        /// </param>
        /// <returns> method doesn't return a value.
        /// </returns>
        /// See<see cref="MyDictionary.SaveResult(string, DictionaryWord)"/> to save word with translations.
        public void SaveResult(string path, DictionaryWord result)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path + "Result.txt", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(result);
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        public override string ToString()
        {
            return $" \t{DictionaryType} Vocabulary\n{string.Join("\n", Words)}";
        }
    }   
}
