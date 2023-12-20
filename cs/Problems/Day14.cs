namespace AOC2023.Problems;

public class Day14(ITestOutputHelper? output) : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => TiltPlatformAndSumRocksWeight(input);

    private int TiltPlatformAndSumRocksWeight(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[100];
        int lines = input.Split(lineRanges, Environment.NewLine);

        var matrix = new char[lines][];

        for (int i = 0; i < lines; i++)
            matrix[i] = input[lineRanges[i]].ToArray();

        for (int i = lines - 1; i > 0; i--)
        {
            var line = matrix[i];
            var prev = matrix[i - 1];
            for (int j = 0; j < line.Length; j++)
            {
                (line[j], prev[j]) = (line[j], prev[j]) switch
                {
                    ('O', '.') => ('.', 'O'),
                    ('O', '#') => ('O', '#'),
                    ('.', _) => (line[j], prev[j]),
                    ('#', _) => (line[j], prev[j]),
                    _ => (line[j], prev[j])
                };
            }
        }

        int sumOfWeight = 0;

        output?.WriteLine("sum: " + sumOfWeight);
        return sumOfWeight;
    }
}
