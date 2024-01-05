using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

var benchmarkConfig = ManualConfig
    .Create(DefaultConfig.Instance)
    .WithOptions(ConfigOptions.JoinSummary)
    .WithOptions(ConfigOptions.DisableLogFile);

// BenchmarkRunner.Run(typeof(Program).Assembly, benchmarkConfig); // All benchmarks
BenchmarkRunner.Run<Day22Benchmarks>(); // A specific one
