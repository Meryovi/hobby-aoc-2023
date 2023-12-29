namespace AOC2023.Problems;

public class Day24Tests(ITestOutputHelper output)
{
    private readonly Day24 sut = new(output);

    [Theory, InlineData(29)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day24_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(3776)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day24_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
