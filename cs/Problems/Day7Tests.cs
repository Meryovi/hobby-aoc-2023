namespace AOC2023.Problems;

public class Day7Tests
{
    private readonly Day7 sut = new();

    [Theory, InlineData(6440)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day7_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(250957639)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day7_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
