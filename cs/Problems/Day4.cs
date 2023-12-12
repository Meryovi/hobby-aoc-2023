namespace AOC2023.Problems;

public class Day4 : IProblem
{
    public string Name => "Day 4";

    public void Solve()
    {
        string input = InputReader.ReadDayInput(day: 4);
        int scratchCardPoints = CalculateScatchCardPoints(input);

        Console.WriteLine(scratchCardPoints);
    }

    private static int CalculateScatchCardPoints(string input)
    {
        int cardPoints = 0;

        foreach (var card in input.Split(Environment.NewLine))
        {
            var numbers = card[(card.IndexOf(": ") + 2)..].Split('|');
            var winning = numbers[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToHashSet();
            var played = numbers[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            int playPoints = 0;

            foreach (var play in played)
                playPoints = winning.Contains(play) ? Math.Max(1, playPoints * 2) : playPoints;

            cardPoints += playPoints;
        }

        return cardPoints;
    }
}
