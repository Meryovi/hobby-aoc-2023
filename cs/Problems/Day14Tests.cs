namespace AOC2023.Problems;

public class Day14Tests
{
    private readonly Day14 sut = new();

    [Theory, InlineData(136)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day14_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(107142)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day14_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
