namespace AOC2023.Problems;

public class Day13 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => SumOfReflectionValues(input);

    private static int SumOfReflectionValues(ReadOnlySpan<char> input)
    {
        Span<Range> puzzleRanges = stackalloc Range[100];
        Span<Range> lineRanges = stackalloc Range[20];
        int puzzles = input.Split(puzzleRanges, Environment.NewLine + Environment.NewLine);

        int summary = 0;

        for (int i = 0; i < puzzles; i++)
        {
            int lines = input[puzzleRanges[i]].Split(lineRanges, Environment.NewLine);

            var puzzle = new char[lines][];

            for (int x = 0; x < lines; x++)
                puzzle[x] = input[puzzleRanges[i]][lineRanges[x]].ToArray();

            int mirrorValue = GetHorizontalMirrorValue(puzzle);

            if (mirrorValue < 0)
                mirrorValue = GetVerticalMirrorValue(puzzle);

            summary += mirrorValue;
        }

        return summary;
    }

    private static int GetHorizontalMirrorValue(char[][] input)
    {
        for (int i = 0; i < input.Length - 1; i++)
        {
            for (int ji = i + 1, jd = i; ; jd--, ji++)
            {
                int reflections = 0;

                for (int x = 0; x < input[0].Length; x++)
                {
                    if (input[jd][x] != input[ji][x])
                        break;

                    reflections++;
                }

                if (reflections != input[0].Length)
                    break;

                if (jd == 0 || ji == input.Length - 1)
                    return (i + 1) * 100;
            }
        }

        return -1;
    }

    private static int GetVerticalMirrorValue(char[][] input)
    {
        for (int i = 0; i < input[0].Length - 1; i++)
        {
            for (int ji = i + 1, jd = i; ; jd--, ji++)
            {
                int reflections = 0;

                foreach (var line in input)
                {
                    if (line[ji] != line[jd])
                        break;

                    reflections++;
                }

                if (reflections != input.Length)
                    break;

                if (jd == 0 || ji == input[0].Length - 1)
                    return i + 1;
            }
        }

        return -1;
    }
}
