namespace AOC2023.Problems;

public class Day10Tests
{
    private readonly Day10 sut = new();

    [Theory, InlineData(4, 1), InlineData(8, 2)]
    public void TestSet_ShouldYield_Result(int expected, int testCase)
    {
        var input = InputReader.ReadProblemInput("day10_" + testCase);
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(6867)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day10_3");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
