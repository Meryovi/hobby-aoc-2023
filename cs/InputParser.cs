using System.Numerics;

namespace AOC2023;

public static class InputParser
{
    public static int ParseNumbers<T>(Span<T> numbers, ReadOnlySpan<char> input, string separator = " ")
        where T : INumber<T>
    {
        Span<Range> ranges = stackalloc Range[numbers.Length];

        int splitSize = input.Split(ranges, separator);
        int actualSize = 0;

        for (int i = 0; i < splitSize; i++)
        {
            var value = input[ranges[i]];

            if (!value.IsEmpty)
            {
                numbers[actualSize] = T.Parse(value, null);
                actualSize++;
            }
        }

        return actualSize;
    }

    public static ParsedNumbers ParseNumbers(ref Span<int> numbers, ReadOnlySpan<char> input, int maxSize, string separator = " ")
    {
        Span<Range> ranges = stackalloc Range[numbers.Length];

        int splitSize = input.Split(ranges, separator);
        int actualSize = 0;

        for (int i = 0; i < splitSize; i++)
        {
            var value = input[ranges[i]];

            if (!value.IsEmpty)
            {
                numbers[actualSize] = int.Parse(value, null);
                actualSize++;
            }
        }

        return new ParsedNumbers(ref numbers, actualSize);
    }
}

public readonly ref struct ParsedNumbers(ref Span<int> numbers, int actualLength)
{
    public ReadOnlySpan<int> Numbers { get; } = numbers;

    public int Length { get; } = actualLength;
}
