namespace AOC2023.Problems;

public class Day5Tests
{
    private readonly Day5 sut = new();

    [Theory, InlineData(35)]
    public void TestSet_ShouldYield_Result(long expected)
    {
        var input = InputReader.ReadProblemInput("day5_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(486613012)]
    public void FullSet_ShouldYield_Result(long expected)
    {
        var input = InputReader.ReadProblemInput("day5_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
