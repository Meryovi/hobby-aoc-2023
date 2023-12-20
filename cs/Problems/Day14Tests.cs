namespace AOC2023.Problems;

public class Day14Tests(ITestOutputHelper output)
{
    private readonly Day14 sut = new(output);

    [Theory, InlineData(405)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day14_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(29213)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day14_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
