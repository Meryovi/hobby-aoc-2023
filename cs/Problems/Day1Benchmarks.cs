namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day1Benchmarks
{
    private readonly Day1 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day1_1");

    [Benchmark(Description = "Day1 problem")]
    public void Measure() => problem.Solve(input);
}
