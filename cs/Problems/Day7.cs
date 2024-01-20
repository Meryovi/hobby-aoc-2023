namespace AOC2023.Problems;

public class Day7 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => CalculateTotalCardWinnings(input);

    private static int CalculateTotalCardWinnings(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[1000];

        int lines = input.Split(lineRanges, InputReader.NewLine);
        Span<CardDraw> draws = stackalloc CardDraw[lines];

        for (int i = 0; i < lines; i++)
            draws[i] = CardDraw.Parse(input[lineRanges[i]]);

        draws.Sort(CardDraw.Compare);

        int totalWinnings = 0;

        for (int rank = 1; rank <= lines; rank++)
            totalWinnings += draws[rank - 1].Bid * rank;

        return totalWinnings;
    }

    readonly record struct CardDraw(long DrawValue, int Bid)
    {
        const string CARD_TYPES = "23456789TJQKA";

        public static CardDraw Parse(ReadOnlySpan<char> cardDrawString)
        {
            int separator = cardDrawString.IndexOf(' ');

            var draw = cardDrawString[..separator];
            int bid = int.Parse(cardDrawString[(separator + 1)..]);
            long drawValue = CalculateDrawValue(draw);

            return new CardDraw(drawValue, bid);
        }

        public static int Compare(CardDraw first, CardDraw second) => first.DrawValue.CompareTo(second.DrawValue);

        static long CalculateDrawValue(ReadOnlySpan<char> drawnCards)
        {
            int typeCount = 0;
            int maxCount = 1;
            int currentCount = 1;
            long drawValue = 0;

            var cards = drawnCards.ToArray();
            Array.Sort(cards);

            for (int i = 0; i < cards.Length; i++)
            {
                if (i > 0 && cards[i] == cards[i - 1])
                {
                    currentCount++;
                }
                else
                {
                    typeCount++;
                    currentCount = 1;
                }

                maxCount = Math.Max(maxCount, currentCount);
                drawValue = (drawValue * 100) + (CARD_TYPES.IndexOf(drawnCards[i]) + 10);
            }

            int drawType = typeCount switch
            {
                1 => 7,
                2 when maxCount is 4 => 6,
                2 => 5,
                3 when maxCount is 3 => 4,
                3 => 3,
                4 => 2,
                _ => 1
            };

            return (drawType * 10_000_000_000) + drawValue;
        }
    }
}
