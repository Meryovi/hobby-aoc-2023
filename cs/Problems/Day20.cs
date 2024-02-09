namespace AOC2023.Problems;

public class Day20 : IProblem<long>
{
    public long Solve(ReadOnlySpan<char> input) => CountNumberOfPulsesSent(input);

    private static long CountNumberOfPulsesSent(ReadOnlySpan<char> input)
    {
        Span<Range> lineRanges = stackalloc Range[58];
        int lines = input.Split(lineRanges, InputReader.NewLine);

        var pulseSender = new PulseSender();

        for (int i = 0; i < lines; i++)
            pulseSender.RegisterDestination(IModule.Parse(input[lineRanges[i]]));

        pulseSender.FeedModulesDependencies();

        for (int i = 0; i < 1000; i++)
            pulseSender.FollowPulse(Button.ModuleName, Button.DestinationName, Button.StartingPulse);

        int pulseValue = pulseSender.HighPulseCount * pulseSender.LowPulseCount;
        return pulseValue;
    }

    ref struct PulseSender()
    {
        public int HighPulseCount { get; private set; }

        public int LowPulseCount { get; private set; }

        readonly Queue<(int Source, int Destination, Pulse Pulse)> pulseQueue = new();

        readonly SortedList<int, IModule> destinations = [];

        public readonly void RegisterDestination(IModule module) => destinations.Add(module.Name, module);

        public void FollowPulse(int source, int destination, Pulse pulse)
        {
            SendPulse(source, destination, pulse);

            while (pulseQueue.TryDequeue(out var message))
            {
                if (destinations.TryGetValue(message.Destination, out var module))
                    module.ReceivePulse(message.Source, message.Pulse, ref this);
            }
        }

        public void SendPulse(int source, int destination, Pulse pulse)
        {
            if (pulse == Pulse.High)
                HighPulseCount++;
            else
                LowPulseCount++;

            pulseQueue.Enqueue((source, destination, pulse));
        }

        public readonly void FeedModulesDependencies()
        {
            foreach (var (_, destination) in destinations)
            {
                if (destination is not Conjunction conjunction)
                    continue;

                foreach (var (key, current) in destinations)
                {
                    if (current.Destinations.Contains(conjunction.Name))
                        conjunction.AddDependency(key);
                }
            }
        }
    }

    interface IModule
    {
        int Name { get; }

        int[] Destinations { get; }

        void ReceivePulse(int source, Pulse pulse, ref PulseSender sender);

        static IModule Parse(ReadOnlySpan<char> moduleString)
        {
            int splitInx = moduleString.IndexOf(" -> ");
            var moduleType = moduleString[0];

            Span<Range> destinationRanges = stackalloc Range[7];
            var destinationString = moduleString[(splitInx + 4)..];
            int destinationCount = destinationString.Split(destinationRanges, ", ");

            var destinations = new int[destinationCount];
            for (int i = 0; i < destinationCount; i++)
                destinations[i] = string.GetHashCode(destinationString[destinationRanges[i]]);

            return Create(moduleType, string.GetHashCode(moduleString[1..splitInx]), destinations);
        }

        static IModule Create(char moduleType, int name, int[] destinations) =>
            moduleType switch
            {
                'b' => new Broadcaster(destinations),
                '%' => new FlipFlop(name, destinations),
                '&' => new Conjunction(name, destinations),
                _ => throw new NotImplementedException(),
            };
    }

    class Broadcaster(int[] destinations) : IModule
    {
        public static readonly int ModuleName = "broadcaster".GetHashCode();

        public int Name => ModuleName;

        public int[] Destinations => destinations;

        public void ReceivePulse(int source, Pulse pulse, ref PulseSender sender)
        {
            foreach (var destination in Destinations)
                sender.SendPulse(Name, destination, pulse);
        }
    }

    class FlipFlop(int name, int[] destinations) : IModule
    {
        public int Name => name;

        public int[] Destinations => destinations;

        private bool turnedOn = false;

        public void ReceivePulse(int source, Pulse pulse, ref PulseSender sender)
        {
            if (pulse == Pulse.High)
                return;

            turnedOn = !turnedOn;
            var newPulse = turnedOn ? Pulse.High : Pulse.Low;

            foreach (var destination in Destinations)
                sender.SendPulse(Name, destination, newPulse);
        }
    }

    class Conjunction(int name, int[] destinations) : IModule
    {
        public int Name => name;

        public int[] Destinations => destinations;

        private readonly Dictionary<int, Pulse> memory = [];

        public void ReceivePulse(int source, Pulse pulse, ref PulseSender sender)
        {
            memory[source] = pulse;
            var newPulse = GetNextPulse();

            foreach (var destination in Destinations)
                sender.SendPulse(Name, destination, newPulse);
        }

        public void AddDependency(int source) => memory.TryAdd(source, Pulse.Low);

        Pulse GetNextPulse()
        {
            foreach (var (_, lastPulse) in memory)
                if (lastPulse == Pulse.Low)
                    return Pulse.High;
            return Pulse.Low;
        }
    }

    static class Button
    {
        public static readonly int ModuleName = "button".GetHashCode();

        public static readonly int DestinationName = Broadcaster.ModuleName;

        public static readonly Pulse StartingPulse = Pulse.Low;
    }

    enum Pulse
    {
        Low,
        High,
    }
}
