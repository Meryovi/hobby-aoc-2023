namespace AOC2023.Problems;

public class Day21 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => CountNumberOfPlotsInSteps(input);

    private static int CountNumberOfPlotsInSteps(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[131];
        int size = input.Split(lineRanges, InputReader.NewLine);

        int midpoint = size / 2;
        var nextVisits = new List<Point>(4 * 8);
        var lastVisits = new List<Point> { new(midpoint, midpoint) };

        for (int step = 0; step < 64; step++)
        {
            foreach (var visit in lastVisits)
            {
                foreach (var direction in Point.NavigableDirections)
                {
                    var next = visit.Move(direction);

                    if (next.Y >= size - 1 || next.Y < 0 || next.X >= size - 1 || next.X < 0)
                        continue;

                    var tile = input[lineRanges[next.Y]][next.X];

                    if (tile != '#' && !nextVisits.Contains(next))
                        nextVisits.Add(next);
                }
            }

            lastVisits.Clear();
            lastVisits.AddRange(nextVisits);
            nextVisits.Clear();
        }

        int numberOfPlots = lastVisits.Count;
        return numberOfPlots;
    }

    readonly record struct Point(int X, int Y)
    {
        public static readonly Direction[] NavigableDirections = [Direction.Up, Direction.Down, Direction.Left, Direction.Right];

        public Point Move(Direction direction) =>
            direction switch
            {
                Direction.Up => new(X, Y - 1),
                Direction.Down => new(X, Y + 1),
                Direction.Left => new(X - 1, Y),
                Direction.Right => new(X + 1, Y),
                _ => throw new NotImplementedException()
            };
    }

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
