namespace AOC2023.Problems;

public class Day9 : IProblem<long>
{
    public long Solve(ReadOnlySpan<char> input) => PredictEnvironmentalInstabilities(input);

    private static long PredictEnvironmentalInstabilities(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[200];
        int lines = input.Split(lineRanges, InputReader.NewLine);

        Span<int> environmentReadings = stackalloc int[25];
        long totalInstabilities = 0;

        for (int i = 0; i < lines; i++)
        {
            int readings = InputParser.ParseNumbers(environmentReadings, input[lineRanges[i]]);
            int prediction = PredictNextSequenceValue(environmentReadings, readings);
            totalInstabilities += prediction;
        }

        return totalInstabilities;
    }

    private static int PredictNextSequenceValue(ReadOnlySpan<int> sequence, int readings)
    {
        Span<int> differences = stackalloc int[readings - 1];

        for (int i = 1; i < readings; i++)
            differences[i - 1] = sequence[i] - sequence[i - 1];

        int prediction = 0;

        if (differences[readings - 2] != 0)
            prediction = PredictNextSequenceValue(differences, readings - 1);

        return sequence[readings - 1] + prediction;
    }
}
