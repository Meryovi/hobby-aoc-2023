namespace AOC2023.Problems;

public class Day9Tests(ITestOutputHelper output)
{
    private readonly Day9 sut = new(output);

    [Theory, InlineData(114)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day9_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    // [Theory, InlineData(250957639)]
    // public void FullSet_ShouldYield_Result(int expected)
    // {
    //     var input = InputReader.ReadProblemInput("day9_2");
    //     var result = sut.Solve(input);

    //     Assert.Equal(expected, result);
    // }
}
