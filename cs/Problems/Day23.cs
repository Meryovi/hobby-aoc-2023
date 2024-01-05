namespace AOC2023.Problems;

public class Day23(ITestOutputHelper? output = null) : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => FindLongestHikeTrail(input);

    private int FindLongestHikeTrail(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[141];
        int lines = input.Split(lineRanges, Environment.NewLine);

        var matrix = new char[lines, lines];
        var entry = new Point(input[lineRanges[0]].IndexOf('.'), 0);
        var goal = new Point(input[lineRanges[lines - 1]].IndexOf('.'), lines - 1);

        for (int j = 0; j < lines; j++)
        for (int i = 0; i < lines; i++)
            matrix[j, i] = input[lineRanges[j]][i];

        int longestPath = 0;
        var queue = new Queue<Path>();
        queue.Enqueue(new Path(entry, 0));

        int loopBreaker = 0;
        while (queue.TryDequeue(out var current) && loopBreaker++ < 175_000)
        {
            if (current.Position == goal)
            {
                longestPath = Math.Max(longestPath, current.Steps);
                continue;
            }

            var (next1, next2, next3, next4) = current.GetNextSteps(matrix);

            if (next1 != null)
                queue.Enqueue(next1.Value);
            if (next2 != null)
                queue.Enqueue(next2.Value);
            if (next3 != null)
                queue.Enqueue(next3.Value);
            if (next4 != null)
                queue.Enqueue(next4.Value);
        }

        return longestPath;
    }

    struct Path(Point position, int steps = 0, Direction direction = Direction.Down, HashSet<Point>? pastSteps = null)
    {
        public readonly Point Position => position;

        public readonly int Steps => steps;

        public readonly Direction Direction => direction;

        public readonly HashSet<Point> PastSteps = pastSteps is null ? [position] : [..pastSteps, position];

        public readonly (Path?, Path?, Path?, Path?) GetNextSteps(char[,] matrix) =>
            matrix[Position.Y, Position.X] switch
            {
                '>' => (Move(Direction.Right, matrix), null, null, null),
                '<' => (Move(Direction.Left, matrix), null, null, null),
                'v' => (Move(Direction.Down, matrix), null, null, null),
                '^' => (Move(Direction.Up, matrix), null, null, null),
                '.' => GetBifurcations(matrix),
                _ => throw new NotImplementedException()
            };

        readonly Path? Move(Direction direction, char[,] matrix) => TryBifurcate(direction, matrix);

        readonly (Path?, Path?, Path?, Path?) GetBifurcations(char[,] matrix)
        {
            var north = Direction != Direction.Down ? TryBifurcate(Direction.Up, matrix) : null;
            var south = Direction != Direction.Up ? TryBifurcate(Direction.Down, matrix) : null;
            var east = Direction != Direction.Right ? TryBifurcate(Direction.Left, matrix) : null;
            var west = Direction != Direction.Left ? TryBifurcate(Direction.Right, matrix) : null;
            return (north, south, east, west);
        }

        readonly Path? TryBifurcate(Direction direction, char[,] matrix)
        {
            var newPosition = Position.Move(direction);

            if (!CanMoveToPosition(newPosition, matrix))
                return null;

            var bifurcation = new Path(newPosition, Steps + 1, direction, PastSteps);
            return bifurcation;
        }

        readonly bool CanMoveToPosition(Point position, char[,] matrix)
        {
            int size = matrix.GetLength(0);

            if (position.X < 0 || position.X > size - 1 || position.Y < 0 || position.Y > size - 1)
                return false;

            var current = matrix[position.Y, position.X];

            if (current == '#' || PastSteps.Contains(position))
                return false;

            return true;
        }
    }

    readonly record struct Point(int X, int Y)
    {
        public static Direction[] AllDirections = [Direction.Up, Direction.Down, Direction.Left, Direction.Right];

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
