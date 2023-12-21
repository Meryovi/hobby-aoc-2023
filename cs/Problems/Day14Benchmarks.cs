namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day14Benchmarks
{
    private readonly Day14 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day14_1");

    [Benchmark(Description = "Day14 problem")]
    public void Measure() => problem.Solve(input);
}
