namespace AOC2023.Problems;

public class Day20Tests
{
    private readonly Day20 sut = new();

    [Theory, InlineData(32000000, 1), InlineData(11687500, 2)]
    public void TestSet_ShouldYield_Result(long expected, int testCase)
    {
        var input = InputReader.ReadProblemInput("day20_" + testCase);
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(839775244)]
    public void FullSet_ShouldYield_Result(long expected)
    {
        var input = InputReader.ReadProblemInput("day20_3");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
