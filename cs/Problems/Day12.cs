namespace AOC2023.Problems;

public class Day12 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => SumOfArrangementCounts(input);

    private int SumOfArrangementCounts(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[1000];
        int lines = input.Split(lineRanges, InputReader.NewLine);

        int totalArrangements = 0;

        for (int i = 0; i < lines; i++)
            totalArrangements += GetArrangementCount(input[lineRanges[i]]);

        return totalArrangements;
    }

    private static int GetArrangementCount(ReadOnlySpan<char> line)
    {
        int separator = line.IndexOf(' ');
        Span<int> numbers = stackalloc int[6];

        var arrangement = line[..separator];
        var group = InputParser.ParseNumbers(ref numbers, line[(separator + 1)..], 8, ",");

        return GetArrangementCount(arrangement, group);
    }

    private static int GetArrangementCount(ReadOnlySpan<char> arrangement, ParsedNumbers<int> group)
    {
        int wildcard = arrangement.IndexOf('?');

        if (wildcard == -1)
            return IsGroupFulfilled(arrangement, group) ? 1 : 0;

        var variation = arrangement.ToArray();

        variation[wildcard] = '#';
        int hashCount = GetArrangementCount(variation, group);

        variation[wildcard] = '.';
        int dotCount = GetArrangementCount(variation, group);

        return hashCount + dotCount;
    }

    private static bool IsGroupFulfilled(ReadOnlySpan<char> input, ParsedNumbers<int> groups)
    {
        Span<Range> groupRange = stackalloc Range[100];
        var actualGroups = input.Split(groupRange, '.', StringSplitOptions.RemoveEmptyEntries);

        if (actualGroups != groups.Length)
            return false;

        for (int i = 0; i < groups.Length; i++)
            if (input[groupRange[i]].Length != groups.Numbers[i])
                return false;

        return true;
    }
}
