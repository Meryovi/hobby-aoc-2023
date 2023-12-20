namespace AOC2023.Problems;

public class Day13(ITestOutputHelper output) : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => SummarizeReflections(input);

    private int SummarizeReflections(ReadOnlySpan<char> input)
    {
        var lines = input.ToString().Split(Environment.NewLine);

        int summary = 0;
        int patternStart = 0;
        int patternEnd = 0;

        do
        {
            patternEnd = Array.IndexOf(lines, string.Empty, patternStart);
            patternEnd = patternEnd == -1 ? lines.Length : patternEnd;
            summary += CalculateReflectionValue(lines, patternStart, patternEnd, output);
            patternStart = patternEnd + 1;
        } while (patternStart < lines.Length);

        output.WriteLine("sum: " + summary);
        return summary;
    }

    private static int CalculateReflectionValue(string[] lines, int y0, int yn, ITestOutputHelper output)
    {
        int width = lines[y0].Length;
        for (int i = 0; i < width - 1; i++)
        {
            // output.WriteLine($"{lines[0][i]}-{lines[0][i + inx]} ");

            if (lines[y0][i] != lines[y0][i + 1])
                continue;

            bool isReflection = true;
            for (int j = y0 + 1; j < yn; j++)
            {
                if (lines[j] == string.Empty)
                    break;

                // output.WriteLine($"{i} - {j} - {lines[j][i]}-{lines[j][i + 1]}");
                if (lines[j][i] != lines[j][i + 1])
                {
                    isReflection = false;
                    break;
                }
            }

            if (isReflection)
            {
                int value = i * 100;
                output.WriteLine("val: " + value);
                return value;
            }
        }

        for (int i = y0; i < yn - 1; i++)
        {
            output.WriteLine($"{lines[i][0]}-{lines[i + 1][0]} ");

            if (lines[i][0] != lines[i + 1][0])
                continue;

            bool isReflection = true;
            for (int j = 1; j < width; j++)
            {
                output.WriteLine($"{i} - {j} - {lines[i][j]}-{lines[i + 1][j]}");
                if (lines[i][j] != lines[i + 1][j])
                {
                    isReflection = false;
                    break;
                }
            }

            if (isReflection)
            {
                int value = i;
                output.WriteLine("val: " + value);
                return value;
            }
        }

        return 0;
    }
}
