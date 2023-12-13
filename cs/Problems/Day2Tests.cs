namespace AOC2023.Problems;

public class Day2Tests
{
    private readonly Day2 sut = new();

    [Theory, InlineData(8)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day2_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(2331)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day2_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
