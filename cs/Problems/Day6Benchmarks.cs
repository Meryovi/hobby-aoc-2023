namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day6Benchmarks
{
    private readonly Day6 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day6_1");

    [Benchmark(Description = "Day6 problem")]
    public void Measure() => problem.Solve(input);
}
