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

    public static T[] ParseNumbers<T>(ReadOnlySpan<char> numbersList, int maxSize, string separator = " ")
        where T : INumber<T>
    {
        var numbers = new T[maxSize];
        Span<Range> ranges = stackalloc Range[maxSize];

        int splitSize = numbersList.Split(ranges, separator);
        int actualSize = 0;
        for (int i = 0; i < splitSize; i++)
        {
            var value = numbersList[ranges[i]];

            if (!value.IsEmpty)
            {
                numbers[actualSize] = T.Parse(value, null);
                actualSize++;
            }
        }

        Array.Resize(ref numbers, actualSize);

        return numbers;
    }
}
