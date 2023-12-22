namespace AOC2023.Problems;

public class Day16 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => CountEnergizedLightTiles(input);

    private int CountEnergizedLightTiles(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[110];
        int height = input.Split(lineRanges, Environment.NewLine);
        int width = input[lineRanges[0]].Length;

        var matrix = new char[height, width];
        var history = new Direction[height, width];

        for (int j = 0; j < height; j++)
        for (int i = 0; i < width; i++)
            matrix[j, i] = input[lineRanges[j]][i];

        var beam = new FacingPoint(0, 0, Direction.Right);
        StartLightBeam(beam, matrix, history);

        int tileCount = 0;

        for (int j = 0; j < height; j++)
        for (int i = 0; i < width; i++)
            tileCount += history[j, i] > 0 ? 1 : 0;

        return tileCount;
    }

    private static bool StartLightBeam(FacingPoint beam, char[,] matrix, Direction[,] history)
    {
        int width = matrix.GetLength(1);
        int height = matrix.GetLength(0);

        while (!beam.IsOffset(width, height))
        {
            var instruction = matrix[beam.Y, beam.X];
            var visits = history[beam.Y, beam.X];

            if (visits.HasFlag(beam.Direction))
                break;

            history[beam.Y, beam.X] = visits | beam.Direction;

            _ = (instruction, beam.Direction) switch
            {
                ('/', Direction.Right) => beam.Move(Direction.Up),
                ('/', Direction.Left) => beam.Move(Direction.Down),
                ('/', Direction.Up) => beam.Move(Direction.Right),
                ('/', Direction.Down) => beam.Move(Direction.Left),
                ('\\', Direction.Right) => beam.Move(Direction.Down),
                ('\\', Direction.Left) => beam.Move(Direction.Up),
                ('\\', Direction.Up) => beam.Move(Direction.Left),
                ('\\', Direction.Down) => beam.Move(Direction.Right),
                ('|', Direction.Right) => StartLightBeam(beam.Split(Direction.Up), matrix, history),
                ('|', Direction.Left) => StartLightBeam(beam.Split(Direction.Up), matrix, history),
                ('-', Direction.Up) => StartLightBeam(beam.Split(Direction.Right), matrix, history),
                ('-', Direction.Down) => StartLightBeam(beam.Split(Direction.Right), matrix, history),
                _ => beam.Move(beam.Direction)
            };
        }

        return true;
    }

    record struct FacingPoint(int X, int Y, Direction Direction)
    {
        public bool Move(Direction direction)
        {
            if (direction == Direction.Up)
                Y--;
            else if (direction == Direction.Down)
                Y++;
            else if (direction == Direction.Right)
                X++;
            else if (direction == Direction.Left)
                X--;

            Direction = direction;
            return true;
        }

        public FacingPoint Split(Direction direction)
        {
            var split = this with { };
            split.Move(direction);

            var oppositeDirection = direction switch
            {
                Direction.Right => Direction.Left,
                Direction.Left => Direction.Right,
                Direction.Up => Direction.Down,
                Direction.Down => Direction.Up,
                _ => direction
            };
            Move(oppositeDirection);

            return split;
        }

        public readonly bool IsOffset(int width, int height) => X < 0 || Y < 0 || X >= width || Y >= height;
    }

    [Flags]
    enum Direction : byte
    {
        Right = 1,
        Left = 2,
        Up = 4,
        Down = 8
    };
}
