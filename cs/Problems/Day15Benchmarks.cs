namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day15Benchmarks
{
    private readonly Day15 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day15_1");

    [Benchmark(Description = "Day15 problem")]
    public void Measure() => problem.Solve(input);
}
