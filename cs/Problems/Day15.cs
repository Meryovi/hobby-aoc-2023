namespace AOC2023.Problems;

public class Day15 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => SumOfHashValues(input);

    private static int SumOfHashValues(ReadOnlySpan<char> input)
    {
        int sumOfHashes = 0;

        int hashValue = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == ',')
            {
                sumOfHashes += hashValue;
                hashValue = 0;
                continue;
            }

            hashValue += input[i];
            hashValue = hashValue * 17 % 256;
        }
        sumOfHashes += hashValue;

        return sumOfHashes;
    }
}
