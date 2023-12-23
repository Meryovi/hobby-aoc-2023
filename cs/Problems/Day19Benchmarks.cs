namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day19Benchmarks
{
    private readonly Day19 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day19_1");

    [Benchmark(Description = "Day19 problem")]
    public void Measure() => problem.Solve(input);
}
