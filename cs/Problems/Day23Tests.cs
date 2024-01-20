namespace AOC2023.Problems;

public class Day23Tests
{
    private readonly Day23 sut = new();

    [Theory, InlineData(94)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day23_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(2362)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day23_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
