namespace AOC2023.Problems;

public class Day13 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => SumOfReflectionValues(input);

    private static int SumOfReflectionValues(ReadOnlySpan<char> input)
    {
        Span<Range> puzzleRanges = stackalloc Range[100];
        Span<Range> lineRanges = stackalloc Range[20];
        int puzzles = input.Split(puzzleRanges, InputReader.NewLine + InputReader.NewLine);

        int summary = 0;

        for (int i = 0; i < puzzles; i++)
        {
            var rawPuzzle = input[puzzleRanges[i]];
            int height = rawPuzzle.Split(lineRanges, InputReader.NewLine);
            int width = rawPuzzle[lineRanges[0]].Length;

            var puzzle = new char[height, width];

            for (int x = 0; x < height; x++)
            for (int y = 0; y < width; y++)
                puzzle[x, y] = rawPuzzle[lineRanges[x]][y];

            int mirrorValue = GetHorizontalMirrorValue(puzzle, height, width);

            if (mirrorValue < 0)
                mirrorValue = GetVerticalMirrorValue(puzzle, height, width);

            summary += mirrorValue;
        }

        return summary;
    }

    private static int GetHorizontalMirrorValue(char[,] input, int height, int width)
    {
        for (int i = 0; i < height - 1; i++)
        {
            for (int ji = i + 1, jd = i; ; jd--, ji++)
            {
                int reflections = 0;

                for (int x = 0; x < width; x++)
                {
                    if (input[jd, x] != input[ji, x])
                        break;

                    reflections++;
                }

                if (reflections != width)
                    break;

                if (jd == 0 || ji == height - 1)
                    return (i + 1) * 100;
            }
        }

        return -1;
    }

    private static int GetVerticalMirrorValue(char[,] input, int height, int width)
    {
        for (int i = 0; i < width - 1; i++)
        {
            for (int ji = i + 1, jd = i; ; jd--, ji++)
            {
                int reflections = 0;

                for (int y = 0; y < height; y++)
                {
                    if (input[y, ji] != input[y, jd])
                        break;

                    reflections++;
                }

                if (reflections != height)
                    break;

                if (jd == 0 || ji == width - 1)
                    return i + 1;
            }
        }

        return -1;
    }
}
