namespace AOC2023.Problems;

public class Day17 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => CalculateMinimumHeatLoss(input);

    private int CalculateMinimumHeatLoss(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[150];
        int size = input.Split(lineRanges, InputReader.NewLine);

        var partsFactory = new Point(size - 1, size - 1);
        var queue = new PriorityQueue<Crucible, int>(size * size * 2);
        var history = new HashSet<Crucible>();

        queue.Enqueue(Crucible.StartPush(Direction.Down), 0);
        queue.Enqueue(Crucible.StartPush(Direction.Left), 0);

        int minHeatLoss = int.MaxValue;

        while (queue.TryDequeue(out var crucible, out int heatLoss))
        {
            if (crucible.Location == partsFactory)
            {
                minHeatLoss = heatLoss;
                break;
            }

            foreach (var direction in NavigableDirections[crucible.Direction])
            {
                var pushed = crucible.TryPush(direction, size, maxConsecutive: 3);
                if (pushed.HasValue && history.Add(pushed.Value))
                {
                    var nextCrucible = pushed.Value;
                    int nextHeat = input[lineRanges[nextCrucible.Location.Y]][nextCrucible.Location.X] - '0';
                    queue.Enqueue(nextCrucible, heatLoss + nextHeat);
                }
            }
        }

        return minHeatLoss;
    }

    readonly record struct Crucible(Point Location, Direction Direction, int Consecutive)
    {
        public static Crucible StartPush(Direction direction) => new(new(0, 0), direction, 0);

        public Crucible? TryPush(Direction direction, int size, int maxConsecutive)
        {
            var nextStep = Location.Move(direction);

            if (nextStep.Y < 0 || nextStep.X < 0 || nextStep.Y >= size || nextStep.X >= size)
                return null;

            int nextConsecutive = direction == Direction ? Consecutive + 1 : 1;

            if (direction == Direction && nextConsecutive > maxConsecutive)
                return null;

            return new(nextStep, direction, nextConsecutive);
        }
    }

    readonly record struct Point(int Y, int X)
    {
        public Point Move(Direction direction) =>
            direction switch
            {
                Direction.Up => new(Y - 1, X),
                Direction.Down => new(Y + 1, X),
                Direction.Left => new(Y, X - 1),
                Direction.Right => new(Y, X + 1),
                _ => throw new NotImplementedException()
            };
    }

    static readonly Dictionary<Direction, Direction[]> NavigableDirections =
        new()
        {
            { Direction.Up, [Direction.Left, Direction.Right, Direction.Up] },
            { Direction.Down, [Direction.Left, Direction.Right, Direction.Down] },
            { Direction.Left, [Direction.Up, Direction.Down, Direction.Left] },
            { Direction.Right, [Direction.Up, Direction.Down, Direction.Right] },
        };

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
