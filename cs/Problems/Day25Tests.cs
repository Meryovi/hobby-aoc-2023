namespace AOC2023.Problems;

public class Day25Tests
{
    private readonly Day25 sut = new();

    [Theory, InlineData(54)]
    public void TestSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day25_1");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }

    [Theory, InlineData(619225)]
    public void FullSet_ShouldYield_Result(int expected)
    {
        var input = InputReader.ReadProblemInput("day25_2");
        var result = sut.Solve(input);

        Assert.Equal(expected, result);
    }
}
