namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day13Benchmarks
{
    private readonly Day13 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day13_1");

    [Benchmark(Description = "Day13 problem")]
    public void Measure() => problem.Solve(input);
}
