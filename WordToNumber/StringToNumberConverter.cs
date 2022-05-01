// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringToNumberConverter.cs" company="Xyz">
// All rights reserved
// </copyright>
// <summary>
//   The string to number converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WordToNumber
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Headers;

    /// <summary>
    /// The string to number converter.
    /// </summary>
    public class StringToNumberConverter
    {
        Dictionary<string, NumberRepresentation> validWords = new Dictionary<string, NumberRepresentation>();
        Dictionary<string, int> scales = new Dictionary<string, int>();
        public StringToNumberConverter()
        {
            this.InitializeWords();
        }

        private void InitializeWords()
        {
            // One place value
            this.validWords.Add("zero", new NumberRepresentation() { Value = 0 });
            this.validWords.Add("one", new NumberRepresentation() { Value = 1 });
            this.validWords.Add("two", new NumberRepresentation() { Value = 2 });
            this.validWords.Add("three", new NumberRepresentation() { Value = 3 });
            this.validWords.Add("four", new NumberRepresentation() { Value = 4 });
            this.validWords.Add("five", new NumberRepresentation() { Value = 5 });
            this.validWords.Add("six", new NumberRepresentation() { Value = 6 });
            this.validWords.Add("seven", new NumberRepresentation() { Value = 7 });
            this.validWords.Add("eight", new NumberRepresentation() { Value = 8 });
            this.validWords.Add("nine", new NumberRepresentation() { Value = 9 });

            // ten's place value
            this.validWords.Add("ten", new NumberRepresentation() { Value = 10 });
            this.validWords.Add("eleven", new NumberRepresentation() { Value = 11 });
            this.validWords.Add("twelve", new NumberRepresentation() { Value = 12 });
            this.validWords.Add("thirteen", new NumberRepresentation() { Value = 13 });
            this.validWords.Add("fourteen", new NumberRepresentation() { Value = 14 });
            this.validWords.Add("fifteen", new NumberRepresentation() { Value = 15 });
            this.validWords.Add("sixteen", new NumberRepresentation() { Value = 16 });
            this.validWords.Add("seventeen", new NumberRepresentation() { Value = 17 });
            this.validWords.Add("eighteen", new NumberRepresentation() { Value = 18 });
            this.validWords.Add("nineteen", new NumberRepresentation() { Value = 19 });

            this.validWords.Add("twenty", new NumberRepresentation() { Value = 20 });
            this.validWords.Add("thirty", new NumberRepresentation() { Value = 30 });
            this.validWords.Add("forty", new NumberRepresentation() { Value = 40 });
            this.validWords.Add("fifty", new NumberRepresentation() { Value = 50 });
            this.validWords.Add("sixty", new NumberRepresentation() { Value = 60 });
            this.validWords.Add("seventy", new NumberRepresentation() { Value = 70 });
            this.validWords.Add("eighty", new NumberRepresentation() { Value = 80 });
            this.validWords.Add("ninety", new NumberRepresentation() { Value = 90 });

            // Initialize scales
            this.scales.Add("hundred", 100);
            this.scales.Add("thousand", 1000);
            this.scales.Add("million", 1000000);
            this.scales.Add("billion", 1000000000);
        }

        /// <summary> The convert to. </summary>
        /// <param name="numberInWord"> The number in word. </param>
        /// <returns> int </returns>
        public int ConvertTo(string numberInWord)
        {
            if (string.IsNullOrWhiteSpace(numberInWord) || !this.IsValidNumber(numberInWord))
            {
                throw new ArgumentException("Invalid input'");
            }

            var convertedNumber = this.FindScaleIndexAndConvert(numberInWord);
            return convertedNumber;
        }

        /// <summary>
        /// Converts a number to it's text representation.
        /// </summary>
        /// <param name="number"> The number. </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ConvertTo(long number)
        {            
            string[] onesAndTeens = new[]
                                        {
                                            "zero", "one", "two", "three", "four", "five", "six", "seven", "eight",
                                            "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen",
                                            "sixteen", "seventeen", "eighteen", "nineteen"
                                        };
            string[] tens = { string.Empty, string.Empty, "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            var current = number;
            if (number == 0)
            {
                return string.Empty;
            }

            if (number < 20)
            {
                return onesAndTeens[number];
            }

            if (number < 100)
            {
                return tens[number / 10] + " " + onesAndTeens[number % 10];
            }

            if (number < 1000)
            {
                return onesAndTeens[number / 100] + " hundred " + this.ConvertTo(number % 100);
            }

            if (number < 10000)
            {
                return onesAndTeens[number / 1000] + " thousand " + this.ConvertTo(number % 1000);
            }

            // Less than 100K
            if (number < 100000)
            {
                return tens[number / 10000] + " " + this.ConvertTo(number % 10000);
            }

            // Less than million
            if (number < 1000000)
            {
                return this.ConvertTo(number / 1000) + " thousand " + this.ConvertTo(number % 1000);
            }

            // less than 10 mil
            if (number < 10000000)
            {
                return this.ConvertTo(number / 1000000) + " million " + this.ConvertTo(number % 1000000);
            }

            // less than 100 mil
            if (number < 100000000)
            {
                return this.ConvertTo(number / 1000000) + " million " + this.ConvertTo(number % 1000000);
            }

            return string.Empty;
        }

        /// <summary> Is valid number. </summary>
        /// <param name="numberInWord"> The number in word. </param>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsValidNumber(string numberInWord)
        {
            if (string.IsNullOrWhiteSpace(numberInWord))
            {
                return false;
            }
            var words = numberInWord.Trim().Split(' ');
            if (!this.AreValidWords(words))
            {
                return false;
            }

            if (!this.WordsInValidOrder(words))
            {
                return false;
            }

            return true;
        }

        #region Private members

        /// <summary> The find index of the words that identify the scale of the number e.g. hundred, thousand, million etc
        /// This gives the power of ten that the number needs to be multiplied with
        /// </summary>
        /// <param name="numberInWord"> The number in word. </param>
        /// <returns> The <see cref="int"/>. </returns>
        private int FindScaleIndexAndConvert(string numberInWord)
        {
            var input = numberInWord.Trim().ToLower();
            var words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var wordList = words.ToList();
            var markersAndScale = this.FindIndexesHavingScale(words);
            var total = 0;
            if (!markersAndScale.Any())
            {
                return this.Convert(words);
            }

            // Recursively splitting the string on highest scale value e.g billion, million, thousand, hundred
            var highestPriorityScale = markersAndScale.OrderByDescending(x => x.scaleValue).First();

            // now split the array in two halves and sum the number multiplied by the scale
            var firstHalf = string.Join(' ', wordList.GetRange(0, highestPriorityScale.marker));
            var secondHalf = string.Join(' ', wordList.GetRange(highestPriorityScale.marker + 1, wordList.Count - (highestPriorityScale.marker + 1)));

            total = (this.FindScaleIndexAndConvert(firstHalf) * highestPriorityScale.scaleValue)
                    + this.FindScaleIndexAndConvert(secondHalf);
            return total;
        }

        /// <summary> Converts the sub words to number which would be multiplied with the scale and added to produce final number. </summary>
        /// <param name="words"> The words. </param>
        /// <returns> The <see cref="int"/>. </returns>
        private int Convert(string[] words)
        {
            // Revisit
            var outPut = 0;
            for (int i = words.Length - 1; i >= 0; i--)
            {
                var word = words[i].Trim().ToLower();
                if (string.IsNullOrWhiteSpace(word))
                {
                    continue;
                }

                // Find values and add to out put
                if (this.validWords.ContainsKey(word))
                {
                    var value = this.validWords[word];
                    outPut += value.Value;
                }
            }

            return outPut;
        }

        /// <summary> The find indexes having scale. e.g. hundred, thousand, million etc </summary>
        /// <param name="words"> The words. </param>
        /// <returns> The <see cref="List"/>. </returns>
        private List<(int marker, int scaleValue)> FindIndexesHavingScale(string[] words)
        {
            var indexes = new List<(int marker, int scaleValue)>();
            for (int i = 0; i < words.Length; i++)
            {
                var word = words[i].Trim();
                if (string.IsNullOrWhiteSpace(word))
                {
                    continue;
                }

                if (this.scales.ContainsKey(word))
                {
                    var scaleValue = this.scales[word];
                    indexes.Add((i, scaleValue));
                }
            }

            return indexes;
        }

        /// <summary> Checks if the words in the string ar valid to make a number. </summary>
        /// <param name="words"> The words. </param>
        /// <returns> The <see cref="bool"/>. </returns>
        private bool AreValidWords(string[] words)
        {
            for (int i = words.Length - 1; i >= 0; i--)
            {
                var word = words[i].Trim().ToLower();
                if (string.IsNullOrWhiteSpace(word))
                {
                    continue;
                }

                // if the words don't contained the valid keys then it's not a valid number
                if (!this.validWords.ContainsKey(word) && !this.scales.ContainsKey(word))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary> The words in valid order. For example this would be invalid string "Forty two twelve three hundred thousand" </summary>
        /// <param name="words"> The words. </param>
        /// <returns> The <see cref="bool"/>. </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        private bool WordsInValidOrder(string[] words)
        {
            // TODO
            return true;
        }
        #endregion
    }
}
