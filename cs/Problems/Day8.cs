namespace AOC2023.Problems;

public class Day8 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => CountStepsThroughNodes(input);

    private int CountStepsThroughNodes(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[716];
        var lines = input.Split(lineRanges, Environment.NewLine);

        var nodes = new NodeList(size: lines - 2);

        for (int i = 2; i < lines; i++)
            nodes.AppendNode(input[lineRanges[i]]);

        int steps = 0;
        var instructions = input[lineRanges[0]];

        Node? current = nodes.GetFirst();

        while (current != null)
        {
            int instruction = steps % instructions.Length;
            current = nodes.GetNext(current!.Value, instructions[instruction]);
            steps++;
        }

        return steps;
    }

    readonly ref struct NodeList(int size)
    {
        const string FIRST_NODE = "AAA";
        const string LAST_NODE = "ZZZ";

        private readonly Dictionary<string, Node> store = new(size);

        public void AppendNode(ReadOnlySpan<char> nodeString) => AppendNode(Node.Parse(nodeString));

        public void AppendNode(Node node) => store.Add(node.Value, node);

        public Node GetFirst() => store[FIRST_NODE];

        public Node? GetNext(Node current, char direction)
        {
            var nextValue = direction == 'L' ? current.Left : current.Right;
            var next = store[nextValue];
            return next.Value == LAST_NODE ? null : next;
        }
    }

    readonly record struct Node(string Value, string Left, string Right)
    {
        public static Node Parse(ReadOnlySpan<char> nodeString)
        {
            string value = nodeString[..3].ToString();
            string left = nodeString[7..10].ToString();
            string right = nodeString[12..15].ToString();

            return new(value, left, right);
        }
    }
}
