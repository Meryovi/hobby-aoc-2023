namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day24Benchmarks
{
    private readonly Day24 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day24_1");

    [Benchmark(Description = "Day24 problem")]
    public void Measure() => problem.Solve(input);
}
