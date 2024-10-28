using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinderApp.Utils
{
    public static class ReportGenerator
    {
        /// <summary>
        /// Generates a report of the top N words by printing each word to the console.
        /// </summary>
        /// <param name="words">A collection of words to include in the report.</param>
        public static void GenerateTopNReport(IEnumerable<string> words)
        {
            if (words == null || !words.Any())
            {
                Console.WriteLine("No words to report.");
                return;
            }

            Console.WriteLine("Top Words Report:");
            Console.WriteLine("-----------------");

            foreach (var word in words)
            {
                Console.WriteLine(word);
            }

            Console.WriteLine("-----------------");
            Console.WriteLine("End of report.");
        }
    }
}
