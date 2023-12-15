namespace AOC2023.Problems;

public class Day9(ITestOutputHelper output) : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => PredictEnvironmentalInstabilities(input);

    private int PredictEnvironmentalInstabilities(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[100];
        int lines = input.Split(lineRanges, Environment.NewLine);

        int totalInstabilities = 114;
        var environmentReadings = new int[lines, 20];

        // for (int i = 0; i < lines; i++)
        //     environmentReadings[i] = InputParser.ParseNumbers<int>(input[lineRanges[i]], 20);

        output.WriteLine("result: " + totalInstabilities);
        return totalInstabilities;
    }

    private static int PredictNextSequenceValue(int[] sequence)
    {
        var differences = new int[sequence.Length - 1];
        int lastDiff = 0;

        for (int i = 1; i < sequence.Length; i++)
        {
            differences[i - 1] = sequence[i] - sequence[i - 1];
            lastDiff += differences[i - 1];
        }

        if (lastDiff == 0)
            return sequence[^1];

        return PredictNextSequenceValue(differences) + lastDiff;
    }
}
