namespace AOC2023.Problems;

public class Day24(ITestOutputHelper? output = null) : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => CountHailIntersections(input);

    private int CountHailIntersections(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[300];
        int lines = input.Split(lineRanges, Environment.NewLine);

        var hailStones = new Hailstone[lines];

        for (int i = 0; i < lines; i++)
            hailStones[i] = Hailstone.Parse(i, input[lineRanges[i]]);

        int intersects = 0;

        for (int i = 0; i < lines - 1; i++)
        for (int j = i + 1; j < lines; j++)
            intersects += hailStones[i].Intersects(hailStones[j], 200000000000000, 400000000000000.99, output) ? 1 : 0;

        return intersects;
    }

    readonly record struct Hailstone(int Id, Point Location, Velocity Velocity)
    {
        public readonly bool Intersects(Hailstone other, double minTime, double maxTime, ITestOutputHelper? output)
        {
            var c1 = Velocity.VY * Location.X - Velocity.VX * Location.Y;
            var c2 = other.Velocity.VY * other.Location.X - other.Velocity.VX * other.Location.Y;

            if (Velocity.VY * -other.Velocity.VX == -Velocity.VX * other.Velocity.VY)
                return false;

            var x =
                (c1 * -other.Velocity.VX - c2 * -Velocity.VX)
                / (double)(Velocity.VY * -other.Velocity.VX - other.Velocity.VY * -Velocity.VX);

            var y =
                (c2 * Velocity.VY - c1 * other.Velocity.VY)
                / (double)(Velocity.VY * -other.Velocity.VX - other.Velocity.VY * -Velocity.VX);

            if (minTime <= x && x <= maxTime && minTime <= y && y <= maxTime)
            {
                if (
                    (x - Location.X) * Velocity.VX >= 0
                    && (y - Location.Y) * Velocity.VY >= 0
                    && (x - other.Location.X) * other.Velocity.VX >= 0
                    && (y - other.Location.Y) * other.Velocity.VY >= 0
                )
                {
                    // output?.WriteLine($"{Id} v {other.Id} - x {x}, y {y}");
                    return true;
                }
            }

            // Returns the intersection point, as well as the timestamp at which "one" will reach it with the given velocity.
            return false;
        }

        public static Hailstone Parse(int id, ReadOnlySpan<char> hailString)
        {
            int separatorInx = hailString.IndexOf(" @ ");
            var positions = hailString[..separatorInx];
            var velocities = hailString[(separatorInx + 3)..];

            Span<long> position = stackalloc long[3];
            Span<int> velocity = stackalloc int[3];

            InputParser.ParseNumbers(ref position, positions, 3, ", ");
            InputParser.ParseNumbers(ref velocity, velocities, 3, ", ");

            return new Hailstone(id, new(position[0], position[1]), new(velocity[0], velocity[1]));
        }
    }

    readonly record struct Point(long X, long Y);

    readonly record struct Velocity(int VX, int VY);
}
