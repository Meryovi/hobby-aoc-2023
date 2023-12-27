namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day20Benchmarks
{
    private readonly Day20 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day20_1");

    [Benchmark(Description = "Day20 problem")]
    public void Measure() => problem.Solve(input);
}
