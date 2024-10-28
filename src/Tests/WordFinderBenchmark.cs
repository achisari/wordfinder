using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Configuration;
using WordFinderApp.Core;

namespace Tests
{
    public class WordFinderBenchmark
    {
        private readonly WordFinder wordFinder;
        private readonly WordFinderLinq wordFinderLinq;
        private readonly List<string> largeMatrix;
        private readonly List<string> wordStream;

        public WordFinderBenchmark()
        {
            // Creates a random 64x64 matrix
            largeMatrix = new List<string>();
            var random = new Random();
            for (int i = 0; i < 64; i++)
            {
                var row = new char[64];
                for (int j = 0; j < 64; j++)
                {
                    // Letters from 'a' to 'z'
                    row[j] = (char)('a' + random.Next(0, 26)); 
                }
                largeMatrix.Add(new string(row));
            }

            // Same word stream for both benchmarks 
            wordStream = new List<string>();

            for (int i = 0; i < 2000; i++)
            {
                // Word length from 3 to 8 characters
                int length = random.Next(3, 8);  

                var word = new char[length];

                for (int j = 0; j < length; j++)
                {
                    word[j] = (char)('a' + random.Next(0, 26));
                }

                wordStream.Add(new string(word));
            }

            // Same data for both instances
            wordFinder = new WordFinder(largeMatrix);
            wordFinderLinq = new WordFinderLinq(largeMatrix);
        }

        [Benchmark]
        public void BenchmarkLinearFind()
        {
            wordFinder.Find(wordStream);
        }

        [Benchmark]
        public void BenchmarkLinearFind_WithLinq()
        {
            wordFinderLinq.Find(wordStream);
        }
    }
}