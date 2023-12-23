namespace AOC2023.Problems;

public class Day19 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => ProcessAllPartRatings(input);

    private int ProcessAllPartRatings(ReadOnlySpan<char> input)
    {
        Span<Range> workflowRanges = stackalloc Range[600];
        Span<Range> partsRanges = stackalloc Range[200];

        int separationInx = input.IndexOf(Environment.NewLine + Environment.NewLine);
        var workflowStrings = input[..separationInx];
        var partStrings = input[(separationInx + 2)..];

        int workflowCount = workflowStrings.Split(workflowRanges, Environment.NewLine);
        int partsCount = partStrings.Split(partsRanges, Environment.NewLine);

        var workflowMap = new SortedList<int, Workflow>
        {
            { Workflow.Accepted.Code, Workflow.Accepted },
            { Workflow.Rejected.Code, Workflow.Rejected }
        };

        for (int i = 0; i < workflowCount; i++)
        {
            var parsed = Workflow.Parse(workflowStrings[workflowRanges[i]]);
            workflowMap.Add(parsed.Code, parsed);
        }

        int totalRating = 0;

        for (int i = 0; i < partsCount; i++)
        {
            var part = Part.Parse(partStrings[partsRanges[i]]);
            var workflow = workflowMap[Workflow.InitialWorkflowCode];

            while (workflow != Workflow.Accepted && workflow != Workflow.Rejected)
            {
                int nextWorkflow = workflow.Process(part);
                workflow = workflowMap[nextWorkflow];

                if (workflow == Workflow.Accepted)
                    totalRating += part.Rating;
            }
        }

        return totalRating;
    }

    readonly record struct Workflow(int Code, WorkflowRule[] Rules)
    {
        public static readonly int InitialWorkflowCode = "in".GetHashCode();

        public static readonly Workflow Accepted = new("A".GetHashCode(), []);

        public static readonly Workflow Rejected = new("R".GetHashCode(), []);

        public static Workflow Parse(ReadOnlySpan<char> workflowString)
        {
            Span<Range> stepsRanges = stackalloc Range[10];
            int stepsInx = workflowString.IndexOf('{');
            var name = workflowString[..stepsInx];

            var stepsString = workflowString[(stepsInx + 1)..^1];
            int stepCount = stepsString.Split(stepsRanges, ',');

            var steps = new WorkflowRule[stepCount];

            for (int i = 0; i < stepCount; i++)
                steps[i] = WorkflowRule.Parse(stepsString[stepsRanges[i]]);

            return new Workflow(string.GetHashCode(name), steps);
        }

        public readonly int Process(Part part)
        {
            for (int i = 0; i < Rules.Length - 1; i++)
                if (Rules[i].Matches(part))
                    return Rules[i].NextWorkflow;

            return Rules[^1].NextWorkflow;
        }
    }

    readonly record struct WorkflowRule(int NextWorkflow, char? Category = null, char? Operand = null, int? Value = null)
    {
        public static WorkflowRule Parse(ReadOnlySpan<char> stepString)
        {
            int separatorInx = stepString.IndexOf(':');

            if (separatorInx == -1)
                return new WorkflowRule(string.GetHashCode(stepString[(separatorInx + 1)..]));

            char category = stepString[0];
            char operand = stepString[1];
            int value = int.Parse(stepString[2..separatorInx]);
            int next = string.GetHashCode(stepString[(separatorInx + 1)..]);

            return new WorkflowRule(next, category, operand, value);
        }

        public readonly bool Matches(Part part)
        {
            if (Category is null || Operand is null || Value is null)
                return true;

            return (Category, Operand) switch
            {
                ('x', '>') => part.X > Value,
                ('m', '>') => part.M > Value,
                ('a', '>') => part.A > Value,
                ('s', '>') => part.S > Value,
                ('x', '<') => part.X < Value,
                ('m', '<') => part.M < Value,
                ('a', '<') => part.A < Value,
                ('s', '<') => part.S < Value,
                _ => throw new InvalidDataException()
            };
        }
    }

    readonly record struct Part(int X, int M, int A, int S)
    {
        public readonly int Rating => X + M + A + S;

        public static Part Parse(ReadOnlySpan<char> partString)
        {
            Span<Range> partRanges = stackalloc Range[4];
            var usablePart = partString[1..^1];
            usablePart.Split(partRanges, ',');

            int x = int.Parse(usablePart[partRanges[0]][2..]);
            int m = int.Parse(usablePart[partRanges[1]][2..]);
            int a = int.Parse(usablePart[partRanges[2]][2..]);
            int s = int.Parse(usablePart[partRanges[3]][2..]);

            return new Part(x, m, a, s);
        }
    }
}
