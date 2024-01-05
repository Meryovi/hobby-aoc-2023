namespace AOC2023.Problems;

[MemoryDiagnoser, ShortRunJob]
[MinColumn, MaxColumn]
public class Day22Benchmarks
{
    private readonly Day22 problem = new();

    private readonly string input = InputReader.ReadProblemInput("day22_1");

    [Benchmark(Description = "Day22 problem")]
    public void Measure() => problem.Solve(input);
}
