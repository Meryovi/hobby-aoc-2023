namespace AOC2023;

public static class InputParser
{
    public static List<int> ParseNumbers(ReadOnlySpan<char> numbersList, int maxSize, char separator = ' ')
    {
        var numbers = new List<int>(maxSize);
        Span<Range> ranges = stackalloc Range[maxSize];

        int actualSize = numbersList.Split(ranges, separator);
        for (int i = 0; i < actualSize; i++)
        {
            var value = numbersList[ranges[i]];

            if (!value.IsEmpty)
                numbers.Add(int.Parse(value));
        }

        return numbers;
    }
}
