namespace AOC2023.Problems;

public class Day25 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => ProductOfComponentGroups(input);

    private int ProductOfComponentGroups(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[1275];
        Span<Range> splitRanges = stackalloc Range[10];
        int lines = input.Split(lineRanges, Environment.NewLine);

        var edges = new List<(int, int)>(lines * 3);

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

    readonly ref struct KargerAlgorithm
    {
        private readonly int initialVerticesCount;
        private readonly List<(int From, int To)> initialEdges;
        private readonly Random rng = new(Seed: 72);

        public KargerAlgorithm(List<(int From, int To)> edges)
        {
            initialEdges = edges ?? throw new ArgumentNullException(nameof(edges));

            HashSet<int> uniqueVertices = [];
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
            var merged = new SortedList<int, List<int>>();

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

                var newEdges = new List<(int From, int To)>(mergedEdges.Count - 2);

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

                mergedEdges = newEdges;
                mergedVerticesCount--;
            }

            var vertexGroups = merged.Values;
            return (mergedEdges.Count, vertexGroups[0].Count + 1, vertexGroups[^1].Count + 1);
        }
    }
}
