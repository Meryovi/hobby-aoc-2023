namespace AOC2023.Problems;

public class Day2 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => NumberOfPossibleGames(input);

    private static int NumberOfPossibleGames(ReadOnlySpan<char> games)
    {
        Span<Range> gameLines = stackalloc Range[99];
        Span<Range> drawRanges = stackalloc Range[18];
        Span<char> cubeSeparators = [',', ';'];

        int possibleGames = 0;
        int currentGame = 1;

        games.Split(gameLines, Environment.NewLine);
        foreach (var gameRange in gameLines)
        {
            var game = games[gameRange];
            game = game[(game.IndexOf(": ") + 2)..];

            int draws = game.SplitAny(drawRanges, cubeSeparators);
            bool possible = true;

            for (int draw = 0; draw < draws && possible; draw++)
            {
                int separatorInx = game[drawRanges[draw]].LastIndexOf(' ');
                var cube = game[drawRanges[draw]][(separatorInx + 1)..];
                int count = int.Parse(game[drawRanges[draw]][..separatorInx]);

                if (cube.Equals("red", StringComparison.Ordinal) && count > 12)
                    possible = false;
                else if (cube.Equals("green", StringComparison.Ordinal) && count > 13)
                    possible = false;
                else if (cube.Equals("blue", StringComparison.Ordinal) && count > 14)
                    possible = false;
            }

            possibleGames += possible ? currentGame : 0;
            currentGame++;
            drawRanges.Clear();
        }

        return possibleGames;
    }
}
