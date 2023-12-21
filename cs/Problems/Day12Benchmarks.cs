namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day12Benchmarks
{
    private readonly Day12 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day12_1")[..13]; // Take first one only.

    [Benchmark(Description = "Day12 problem")]
    public void Measure() => problem.Solve(input);
}
