namespace AOC2023.Problems;

public class Day11Tests
{
    private readonly Day11 sut = new();

    [Theory, InlineData(374)]
    public void TestSet_ShouldYield_Result(long expected)
    {
        var input = InputReader.ReadProblemInput("day11_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(10292708)]
    public void FullSet_ShouldYield_Result(long expected)
    {
        var input = InputReader.ReadProblemInput("day11_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
