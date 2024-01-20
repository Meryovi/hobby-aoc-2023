namespace AOC2023.Problems;

public class Day24 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => CountHailIntersections(input);

    private static int CountHailIntersections(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[301];
        int lines = input.Split(lineRanges, InputReader.NewLine);

        var firstLine = input[lineRanges[0]];
        long minTime = long.Parse(firstLine[..firstLine.IndexOf(' ')]);
        long maxTime = long.Parse(firstLine[(firstLine.IndexOf(' ') + 1)..]);

        Span<Hailstone> hailStones = stackalloc Hailstone[lines];

        for (int i = 1; i < lines; i++)
            hailStones[i] = Hailstone.Parse(i, input[lineRanges[i]]);

        int intersects = 0;

        for (int i = 1; i < lines - 1; i++)
        for (int j = i + 1; j < lines; j++)
            intersects += hailStones[i].Intersects(hailStones[j], minTime, maxTime) ? 1 : 0;

        return intersects;
    }

    readonly record struct Hailstone(int Id, Point Position, Velocity Velocity)
    {
        public readonly bool Intersects(Hailstone other, long minTime, long maxTime)
        {
            // Intersection of dimensional lines formula, found it somewhere else.
            decimal determinant = Velocity.VX * other.Velocity.VY - Velocity.VY * other.Velocity.VX;

            if (determinant == 0)
                return false;

            decimal t1 = Velocity.VX * Position.Y - Velocity.VY * Position.X;
            decimal t2 = other.Velocity.VX * other.Position.Y - other.Velocity.VY * other.Position.X;

            decimal xIntersect = (other.Velocity.VX * t1 - Velocity.VX * t2) / determinant;
            decimal yIntersect = (other.Velocity.VY * t1 - Velocity.VY * t2) / determinant;

            if (xIntersect < minTime || xIntersect > maxTime || yIntersect < minTime || yIntersect > maxTime)
                return false;

            bool thisIntersect = Math.Sign(xIntersect - Position.X) != Math.Sign(Velocity.VX);
            bool otherIntersect = Math.Sign(xIntersect - other.Position.X) != Math.Sign(other.Velocity.VX);

            if (thisIntersect || otherIntersect)
                return false;

            return true;
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
