namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day23Benchmarks
{
    private readonly Day23 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day23_1");

    [Benchmark(Description = "Day23 problem")]
    public void Measure() => problem.Solve(input);
}
