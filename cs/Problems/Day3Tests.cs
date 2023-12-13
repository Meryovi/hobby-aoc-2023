namespace AOC2023.Problems;

public class Day3Tests
{
    private readonly Day3 sut = new();

    [Theory, InlineData(4361)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day3_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(533784)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day3_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
