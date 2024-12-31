using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadFile
{
    public class WorkString
    {
        private List<string> _textKey = new List<string>();
        public List<string> TextKey
        { 
            get=> _textKey;
            set 
            {
                if (_textKey != value)
                {
                    _textKey=value;
                }
            } 
        }
        public int CountWord { get; set; }
        public int SizeWord { get; set; }
        public int SizeNumber {  get; set; }


        public List<string> FirstAndLastLetterWordAll { get; set; } = new List<string>();
        public List<string> ExclamatorySentence { get; set; } = new List<string>();
        public List<string> InterrogativeSentence { get; set; } = new List<string>();
        public List<string> NumberFirstAndLastLetter { get; set; } = new List<string>();

        public string bigWords = null;
        public  string bigNumber = null;

        public WorkString() { }

        public void FindBigWords()
        {
            int i = 0;
            foreach (var line in _textKey)
            {
                i++;
                bool containsDigital = line.Any(char.IsLetter);
                if (containsDigital && SizeWord < line.Length)
                {
                    SizeWord = line.Length;
                    bigWords = line;
                }
            }

            CountWord =_textKey.Where(word=>word.Equals(bigWords)).Count();
        }

        public void ConvertNumberToString()
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<char, string> mappings = new Dictionary<char, string>
            {
                { '1', "one" },
                { '2', "two" },
                { '3', "three" },
                { '4', "four" },
                { '5', "five" },
                { '6', "six" },
                { '7', "seven" },
                { '8', "eight" },
                { '9', "nine" },
                { '0', "zero" }
            };

            //foreach (var item in number)
            //{
            //    mappings.TryGetValue(item, out var result);
            //    sb.AppendLine(result.ToString());
            //    sb.AppendLine(" ");
            //}

        }

        public void SplitСollection(List<string> sentenceList)
        {
            foreach (var item in sentenceList)
            {
                if (item.EndsWith("!"))
                {
                    ExclamatorySentence.Add(item);
                }
                else if (item.EndsWith("?"))
                {
                    InterrogativeSentence.Add(item);
                }
            }
        }

        internal void FindWordsWithSameFirstAndLastLetter()
        {
            foreach(var word in _textKey)
            {
                string firstLetter = word[0].ToString();
                if(word.EndsWith(firstLetter))
                    FirstAndLastLetterWordAll.Add(word);
            }
        }

        public void FindNumberWithSameFirstAndLastLetter()
        {
            var wordsWithDigits = _textKey.Where(word=>word.Any(char.IsDigit)).ToList();
            var maxNumber = wordsWithDigits.Max(word=>word.Length);
            NumberFirstAndLastLetter = wordsWithDigits.Where(word=>word.Length >= maxNumber).ToList();
        }
    }
}
