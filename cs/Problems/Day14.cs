namespace AOC2023.Problems;

public class Day14 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => TiltPlatformAndSumRockWeight(input);

    private static int TiltPlatformAndSumRockWeight(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[100];
        int height = input.Split(lineRanges, InputReader.NewLine);
        int width = input[lineRanges[0]].Length;

        var matrix = new char[height, width];

        for (int j = 0; j < height; j++)
        for (int i = 0; i < width; i++)
            matrix[j, i] = input[lineRanges[j]][i];

        int weight = 0;

        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                if (matrix[j, i] != 'O')
                    continue;

                weight += height - j;

                int moves = 0;
                for (int k = j - 1; k >= 0; k--)
                {
                    if (matrix[k, i] != '.')
                        break;

                    (matrix[k, i], matrix[j - moves, i]) = (matrix[j - moves, i], matrix[k, i]);
                    weight++;
                    moves++;
                }
            }
        }

        return weight;
    }
}
