namespace AOC2023.Problems;

public class Day3 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => ExtractPartNumber(input);

    private static int ExtractPartNumber(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[140];
        int count = input.Split(lineRanges, InputReader.NewLine);

        var matrix = new char[count, count];

        for (int j = 0; j < count; j++)
        for (int i = 0; i < count; i++)
            matrix[j, i] = input[lineRanges[j]][i];

        int partNumber = 0;

        for (int j = 0; j < count; j++)
        {
            bool adjacentSymbol = false;
            int accumulator = 0;

            for (int i = 0; i < count; i++)
            {
                if (char.IsDigit(matrix[j, i]))
                {
                    accumulator = accumulator * 10 + (matrix[j, i] - '0');
                    if (!adjacentSymbol)
                        adjacentSymbol = HasAdjacentSymbol(matrix, j, i, count);
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

    private static bool HasAdjacentSymbol(char[,] lines, int col, int row, int size)
    {
        for (int y = Math.Max(0, col - 1); y <= Math.Min(size - 1, col + 1); y++)
        for (int x = Math.Max(0, row - 1); x <= Math.Min(size - 1, row + 1); x++)
            if ((x != row || y != col) && lines[y, x] != '.' && !char.IsDigit(lines[y, x]))
                return true;

        return false;
    }
}
