namespace AOC2023.Problems;

public class Day6(ITestOutputHelper output) : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => NumberOfWaysToWin(input);

    private int NumberOfWaysToWin(ReadOnlySpan<char> input)
    {
        var lineBreak = input.IndexOf(Environment.NewLine);
        var times = InputParser.ParseNumbers(input[5..lineBreak].TrimStart(), maxSize: 4);
        var distances = InputParser.ParseNumbers(input[(lineBreak + 11)..].TrimStart(), maxSize: 4);

        int totalWins = 0;

        for (int i = 0; i < times.Count; i++)
        {
            int wins = 0;

            for (int j = 1; j < times[i]; j++)
                if (j * (times[i] - j) > distances[i])
                    wins++;

            output.WriteLine(times[i] + " wins " + wins);
            totalWins = Math.Max(totalWins, 1) * Math.Max(wins, 1);
        }

        return totalWins + 1;
    }
}
