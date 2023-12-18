namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day5Benchmarks
{
    private readonly Day5 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day5_1");

    [Benchmark(Description = "Day5 problem")]
    public void Measure() => problem.Solve(input);
}
