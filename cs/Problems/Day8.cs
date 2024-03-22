namespace AOC2023.Problems;

public class Day8 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => CountStepsThroughNodes(input);

    private int CountStepsThroughNodes(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[716];
        var lines = input.Split(lineRanges, InputReader.NewLine);

        var nodeList = new NodeList(size: lines - 2);

        for (int i = 2; i < lines; i++)
            nodeList.AppendNode(input[lineRanges[i]]);

        int steps = 0;
        var instructions = input[lineRanges[0]];

        Node? current = nodeList.GetStartingNode();

        while (current is not null)
        {
            int instruction = steps % instructions.Length;
            current = nodeList.GetNextNode(current!.Value, instructions[instruction]);
            steps++;
        }

        return steps;
    }

    readonly ref struct NodeList(int size)
    {
        static readonly int START_NODE = "AAA".GetHashCode();
        static readonly int END_NODE = "ZZZ".GetHashCode();

        private readonly Dictionary<int, Node> store = new(size);

        public void AppendNode(ReadOnlySpan<char> nodeString) => AppendNode(Node.Parse(nodeString));

        public void AppendNode(Node node) => store.Add(node.Value, node);

        public Node GetStartingNode() => store[START_NODE];

        public Node? GetNextNode(Node current, char direction)
        {
            var nextValue = direction == 'L' ? current.Left : current.Right;
            var next = store[nextValue];
            return next.Value == END_NODE ? null : next;
        }
    }

    readonly record struct Node(int Value, int Left, int Right)
    {
        public static Node Parse(ReadOnlySpan<char> nodeString)
        {
            int value = string.GetHashCode(nodeString[..3]);
            int left = string.GetHashCode(nodeString[7..10]);
            int right = string.GetHashCode(nodeString[12..15]);

            return new(value, left, right);
        }
    }
}
