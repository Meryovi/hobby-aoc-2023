namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day18Benchmarks
{
    private readonly Day18 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day18_1");

    [Benchmark(Description = "Day18 problem")]
    public void Measure() => problem.Solve(input);
}
