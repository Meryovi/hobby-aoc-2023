namespace AOC2023.Problems;

public class Day16Tests
{
    private readonly Day16 sut = new();

    [Theory, InlineData(46)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day16_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(7067)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day16_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
