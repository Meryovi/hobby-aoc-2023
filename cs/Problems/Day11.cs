namespace AOC2023.Problems;

public class Day11 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => SumDistanceBetweenGalaxies(input);

    private int SumDistanceBetweenGalaxies(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[140];
        int lines = input.Split(lineRanges, Environment.NewLine);

        var range = Enumerable.Range(0, lines).Reverse().ToArray();
        var emptyCols = new HashSet<int>(range);
        var emptyRows = new HashSet<int>(range);

        var galaxies = new List<Point>();

        for (int i = 0; i < lines; i++)
        {
            var line = input[lineRanges[i]];
            for (int j = 0; j < line.Length; j++)
            {
                if (line[j] == '#')
                {
                    galaxies.Add(new(i, j));
                    emptyCols.Remove(j);
                    emptyRows.Remove(i);
                }
            }
        }

        for (int i = 0; i < galaxies.Count; i++) // Universe expansion.
        {
            var galaxy = galaxies[i];

            foreach (int col in emptyCols)
                galaxy.Y += col > galaxy.Y ? 0 : 1;

            foreach (int row in emptyRows)
                galaxy.X += row > galaxy.X ? 0 : 1;

            galaxies[i] = galaxy;
        }

        int sumOfDistances = 0;

        for (int i = 0; i < galaxies.Count - 1; i++)
        {
            for (int j = i + 1; j < galaxies.Count; j++)
            {
                int distance = Math.Abs(galaxies[j].X - galaxies[i].X) + Math.Abs(galaxies[j].Y - galaxies[i].Y);
                sumOfDistances += distance;
            }
        }

        return sumOfDistances;
    }

    record struct Point(int X, int Y);
}
