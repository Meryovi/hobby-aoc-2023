namespace AOC2023.Problems;

public class Day10 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => FindFarthestTileFromStart(input);

    private static int FindFarthestTileFromStart(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[140];
        int lines = input.Split(lineRanges, Environment.NewLine);

        var startingPoint = new Point(X: 0, Y: 0);
        for (int i = 0; i < lines; i++)
        {
            int index = input[lineRanges[i]].IndexOf('S');
            if (index > -1)
            {
                startingPoint.X = index;
                startingPoint.Y = i;
                break;
            }
        }

        int totalSteps = 0;
        var location = new Point(startingPoint.X + 1, startingPoint.Y);
        var direction = Direction.Right;

        while (location != startingPoint)
        {
            var pipe = input[lineRanges[location.Y]][location.X];

            (location, direction) = (pipe, direction) switch
            {
                ('|', _) => location.Move(direction),
                ('-', _) => location.Move(direction),
                ('L', Direction.Down) => location.Move(Direction.Right),
                ('L', Direction.Left) => location.Move(Direction.Up),
                ('J', Direction.Down) => location.Move(Direction.Left),
                ('J', Direction.Right) => location.Move(Direction.Up),
                ('7', Direction.Up) => location.Move(Direction.Left),
                ('7', Direction.Right) => location.Move(Direction.Down),
                ('F', Direction.Up) => location.Move(Direction.Right),
                ('F', Direction.Left) => location.Move(Direction.Down),
                _ => (startingPoint, direction)
            };

            totalSteps++;
        }

        int midPoint = (int)Math.Ceiling(totalSteps / 2.0);
        return midPoint;
    }

    record struct Point(int X, int Y)
    {
        public (Point, Direction) Move(Direction direction)
        {
            if (direction == Direction.Up)
                Y--;
            else if (direction == Direction.Down)
                Y++;
            else if (direction == Direction.Right)
                X++;
            else if (direction == Direction.Left)
                X--;
            return (this, direction);
        }
    }

    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
