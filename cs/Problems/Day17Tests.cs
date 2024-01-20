namespace AOC2023.Problems;

public class Day17Tests
{
    private readonly Day17 sut = new();

    [Theory, InlineData(102)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day17_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(970)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day17_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
