namespace AOC2023.Problems;

public class Day8Tests
{
    private readonly Day8 sut = new();

    [Theory, InlineData(2, 1), InlineData(6, 2)]
    public void TestSet_ShouldYield_Result(int expected, int testCase)
    {
        var input = InputReader.ReadProblemInput("day8_" + testCase);
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(14893)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day8_3");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
