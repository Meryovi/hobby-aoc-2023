namespace AOC2023.Problems;

public class Day17(ITestOutputHelper? output) : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => CalculateMinimumHeatLoss(input);

    private int CalculateMinimumHeatLoss(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[100];
        int height = input.Split(lineRanges, Environment.NewLine);
        int width = input[lineRanges[0]].Length;

        var history = new HashSet<(int, int)>();
        var matrix = new int[height, width];

        for (int j = 0; j < height; j++)
        for (int i = 0; i < width; i++)
            matrix[j, i] = input[lineRanges[j]][i] - '0';

        int minHeatLoss = 0;
        output?.WriteLine("loss: " + minHeatLoss);
        return minHeatLoss;
    }
}
