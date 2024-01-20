namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day17Benchmarks
{
    private readonly Day17 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day17_1").Split(InputReader.NewLine)[0]; // Benchmark using the first line only.

    [Benchmark(Description = "Day17 problem")]
    public void Measure() => problem.Solve(input);
}
