namespace AOC2023.Problems;

public class Day11 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => SumDistanceBetweenGalaxies(input);

    private int SumDistanceBetweenGalaxies(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[140];
        int lines = input.Split(lineRanges, InputReader.NewLine);

        var emptyCols = new List<int>(lines);
        var emptyRows = new List<int>(lines);

        for (int j = 0; j < lines; j++)
        {
            emptyCols.Add(lines - j);
            emptyRows.Add(lines - j);
        }

        Span<Point> galaxies = stackalloc Point[450];
        int galaxyCount = 0;

        for (int j = lines - 1; j >= 0; j--)
        {
            var line = input[lineRanges[j]];
            for (int i = line.Length - 1; i >= 0; i--)
            {
                if (line[i] == '#')
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
                galaxy.Y += col <= galaxy.Y ? 1 : 0;

            foreach (int row in emptyRows)
                galaxy.X += row <= galaxy.X ? 1 : 0;

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
