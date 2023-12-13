using AOC2023.Problems;

var problem = ParseProblem(args);

Console.WriteLine($"Running problem '{problem.Name}', answer:");
problem.Solve();

static IProblem ParseProblem(string[] args)
{
    var problems = new Dictionary<int, Func<IProblem>>()
    {
        { 1, () => new Day1() },
        { 2, () => new Day2() },
        { 3, () => new Day3() },
        { 4, () => new Day4() },
    };

    if (int.TryParse(args.Length > 1 ? args[1] : "1", out int option) && !problems.ContainsKey(option))
        throw new ArgumentException("Could not parse problem number, provide --prob arg with valid value");

    return problems[option]();
}
