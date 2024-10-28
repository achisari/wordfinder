using System.Diagnostics;
using WordFinderApp.Core;
using WordFinderApp.Utils;

namespace WordFinderApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 12x12 matrix definition
            var matrix = new List<string>
            {
                "snowstormblz",
                "rainycloudyy",
                "foggywinddrz",
                "hailstormsun",
                "chillyfrosty",
                "autumnwinter",
                "icydrizzlerz",
                "stormyfreeze",
                "summerskydrz",
                "blizzardcold",
                "freezingrain",
                "windynightsk"
            };

            // Weather related words to search in the matrix
            var wordStream = new List<string>
            {
                "snow", "storm", "rain", "cloudy", "foggy", "wind",
                "drizzle", "hail", "sunny", "chilly", "frosty", "autumn",
                "winter", "icy", "freeze", "summer", "sky", "blizzard",
                "cold", "freezing", "rainy", "night"
            };

            var wordFinder = new WordFinder(matrix);
            var wordFrequency = wordFinder.Find(wordStream);

            ReportGenerator.GenerateTopNReport(wordFrequency);

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
