namespace AOC2023.Problems;

public class Day18Tests(ITestOutputHelper output)
{
    private readonly Day18 sut = new(output);

    [Theory, InlineData(62)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day18_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(970)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day18_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
