namespace AOC2023.Problems;

public class Day5 : IProblem<uint>
{
    public uint Solve(ReadOnlySpan<char> input) => FindLowestSeedLocation(input);

    private uint FindLowestSeedLocation(ReadOnlySpan<char> input)
    {
        Span<Range> linesRange = stackalloc Range[190];
        Span<Range> seedsRange = stackalloc Range[20];

        uint lowest = uint.MaxValue;
        byte currentMapType = 0;
        var maps = new Dictionary<byte, List<RangeMap>>()
        {
            { 1, new List<RangeMap>() },
            { 2, new List<RangeMap>() },
            { 3, new List<RangeMap>() },
            { 4, new List<RangeMap>() },
            { 5, new List<RangeMap>() },
            { 6, new List<RangeMap>() },
            { 7, new List<RangeMap>() },
        };

        int lines = input.Split(linesRange, Environment.NewLine);
        for (int i = 2; i < lines; i++)
        {
            var line = input[linesRange[i]];

            if (line.Contains(':'))
                currentMapType++;
            else if (!line.IsEmpty)
                maps[currentMapType].Add(RangeMap.Parse(line));
        }

        var seedsString = input[(input.IndexOf(':') + 2)..input.IndexOf(Environment.NewLine)];
        int parsedSeeds = seedsString.Split(seedsRange, ' ');
        for (int i = 0; i < parsedSeeds; i++)
        {
            uint destination = uint.Parse(seedsString[seedsRange[i]]);

            foreach (var key in maps.Keys)
                destination += GetDiffForValueInRange(maps[key], destination);

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
            Span<Range> rangeParts = stackalloc Range[3];
            mapString.Split(rangeParts, " ");

            uint destination = uint.Parse(mapString[rangeParts[0]]);
            uint source = uint.Parse(mapString[rangeParts[1]]);
            uint range = uint.Parse(mapString[rangeParts[2]]);

            return new RangeMap(source, source + range - 1, destination - source);
        }
    }
}
