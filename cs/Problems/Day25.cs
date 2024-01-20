namespace AOC2023.Problems;

public class Day25 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => ProductOfComponentGroups(input);

    private int ProductOfComponentGroups(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[1275];
        Span<Range> splitRanges = stackalloc Range[10];
        int lines = input.Split(lineRanges, InputReader.NewLine);

        var edges = new List<(int From, int To)>(lines * 3);

        for (int i = 0; i < lines; i++)
        {
            var line = input[lineRanges[i]];
            int connectionCount = line.Split(splitRanges, " ");

            for (int j = 1; j < connectionCount; j++)
                edges.Add((string.GetHashCode(line[..3]), string.GetHashCode(line[splitRanges[j]])));
        }

        var mergeAlgorithm = new KargerAlgorithm(edges);
        var (cutSize, group1Size, group2Size) = (0, 0, 0);

        while (cutSize != 3)
            (cutSize, group1Size, group2Size) = mergeAlgorithm.FindMinimumCut();

        return group1Size * group2Size;
    }

    // Found this algorithm somewhere else.
    readonly struct KargerAlgorithm
    {
        private readonly int initialVerticesCount;
        private readonly List<(int From, int To)> initialEdges;
        private readonly Random rng = new(Seed: 72);

        private readonly SortedList<int, List<int>> merged = [];
        private readonly List<(int From, int To)> cache1 = [];
        private readonly List<(int From, int To)> cache2 = [];

        public KargerAlgorithm(List<(int From, int To)> edges)
        {
            initialEdges = edges;

            var uniqueVertices = new HashSet<int>();
            foreach (var (from, to) in edges)
            {
                initialVerticesCount += uniqueVertices.Add(from) ? 1 : 0;
                initialVerticesCount += uniqueVertices.Add(to) ? 1 : 0;
            }
        }

        public readonly (int CutSize, int Group1Count, int Group2Count) FindMinimumCut()
        {
            var mergedEdges = initialEdges;
            var mergedVerticesCount = initialVerticesCount;

            // Clears and reuses list instances. Not amazing but otherwise this becomes a memory hog.
            merged.Clear();
            cache1.Clear();
            cache2.Clear();

            while (mergedVerticesCount > 2)
            {
                var (from, to) = mergedEdges[rng.Next(0, mergedEdges.Count)];

                if (!merged.ContainsKey(from))
                    merged[from] = [];

                merged[from].Add(to);

                if (merged.TryGetValue(to, out List<int>? connections))
                {
                    merged[from].AddRange(connections);
                    merged.Remove(to);
                }

                var newEdges = cache1 == mergedEdges ? cache2 : cache1; // Prevents creating a new list on each iter.
                newEdges.Clear();

                foreach (var edge in mergedEdges)
                {
                    if (edge.To == to)
                    {
                        if (edge.From != from)
                            newEdges.Add((edge.From, from));
                    }
                    else if (edge.From == to)
                    {
                        if (from != edge.To)
                            newEdges.Add((from, edge.To));
                    }
                    else
                    {
                        newEdges.Add(edge);
                    }
                }

                (mergedEdges, _) = (newEdges, mergedEdges);
                mergedVerticesCount--;
            }

            var vertexGroups = merged.Values;
            return (mergedEdges.Count, vertexGroups[0].Count + 1, vertexGroups[^1].Count + 1);
        }
    }
}
