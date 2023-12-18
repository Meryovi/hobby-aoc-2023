namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day8Benchmarks
{
    private readonly Day8 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day8_1");

    [Benchmark(Description = "Day8 problem")]
    public void Measure() => problem.Solve(input);
}
