namespace AOC2023.Problems;

public class Day6 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => NumberOfWaysToWin(input);

    private static int NumberOfWaysToWin(ReadOnlySpan<char> input)
    {
        var lineBreak = input.IndexOf(Environment.NewLine);
        var times = InputParser.ParseNumbers<ushort>(input[5..lineBreak], maxSize: 10, separator: "  ");
        var distances = InputParser.ParseNumbers<ushort>(input[(lineBreak + 11)..], maxSize: 10, separator: "  ");

        int waysToWinProduct = 0;

        for (int i = 0; i < times.Length; i++)
        {
            int wayToWin = 0;

            for (int j = times[i] / 2; j > 0; j--)
            {
                if (j * (times[i] - j) <= distances[i])
                {
                    wayToWin += (times[i] / 2) - j;
                    break;
                }
            }

            for (int k = times[i] / 2 + 1; k < times[i]; k++)
            {
                if (k * (times[i] - k) <= distances[i])
                {
                    wayToWin += k - (times[i] / 2 + 1);
                    break;
                }
            }

            waysToWinProduct = Math.Max(waysToWinProduct, 1) * Math.Max(wayToWin, 1);
        }

        return waysToWinProduct;
    }
}
