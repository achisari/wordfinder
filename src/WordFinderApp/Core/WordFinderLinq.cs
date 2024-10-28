using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinderApp.Core
{
    /// <summary>
    /// Represents a utility class for finding specified words in a character matrix.
    /// The algorithm performs a sequential linear search in the matrix of n rows and m columns to find the specified words.
    /// Given a list of words, it searches for their occurrences and returns the top N most frequently found words.
    /// The search is performed horizontally from left to right and vertically from top to bottom.
    /// 
    /// Complexity:
    /// O(n * m * w):
    /// n = Number of rows in the matrix.
    /// m = Length of each row (number of columns).
    /// w = Number of words in the list.
    /// 
    /// The execution time of the algorithm grows linearly with the product
    /// of the number of rows, the length of each row, and the number of words.
    /// </summary>
    public class WordFinderLinq
    {
        private readonly List<string> _matrix;
        private readonly List<string> _matrixColumns;
     
        /// <summary>
        /// Initializes a new instance of the <see cref="WordFinderLinq"/> class.
        /// </summary>
        /// <param name="matrix">A set of strings representing the character matrix.</param>
        public WordFinderLinq(IEnumerable<string> matrix)
        {
            // Convert the input matrix to a list for easier access by index, if necessary
            _matrix = matrix as List<string> ?? new List<string>(matrix);

            // Check if the matrix is empty and return if it is
            if (_matrix.Count == 0)
            {
                _matrixColumns = [];
                return;
            }

            // Builds column strings for vertical word search in the matrix
            _matrixColumns = new List<string>();
            for (int col = 0; col < _matrix[0].Length; col++)
            {
                var column = string.Concat(_matrix.Select(row => row[col]));
                _matrixColumns.Add(column);
            }
        }

        /// <summary>
        /// Searches the character matrix for words found in the specified word stream
        /// and returns the top N most frequently occurring words.
        /// </summary>
        /// <param name="wordstream">A collection of words to search for within the matrix.</param>
        /// <param name="topN">The maximum number of most frequent words to return. Defaults to 10.</param>
        /// <returns>
        /// An enumerable collection of the top N words from the word stream that are found
        /// in the matrix, ordered by frequency in descending order. If no words are found,
        /// returns an empty collection.
        /// </returns>
        public IEnumerable<string> Find(IEnumerable<string> wordstream, int topN = 10)
        {
            var wordFrequency = new Dictionary<string, int>();

            foreach (var word in wordstream.Distinct())
            {
                // Counts occurrences of each distinct word in both rows and columns
                int count = CountOccurrences(word, _matrix) + CountOccurrences(word, _matrixColumns);

                // Adds the word to the dictionary if found at least once
                if (count > 0)
                {
                    wordFrequency[word] = count;
                }
            }

            // Returns the top N most frequent words
            return wordFrequency
                .OrderByDescending(kv => kv.Value)
                .Take(topN)
                .Select(kv => kv.Key);
        }

        /// <summary>
        /// Counts the occurrences of a word in each line of the matrix.
        /// Complexity: O(n * m), where 'n' is the number of lines and 'm' is the length of each line.
        /// </summary>
        private int CountOccurrences(string word, List<string> lines)
        {
            int count = 0;
            int wordLength = word.Length;

            foreach (var line in lines)
            {
                int index = 0;
                while ((index = line.IndexOf(word, index, StringComparison.Ordinal)) != -1)
                {
                    count++;
                    index += wordLength;
                }
            }
            return count;
        }
    }
}
