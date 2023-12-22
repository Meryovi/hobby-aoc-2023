namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day16Benchmarks
{
    private readonly Day16 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day16_1");

    [Benchmark(Description = "Day16 problem")]
    public void Measure() => problem.Solve(input);
}
