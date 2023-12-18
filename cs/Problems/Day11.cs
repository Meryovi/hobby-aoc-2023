namespace AOC2023.Problems;

public class Day11 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => SumDistanceBetweenGalaxies(input);

    private int SumDistanceBetweenGalaxies(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[140];
        int lines = input.Split(lineRanges, Environment.NewLine);

        var range = new int[lines];

        for (int i = 0; i < lines; i++)
            range[i] = lines - i;

        var emptyCols = new List<int>(range);
        var emptyRows = new List<int>(range);

        Span<Point> galaxies = stackalloc Point[450];
        int galaxyCount = 0;

        for (int i = lines - 1; i >= 0; i--)
        {
            var line = input[lineRanges[i]];
            for (int j = line.Length - 1; j >= 0; j--)
            {
                if (line[j] == '#')
                {
                    galaxies[galaxyCount++] = new Point(i, j);
                    emptyCols.Remove(j);
                    emptyRows.Remove(i);
                }
            }
        }

        for (int i = 0; i < galaxyCount; i++) // Universe expansion.
        {
            var galaxy = galaxies[i];

            foreach (int col in emptyCols)
                galaxy.Y += col > galaxy.Y ? 0 : 1;

            foreach (int row in emptyRows)
                galaxy.X += row > galaxy.X ? 0 : 1;

            galaxies[i] = galaxy;
        }

        int sumOfDistances = 0;

        for (int i = 0; i < galaxyCount - 1; i++)
        {
            for (int j = i + 1; j < galaxyCount; j++)
            {
                int distance = Math.Abs(galaxies[j].X - galaxies[i].X) + Math.Abs(galaxies[j].Y - galaxies[i].Y);
                sumOfDistances += distance;
            }
        }

        return sumOfDistances;
    }

    record struct Point(int X, int Y);
}
