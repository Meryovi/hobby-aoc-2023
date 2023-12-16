namespace AOC2023.Problems;

public class Day9 : IProblem<long>
{
    public long Solve(ReadOnlySpan<char> input) => PredictEnvironmentalInstabilities(input);

    private static long PredictEnvironmentalInstabilities(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[200];
        int lines = input.Split(lineRanges, Environment.NewLine);

        long totalInstabilities = 0;

        for (int i = 0; i < lines; i++)
        {
            var environmentReadings = InputParser.ParseNumbers<int>(input[lineRanges[i]], maxSize: 30);
            int prediction = PredictNextSequenceValue(environmentReadings);

            totalInstabilities += prediction;
        }

        return totalInstabilities;
    }

    private static int PredictNextSequenceValue(ReadOnlySpan<int> sequence)
    {
        Span<int> differences = stackalloc int[sequence.Length - 1];

        for (int i = 1; i < sequence.Length; i++)
            differences[i - 1] = sequence[i] - sequence[i - 1];

        int prediction = 0;

        if (differences[sequence.Length - 2] != 0)
            prediction = PredictNextSequenceValue(differences);

        return sequence[^1] + prediction;
    }
}
