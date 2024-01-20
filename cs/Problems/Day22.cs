namespace AOC2023.Problems;

public class Day22 : IProblem<int>
{
    public int Solve(ReadOnlySpan<char> input) => CountSafelyDisintegratedBlocks(input);

    private int CountSafelyDisintegratedBlocks(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[1500];
        int lines = input.Split(lineRanges, InputReader.NewLine);

        Span<Block> blocks = stackalloc Block[lines];

        for (int i = 0; i < lines; i++)
            blocks[i] = Block.Parse(input[lineRanges[i]]);

        Block.ApplyGravity(blocks);

        int safelyDisintegrated = 0;

        var blockQueue = new Queue<Block>(lines / 2);
        var falling = new HashSet<Block>(lines);
        var supportChain = SupportChain.BuildSupportChain(blocks);

        foreach (var disintegratingBlock in blocks)
        {
            blockQueue.Enqueue(disintegratingBlock);

            while (blockQueue.TryDequeue(out var block))
            {
                falling.Add(block);

                foreach (var above in supportChain.BlocksAbove[block])
                    if (supportChain.BlocksBelow[above].IsSubsetOf(falling))
                        blockQueue.Enqueue(above);
            }

            safelyDisintegrated += falling.Count - 1 == 0 ? 1 : 0;

            blockQueue.Clear();
            falling.Clear();
        }

        return safelyDisintegrated;
    }

    readonly record struct Block(LinearRange X, LinearRange Y, LinearRange Z)
    {
        public int Top => Z.End;

        public int Bottom => Z.Start;

        public static Block Parse(ReadOnlySpan<char> blockString)
        {
            Span<Range> numberRanges = stackalloc Range[6];
            blockString.SplitAny(numberRanges, [',', '~']);

            Span<int> numbers = stackalloc int[6];
            for (int i = 0; i < 6; i++)
                numbers[i] = int.Parse(blockString[numberRanges[i]]);

            return new Block(
                X: new LinearRange(numbers[0], numbers[3]),
                Y: new LinearRange(numbers[1], numbers[4]),
                Z: new LinearRange(numbers[2], numbers[5])
            );
        }

        public static void ApplyGravity(Span<Block> blocks)
        {
            MemoryExtensions.Sort(blocks, OrderByPosition);

            for (var i = 0; i < blocks.Length; i++)
            {
                int distance = 0;

                for (var j = 0; j < i; j++)
                    if (blocks[i].Intersects(blocks[j]))
                        distance = Math.Max(distance, blocks[j].Top);

                int fall = blocks[i].Bottom - distance - 1;
                blocks[i] = blocks[i] with { Z = blocks[i].Z.Reduce(fall) };
            }
        }

        public bool Intersects(Block other) => X.Intersects(other.X) && Y.Intersects(other.Y);

        static int OrderByPosition(Block a, Block b) => a.Bottom - b.Bottom;
    }

    readonly record struct LinearRange(int Start, int End)
    {
        public bool Intersects(LinearRange other) => Start <= other.End && other.Start <= End;

        public LinearRange Reduce(int amount) => new(Start - amount, End - amount);
    }

    readonly record struct SupportChain(Dictionary<Block, List<Block>> BlocksAbove, Dictionary<Block, HashSet<Block>> BlocksBelow)
    {
        public static SupportChain BuildSupportChain(ReadOnlySpan<Block> blocks)
        {
            var blocksAbove = new Dictionary<Block, List<Block>>(blocks.Length);
            var blocksBelow = new Dictionary<Block, HashSet<Block>>(blocks.Length);

            foreach (var block in blocks)
            {
                blocksAbove.Add(block, []);
                blocksBelow.Add(block, []);
            }

            for (var i = 0; i < blocks.Length; i++)
            {
                for (var j = i + 1; j < blocks.Length; j++)
                {
                    var zNeighbors = blocks[j].Bottom == 1 + blocks[i].Top;
                    if (zNeighbors && blocks[i].Intersects(blocks[j]))
                    {
                        blocksBelow[blocks[j]].Add(blocks[i]);
                        blocksAbove[blocks[i]].Add(blocks[j]);
                    }
                }
            }

            return new SupportChain(blocksAbove, blocksBelow);
        }
    }
}
