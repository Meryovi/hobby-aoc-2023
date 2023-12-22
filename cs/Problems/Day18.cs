namespace AOC2023.Problems;

public class Day18(ITestOutputHelper? output) : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => CalculateLavaCubicMeters(input);

    private int CalculateLavaCubicMeters(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[10000];
        Span<Range> instructionRanges = stackalloc Range[3];
        int lines = input.Split(lineRanges, Environment.NewLine);

        var matrix = new bool[500, 500];
        var point = new Point(0, 0);

        int maxX = 0;
        int maxY = 0;

        try
        {
            for (int i = 0; i < lines; i++)
            {
                var line = input[lineRanges[i]];
                int ins = line.Split(instructionRanges, ' ');

                char direction = line[instructionRanges[0]][0];
                int steps = int.Parse(line[instructionRanges[1]]);

                try
                {
                    matrix[point.Y, point.X] = true;
                }
                catch (System.Exception)
                {
                    output?.WriteLine("inx: " + point.ToString());
                    throw;
                }
                for (int s = 0; s < steps; s++)
                {
                    point.Move(direction, 1);
                    // matrix[point.Y, point.X] = true;
                }

                if (direction == 'R')
                    maxX = Math.Max(maxX, point.X + 1);

                if (direction == 'D')
                    maxY = Math.Max(maxY, point.Y + 1);
            }

            output?.WriteLine($"mx: {maxX}{Environment.NewLine}my: {maxY}");
        }
        catch (System.Exception e)
        {
            output?.WriteLine("issue was here... " + e.Message);
            throw;
        }

        int totalCubes = 0;

        for (int j = 0; j < maxY; j++)
        {
            int holeStart = -1;
            string result = "";
            for (int i = 0; i < maxX; i++)
            {
                result += matrix[j, i] ? '#' : '.';
                if (matrix[j, i])
                {
                    totalCubes++;

                    if (holeStart > -1 && !matrix[j, i - 1])
                        totalCubes += i - holeStart - 1;

                    holeStart = i;
                }
            }
            output?.WriteLine(result);
            output?.WriteLine("count: " + totalCubes);
        }

        output?.WriteLine("total: " + totalCubes);
        return totalCubes;
    }

    record struct Point(int X, int Y)
    {
        public void Move(char direction, int steps)
        {
            if (direction == 'U' && Y >= steps)
                Y -= steps;
            else if (direction == 'D')
                Y += steps;
            else if (direction == 'L' && X >= steps)
                X -= steps;
            else if (direction == 'R')
                X += steps;
        }
    }
}
