namespace AOC2023.Problems;

public class Day5 : IProblem<uint>
{
    public uint Solve(ReadOnlySpan<char> input) => FindLowestSeedLocation(input);

    private uint FindLowestSeedLocation(ReadOnlySpan<char> input)
    {
        Span<Range> linesRange = stackalloc Range[190];
        Span<uint> seedsRange = stackalloc uint[20];

        int currentMap = 0;
        var maps = new List<RangeDiff>[] { [], [], [], [], [], [], [], };

        int lines = input.Split(linesRange, InputReader.NewLine);
        for (int i = 3; i < lines; i++)
        {
            var line = input[linesRange[i]];

            if (line.Contains(':'))
                currentMap++;
            else if (!line.IsEmpty)
                maps[currentMap].Add(RangeDiff.Parse(line));
        }

        uint lowest = uint.MaxValue;

        var seedsString = input[(input.IndexOf(':') + 2)..input.IndexOf(InputReader.NewLine)];
        int parsedSeeds = InputParser.ParseNumbers(seedsRange, seedsString);

        for (int i = 0; i < parsedSeeds; i++)
        {
            uint destination = seedsRange[i];

            foreach (var ranges in maps)
                destination += GetDiffForValueInRange(ranges, destination);

            lowest = Math.Min(lowest, destination);
        }

        return lowest;
    }

    private static uint GetDiffForValueInRange(List<RangeDiff> ranges, uint value)
    {
        foreach (var range in ranges)
            if (value >= range.Start && value <= range.End)
                return range.Diff;

        return 0;
    }

    readonly record struct RangeDiff(uint Start, uint End, uint Diff)
    {
        public static RangeDiff Parse(ReadOnlySpan<char> mapString)
        {
            int separator1 = mapString.IndexOf(' ');
            int separator2 = mapString.LastIndexOf(' ');

            uint destination = uint.Parse(mapString[..separator1]);
            uint source = uint.Parse(mapString[(separator1 + 1)..separator2]);
            uint range = uint.Parse(mapString[(separator2 + 1)..]);

            return new RangeDiff(source, source + range - 1, destination - source);
        }
    }
}
