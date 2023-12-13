namespace AOC2023.Problems;

public class Day6Tests(ITestOutputHelper output)
{
    private readonly Day6 sut = new(output);

    [Theory, InlineData(288)]
    public void TestSet_ShouldYield_Result(long expected)
    {
        var input = InputReader.ReadProblemInput("day6_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(138915)]
    public void FullSet_ShouldYield_Result(long expected)
    {
        var input = InputReader.ReadProblemInput("day6_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
