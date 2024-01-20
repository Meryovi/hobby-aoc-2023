namespace AOC2023.Problems;

public class Day18 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => CalculateLavaCubicMeters(input);

    private int CalculateLavaCubicMeters(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[664];
        Span<Range> instructionRanges = stackalloc Range[3];
        int lines = input.Split(lineRanges, InputReader.NewLine);

        int totalCubes = 0;
        var digger = new Point(0, 0);

        for (int i = 0; i < lines; i++)
        {
            var line = input[lineRanges[i]];
            line.Split(instructionRanges, ' ');

            char direction = line[instructionRanges[0]][0];
            int steps = int.Parse(line[instructionRanges[1]]);

            var end = digger.Move(direction, steps);
            totalCubes += digger.X * end.Y - digger.Y * end.X + steps; // Gauss
            digger = end;
        }

        totalCubes = totalCubes / 2 + 1;
        return totalCubes;
    }

    readonly record struct Point(int X, int Y)
    {
        public Point Move(char direction, int steps) =>
            direction switch
            {
                'U' => new(X, Y - steps),
                'D' => new(X, Y + steps),
                'L' => new(X - steps, Y),
                'R' => new(X + steps, Y),
                _ => this
            };
    }
}
