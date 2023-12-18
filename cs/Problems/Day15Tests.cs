namespace AOC2023.Problems;

public class Day15Tests
{
    private readonly Day15 sut = new();

    [Theory, InlineData(1320)]
    public void TestSet_ShouldYield_Result(long expected)
    {
        var input = InputReader.ReadProblemInput("day15_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(505379)]
    public void FullSet_ShouldYield_Result(long expected)
    {
        var input = InputReader.ReadProblemInput("day15_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
