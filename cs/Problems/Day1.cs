namespace AOC2023.Problems;

public class Day1 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => SumOfFirstLastDigits(input.ToString());

    private static int SumOfFirstLastDigits(string input)
    {
        int sum = 0;
        var lines = input.Split(Environment.NewLine);

        for (int x = 0; x < lines.Length; x++)
        {
            (int d1, int d2) = (0, 0);

            for (int i = 0, j = lines[x].Length - 1; j >= 0 && (d1 == 0 || d2 == 0); i++, j--)
            {
                if (d1 == 0 && int.TryParse(lines[x][i..(i + 1)], out int v1))
                    d1 = v1;

                if (d2 == 0 && int.TryParse(lines[x][j..(j + 1)], out int v2))
                    d2 = v2;
            }

            sum += d1 * 10 + d2;
        }

        return sum;
    }
}
