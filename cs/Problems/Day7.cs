namespace AOC2023.Problems;

public class Day7 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => CalculateTotalCardWinnings(input);

    private static int CalculateTotalCardWinnings(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[1000];

        int lines = input.Split(lineRanges, Environment.NewLine);
        Span<CardDraw> draws = stackalloc CardDraw[lines];

        for (int i = 0; i < lines; i++)
            draws[i] = CardDraw.Parse(input[lineRanges[i]]);

        draws.Sort(CardDraw.Compare);

        int totalWinnings = 0;

        for (int rank = 1; rank <= lines; rank++)
            totalWinnings += draws[rank - 1].Bid * rank;

        return totalWinnings;
    }

    readonly struct CardDraw(long drawValue, int bid)
    {
        const string CARD_TYPES = "23456789TJQKA";

        public long DrawValue => drawValue;

        public int Bid => bid;

        public static CardDraw Parse(ReadOnlySpan<char> cardDrawString)
        {
            int separator = cardDrawString.IndexOf(' ');

            var draw = cardDrawString[..separator];
            int bid = int.Parse(cardDrawString[(separator + 1)..]);
            long drawValue = CalculateDrawValue(draw);

            return new CardDraw(drawValue, bid);
        }

        static long CalculateDrawValue(ReadOnlySpan<char> drawnCards)
        {
            var cardTypeCount = new Dictionary<char, int>();
            long drawValue = 0;
            int maxCount = 0;

            foreach (var card in drawnCards)
            {
                if (!cardTypeCount.TryAdd(card, 1))
                {
                    cardTypeCount[card]++;
                    maxCount = Math.Max(maxCount, cardTypeCount[card]);
                }
                drawValue = (drawValue * 100) + (CARD_TYPES.IndexOf(card) + 10);
            }

            int drawType = cardTypeCount switch
            {
                { Count: 1 } => 7,
                { Count: 2 } when maxCount is 4 => 6,
                { Count: 2 } => 5,
                { Count: 3 } when maxCount is 3 => 4,
                { Count: 3 } => 3,
                { Count: 4 } => 2,
                _ => 1
            };

            return (drawType * 10_000_000_000) + drawValue;
        }

        public static int Compare(CardDraw first, CardDraw second) => first.DrawValue.CompareTo(second.DrawValue);
    }
}
