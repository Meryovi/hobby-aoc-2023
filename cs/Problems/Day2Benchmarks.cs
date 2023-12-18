namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day2Benchmarks
{
    private readonly Day2 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day2_1");

    [Benchmark(Description = "Day2 problem")]
    public void Measure() => problem.Solve(input);
}
