namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day25Benchmarks
{
    private readonly Day25 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day25_1");

    [Benchmark(Description = "Day25 problem")]
    public void Measure() => problem.Solve(input);
}
