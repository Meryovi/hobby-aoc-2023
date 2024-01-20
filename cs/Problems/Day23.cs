namespace AOC2023.Problems;

public class Day23 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => FindLongestHikeTrail(input);

    private static int FindLongestHikeTrail(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[150];
        int height = input.Split(lineRanges, InputReader.NewLine);
        int width = input[lineRanges[0]].Length;

        var grid = new char[height, width];
        var start = new Point(0, 0);
        var end = new Point(0, 0);

        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                grid[j, i] = input[lineRanges[j]][i];
                start = j == 0 && grid[j, i] == '.' ? new(j, i) : start;
                end = j == height - 1 && grid[j, i] == '.' ? new(j, i) : end;
            }
        }

        var mapGraph = new MapGraph(start, end);
        mapGraph.BuildGraph(grid);

        int longestTrail = mapGraph.GetLongestTrail(start);
        return longestTrail;
    }

    readonly record struct MapGraph(Point Start, Point End)
    {
        private readonly Dictionary<Point, List<Edge>> graph = [];

        private readonly HashSet<Point> visited = [];

        public void BuildGraph(char[,] grid)
        {
            int maxY = grid.GetLength(0);
            int maxX = grid.GetLength(1);

            var nodes = FindAllNodes(grid, maxY, maxX);
            var queue = new Queue<State>();
            var next = new List<Point>();

            graph.Clear();
            visited.Clear();
            foreach (var node in nodes)
            {
                queue.Clear();
                queue.Enqueue(new State(node, node, 0));

                while (queue.Count > 0)
                {
                    var state = queue.Dequeue();

                    if (state.Current != node && nodes.Contains(state.Current))
                    {
                        if (!graph.ContainsKey(node))
                            graph.Add(node, []);
                        graph[node].Add(new Edge(state.Current, state.Steps));
                        continue;
                    }

                    next.Clear();
                    foreach (var direction in Point.AllDirections)
                    {
                        var current = state.Current + direction;

                        if (current == state.Previous || current.Y < 0 || current.Y >= maxY || current.X < 0 || current.X >= maxX)
                            continue;

                        if (grid[current.Y, current.X] == '#')
                            continue;

                        if (grid[current.Y, current.X] == '.')
                            next.Add(current);
                        else if (grid[current.Y, current.X] == 'v' && current.Y - state.Current.Y > 0)
                            next.Add(current);
                        else if (grid[current.Y, current.X] == '>' && current.X - state.Current.X > 0)
                            next.Add(current);
                    }

                    foreach (var following in next)
                        queue.Enqueue(new State(following, state.Current, state.Steps + 1));
                }
            }
        }

        public int GetLongestTrail(Point current, int length = 0)
        {
            if (current == End)
                return length;

            int max = int.MinValue;

            foreach (var edge in graph[current])
            {
                if (!visited.Add(edge.To))
                    continue;

                max = Math.Max(GetLongestTrail(current = edge.To, length + edge.Length), max);
                visited.Remove(edge.To);
            }

            return max;
        }

        List<Point> FindAllNodes(char[,] grid, int maxY, int maxX)
        {
            var nodes = new List<Point> { Start, End };
            for (int j = 0; j < maxY; j++)
            {
                for (int i = 0; i < maxX; i++)
                {
                    if (grid[j, i] == '#')
                        continue;

                    if (CountNeighbors(grid, maxY, maxX, j, i) >= 3)
                        nodes.Add(new(j, i));
                }
            }
            return nodes;
        }

        static int CountNeighbors(char[,] grid, int maxY, int maxX, int y, int x)
        {
            int count = 0;
            var point = new Point(y, x);

            foreach (var direction in Point.AllDirections)
            {
                var tp = point + direction;
                if (tp.Y >= 0 && tp.X >= 0 && tp.Y < maxY && tp.X < maxX && grid[tp.Y, tp.X] != '#')
                    count++;
            }

            return count;
        }
    }

    readonly record struct Edge(Point To, int Length);

    readonly record struct State(Point Current, Point Previous, int Steps);

    readonly record struct Point(int Y, int X)
    {
        public static readonly Point[] AllDirections = [new(-1, 0), new(0, -1), new(1, 0), new(0, 1)];

        public static Point operator +(Point a, Point b) => new(a.Y + b.Y, a.X + b.X);
    }
}
