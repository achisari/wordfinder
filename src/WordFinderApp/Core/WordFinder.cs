using System;
using System.Collections.Generic;
using System.Text;

namespace WordFinderApp.Core
{
    /// <summary>
    /// Represents a utility class for finding specified words in a character matrix.
    /// The algorithm performs a sequential linear search in the matrix of n rows and m columns to find the specified words.
    /// Given a list of words, it searches for their occurrences and returns the top N most frequently found words.
    /// The search is performed horizontally from left to right and vertically from top to bottom.
    /// 
    /// The algorithm avoids the use of LINQ for searching and sorting operations. Instead, it uses manual implementation
    /// for word frequency counting and ordering, ensuring more control over the execution flow and potentially reducing
    /// overhead in performance-critical scenarios.
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
    public class WordFinder
    {
        private readonly List<string> _matrix;
        private readonly List<string> _matrixColumns;

        /// <summary>
        /// Initializes a new instance of the <see cref="WordFinder"/> class.
        /// </summary>
        /// <param name="matrix">A set of strings representing the character matrix.</param>
        public WordFinder(IEnumerable<string> matrix)
        {
            // Convert the input matrix to a list for easier access by index, if necessary
            _matrix = matrix as List<string> ?? new List<string>(matrix);

            // Check if the matrix is empty and return if it is
            if (_matrix.Count == 0)
            {
                _matrixColumns = [];
                return;
            }

            // Build column strings for vertical word search in the matrix
            int numRows = _matrix.Count;
            int numCols = _matrix[0].Length;
            _matrixColumns = new List<string>(numCols);

            // StringBuilder for better efficiency
            for (int col = 0; col < numCols; col++)
            {
                var columnBuilder = new StringBuilder(numRows);
                for (int row = 0; row < numRows; row++)
                {
                    columnBuilder.Append(_matrix[row][col]);
                }
                _matrixColumns.Add(columnBuilder.ToString());
            }
        }

        /// <summary>
        /// Searches the character matrix for words found in the specified word stream
        /// and returns the top N most frequently occurring words.
        /// </summary>
        /// <param name="wordStream">A collection of words to search for within the matrix.</param>
        /// <param name="topN">The maximum number of most frequent words to return. Defaults to 10.</param>
        /// <returns>
        /// A collection of the top N words from the word stream that are found
        /// in the matrix, ordered by frequency in descending order. If no words are found,
        /// returns an empty collection.
        /// </returns>
        public IEnumerable<string> Find(IEnumerable<string> wordStream, int topN = 10)
        {
            var wordFrequency = new Dictionary<string, int>();

            // HashSet to avoid duplicates without the need for Distinct()
            var uniqueWords = new HashSet<string>(wordStream);

            foreach (var word in uniqueWords)
            {
                // Count occurrences of each distinct word in both rows and columns
                int count = CountOccurrences(word, _matrix) + CountOccurrences(word, _matrixColumns);

                // Add the word to the dictionary if found at least once
                if (count > 0)
                {
                    wordFrequency[word] = count;
                }
            }

            // Get the top 'N' most frequent words manually without LINQ
            var topWords = new List<string>(topN);
            foreach (var kvp in wordFrequency)
            {
                int i = 0;
                while (i < topWords.Count && wordFrequency[topWords[i]] > kvp.Value)
                {
                    i++;
                }

                // If the frequency is the same, move 'i' forward to place the word at the end of the current priority group
                while (i < topWords.Count && wordFrequency[topWords[i]] == kvp.Value)
                {
                    i++;
                }

                // Insert word in the correct position 
                topWords.Insert(i, kvp.Key);

                // // If the list exceeds the desired length, remove the last element
                if (topWords.Count > topN)
                {
                    topWords.RemoveAt(topWords.Count - 1);
                }
            }

            return topWords;
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
