namespace AOC2023.Problems;

public class Day1 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => SumFirstAndLastDigits(input);

    private static int SumFirstAndLastDigits(ReadOnlySpan<char> input)
    {
        Span<Range> gameLines = stackalloc Range[1000];
        int sum = 0;

        int lines = input.Split(gameLines, InputReader.NewLine);
        for (int c = 0; c < lines; c++)
        {
            var line = input[gameLines[c]];
            (int d1, int d2) = (0, 0);

            for (int i = 0, j = line.Length - 1; j >= 0 && (d1 == 0 || d2 == 0); i++, j--)
            {
                if (d1 == 0 && char.IsDigit(line[i]))
                    d1 = line[i] - '0';

                if (d2 == 0 && char.IsDigit(line[j]))
                    d2 = line[j] - '0';
            }

            sum += d1 * 10 + d2;
        }

        return sum;
    }
}
