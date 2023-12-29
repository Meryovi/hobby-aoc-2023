namespace AOC2023.Problems;

public class Day21Tests
{
    private readonly Day21 sut = new();

    [Theory, InlineData(29)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day21_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(3776)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day21_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
