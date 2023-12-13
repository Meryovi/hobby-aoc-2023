namespace AOC2023.Problems;

public class Day3 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => ExtractPartNumber(input.ToString());

    private static int ExtractPartNumber(string input)
    {
        int partNumber = 0;
        var lines = input.Split(Environment.NewLine);

        for (int i = 0; i < lines.Length; i++)
        {
            bool adjacentSymbol = false;
            int accumulator = 0;

            for (int j = 0; j < lines[i].Length; j++)
            {
                if (char.IsDigit(lines[i][j]))
                {
                    accumulator = accumulator * 10 + (lines[i][j] - '0');
                    adjacentSymbol |= !adjacentSymbol && HasAdjacentSymbol(lines, i, j);
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

    private static bool HasAdjacentSymbol(string[] lines, int row, int col)
    {
        for (int x = Math.Max(0, row - 1); x <= Math.Min(lines.Length - 1, row + 1); x++)
        {
            for (int y = Math.Max(0, col - 1); y <= Math.Min(lines[x].Length - 1, col + 1); y++)
            {
                if ((x != row || y != col) && lines[x][y] != '.' && !char.IsDigit(lines[x][y]))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
