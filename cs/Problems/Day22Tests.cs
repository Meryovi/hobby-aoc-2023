namespace AOC2023.Problems;

public class Day22Tests
{
    private readonly Day22 sut = new();

    [Theory, InlineData(5)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day22_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(534)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day22_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
