namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day10Benchmarks
{
    private readonly Day10 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day10_1");

    [Benchmark(Description = "Day10 problem")]
    public void Measure() => problem.Solve(input);
}
