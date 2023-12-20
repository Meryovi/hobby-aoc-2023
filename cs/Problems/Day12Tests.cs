namespace AOC2023.Problems;

public class Day12Tests
{
    private readonly Day12 sut = new();

    [Theory, InlineData(21)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day12_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(7017)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day12_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
