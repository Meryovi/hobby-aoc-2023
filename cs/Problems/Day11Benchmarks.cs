namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day11Benchmarks
{
    private readonly Day11 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day11_1");

    [Benchmark(Description = "Day11 problem")]
    public void Measure() => problem.Solve(input);
}
