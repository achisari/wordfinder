using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class BenchmarkRunnerProgram
    {
        static void Main(string[] args)
        {
            var config = ManualConfig.Create(DefaultConfig.Instance)
                .AddJob(Job.Default.WithRuntime(NativeAotRuntime.Net80))
                .AddDiagnoser(MemoryDiagnoser.Default);

            BenchmarkRunner.Run<WordFinderBenchmark>(config);
        }
    }
}