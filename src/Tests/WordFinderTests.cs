using WordFinderApp.Core;

namespace Tests
{
    public class WordFinderTests
    {
        [Fact]
        public void Find_ReturnsTop10MostRepeatedWords()
        {
            var matrix = new List<string>
            {
                "coldwuv",
                "windxhg",
                "snowmqv",
                "chillpq"
            };
            var expectedWords = new List<string> { "cold", "wind", "chill", "snow" };
            var wordStream = new List<string> { "cold", "wind", "chill", "snow" };
            var wordFinder = new WordFinder(matrix);

            var result = wordFinder.Find(wordStream);

            Assert.Equal(expectedWords, result);
        }

        [Fact]
        public void Find_ReturnsTop10MostRepeatedWords_LargeSet()
        {
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
            var wordStream = new List<string>
            {
                "snow", "storm", "rain", "cloudy", "foggy", "wind",
                "drizzle", "hail", "sunny", "chilly", "frosty", "autumn",
                "winter", "icy", "freeze", "summer", "sky", "blizzard",
                "cold", "freezing", "rainy", "night"
            };
            var expectedWords = new List<string>
            {
                "storm", "rain", "wind", "snow", "cloudy",
                "foggy", "drizzle", "hail", "chilly", "frosty"
            };
            var wordFinder = new WordFinder(matrix);

            var result = wordFinder.Find(wordStream);
    
            Assert.Equal(expectedWords, result);
        }

        [Fact]
        public void Find_ReturnsEmpty_WhenMatrixIsEmpty()
        {
            var matrix = new List<string>();
            var wordStream = new List<string> { "cold", "wind" };
            var wordFinder = new WordFinder(matrix);

            var result = wordFinder.Find(wordStream);

            Assert.Empty(result);
        }

        [Fact]
        public void Find_ReturnsEmpty_WhenWordsNotInMatrix()
        {
            var matrix = new List<string> { "abc", "def", "ghi" };
            var wordStream = new List<string> { "xyz", "uvw" };
            var wordFinder = new WordFinder(matrix);

            var result = wordFinder.Find(wordStream);

            Assert.Empty(result);
        }

        [Fact]
        public void Find_IgnoresDuplicateWordsInWordStream()
        {
            var matrix = new List<string> { "cold", "wind", "chill" };
            var wordStream = new List<string> { "cold", "cold", "chill" };
            var wordFinder = new WordFinder(matrix);

            var result = wordFinder.Find(wordStream);

            Assert.Equal(new List<string> { "cold", "chill" }, result);
        }

        [Fact]
        public void Find_WorksCorrectly_With64x64RandomMatrix()
        {
            var matrix = new List<string>();
            for (int i = 0; i < 64; i++)
            {
                var row = new string('a', 64);
                matrix.Add(row);
            }

            var wordStream = new List<string> { "aaa", "aaaa", "aaaaa" };
            var wordFinder = new WordFinder(matrix);

            var result = wordFinder.Find(wordStream);

            Assert.Equal(wordStream, result);
        }

    }
}