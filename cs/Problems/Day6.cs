namespace AOC2023.Problems;

public class Day6 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => NumberOfWaysToWin(input);

    private static int NumberOfWaysToWin(ReadOnlySpan<char> input)
    {
        Span<int> times = stackalloc int[10];
        Span<int> distances = stackalloc int[10];

        int lineBreak = input.IndexOf(Environment.NewLine);
        int count = InputParser.ParseNumbers(times, input[5..lineBreak], separator: "  ");
        InputParser.ParseNumbers(distances, input[(lineBreak + 11)..], separator: "  ");

        int waysToWinProduct = 0;

        for (int i = 0; i < count; i++)
        {
            int wayToWin = 0;

            for (int j = 1; j < times[i] - 1; j++)
            {
                if (j * (times[i] - j) > distances[i])
                {
                    wayToWin++;
                }
            }

            waysToWinProduct = Math.Max(waysToWinProduct, 1) * Math.Max(wayToWin, 1);
        }

        return waysToWinProduct;
    }
}
