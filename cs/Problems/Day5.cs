namespace AOC2023.Problems;

public class Day5 : IProblem<uint>
{
    public uint Solve(ReadOnlySpan<char> input) => FindLowestSeedLocation(input);

    private uint FindLowestSeedLocation(ReadOnlySpan<char> input)
    {
        Span<Range> linesRange = stackalloc Range[190];
        Span<uint> seedsRange = stackalloc uint[20];

        int currentMap = 0;
        var maps = new List<RangeMap>[] { [], [], [], [], [], [], [], };

        int lines = input.Split(linesRange, Environment.NewLine);
        for (int i = 3; i < lines; i++)
        {
            var line = input[linesRange[i]];

            if (line.Contains(':'))
                currentMap++;
            else if (!line.IsEmpty)
                maps[currentMap].Add(RangeMap.Parse(line));
        }

        uint lowest = uint.MaxValue;

        var seedsString = input[(input.IndexOf(':') + 2)..input.IndexOf(Environment.NewLine)];
        int parsedSeeds = InputParser.ParseNumbers(seedsRange, seedsString);

        for (int i = 0; i < parsedSeeds; i++)
        {
            var destination = seedsRange[i];

            foreach (var item in maps)
                destination += GetDiffForValueInRange(item, destination);

            lowest = Math.Min(lowest, destination);
        }

        return lowest;
    }

    private static uint GetDiffForValueInRange(List<RangeMap> rangeMaps, uint value)
    {
        foreach (var map in rangeMaps)
            if (value >= map.Start && value <= map.End)
                return map.Diff;

        return 0;
    }

    readonly record struct RangeMap(uint Start, uint End, uint Diff)
    {
        public static RangeMap Parse(ReadOnlySpan<char> mapString)
        {
            int separator1 = mapString.IndexOf(' ');
            int separator2 = mapString.LastIndexOf(' ');

            uint destination = uint.Parse(mapString[..separator1]);
            uint source = uint.Parse(mapString[(separator1 + 1)..separator2]);
            uint range = uint.Parse(mapString[(separator2 + 1)..]);

            return new RangeMap(source, source + range - 1, destination - source);
        }
    }
}
