namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day21Benchmarks
{
    private readonly Day21 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day21_1");

    [Benchmark(Description = "Day21 problem")]
    public void Measure() => problem.Solve(input);
}
