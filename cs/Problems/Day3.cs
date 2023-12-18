namespace AOC2023.Problems;

public class Day3 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => ExtractPartNumber(input);

    private static int ExtractPartNumber(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[140];
        var count = input.Split(lineRanges, Environment.NewLine);

        var matrix = new char[count, count];

        for (int i = 0; i < count; i++)
        for (int j = 0; j < count; j++)
            matrix[i, j] = input[lineRanges[i]][j];

        int partNumber = 0;

        for (int i = 0; i < count; i++)
        {
            bool adjacentSymbol = false;
            int accumulator = 0;

            for (int j = 0; j < count; j++)
            {
                if (char.IsDigit(matrix[i, j]))
                {
                    accumulator = accumulator * 10 + (matrix[i, j] - '0');
                    adjacentSymbol |= !adjacentSymbol && HasAdjacentSymbol(matrix, i, j, count);
                }
                else if (accumulator > 0)
                {
                    partNumber += adjacentSymbol ? accumulator : 0;
                    accumulator = 0;
                    adjacentSymbol = false;
                }
            }

            partNumber += adjacentSymbol ? accumulator : 0;
        }

        return partNumber;
    }

    private static bool HasAdjacentSymbol(char[,] lines, int row, int col, int size)
    {
        for (int x = Math.Max(0, row - 1); x <= Math.Min(size - 1, row + 1); x++)
        {
            for (int y = Math.Max(0, col - 1); y <= Math.Min(size - 1, col + 1); y++)
            {
                if ((x != row || y != col) && lines[x, y] != '.' && !char.IsDigit(lines[x, y]))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
