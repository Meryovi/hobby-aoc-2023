namespace AOC2023.Problems;

public class Day4Tests
{
    private readonly Day4 sut = new();

    [Theory, InlineData(13)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day4_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(17803)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day4_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
