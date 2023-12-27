namespace AOC2023.Problems;

public class Day20 : IProblem<long>
{
    public long Solve(ReadOnlySpan<char> input) => CountNumberOfPulsesSent(input);

    private long CountNumberOfPulsesSent(ReadOnlySpan<char> input)
    {
        // | 'Day20 problem' | 257.3 us | 33.22 us | 1.82 us | 255.9 us | 259.3 us | 9.7656 |  79.89 KB |
        // | 'Day20 problem' | 136.2 us | 5.44 us | 0.30 us | 135.9 us | 136.4 us |   1.13 KB |
        Span<Range> lineRanges = stackalloc Range[60];
        int lines = input.Split(lineRanges, Environment.NewLine);

        var pulseSender = new PulseSender();

        for (int i = 0; i < lines; i++)
            pulseSender.AddDestination(IModule.Parse(input[lineRanges[i]]));

        pulseSender.BuildDependencyTree();

        for (int i = 0; i < 1000; i++)
            pulseSender.FollowPulse(Button.ModuleName, Button.DestinationName, Button.StartingPulse);

        int pulseValue = pulseSender.HighPulses * pulseSender.LowPulses;
        return pulseValue;
    }

    ref struct PulseSender()
    {
        public int HighPulses { get; private set; }

        public int LowPulses { get; private set; }

        private readonly Queue<(int Source, int Destination, Pulse Pulse)> pulseQueue = new(4);

        private readonly Dictionary<int, IModule> destinations = [];

        public readonly void AddDestination(IModule module) => destinations.Add(module.Name, module);

        public void FollowPulse(int source, int destination, Pulse pulse)
        {
            SendPulse(source, destination, pulse);

            while (pulseQueue.TryDequeue(out var message))
            {
                if (destinations.TryGetValue(message.Destination, out var module))
                    module.Handle(message.Source, message.Pulse, ref this);
            }
        }

        public void SendPulse(int source, int destination, Pulse pulse)
        {
            if (pulse == Pulse.High)
                HighPulses++;
            else
                LowPulses++;

            pulseQueue.Enqueue((source, destination, pulse));
        }

        public readonly void BuildDependencyTree()
        {
            foreach (var item in destinations)
            {
                if (item.Value is Conjunction conjunction)
                {
                    foreach (var item2 in destinations)
                    {
                        if (item2.Value is not Conjunction && item2.Value.Destinations.Contains(conjunction.Name))
                            conjunction.AddDependency(item2.Key);
                    }
                }
            }
        }
    }

    interface IModule
    {
        int Name { get; }

        int[] Destinations { get; }

        void Handle(int source, Pulse pulse, ref PulseSender sender);

        static IModule Parse(ReadOnlySpan<char> moduleString)
        {
            int splitInx = moduleString.IndexOf(" -> ");
            var moduleType = (ModuleType)moduleString[0];

            Span<Range> destinationRanges = stackalloc Range[7];
            var destinationString = moduleString[(splitInx + 4)..];
            int destinationCount = destinationString.Split(destinationRanges, ", ");

            var destinations = new int[destinationCount];
            for (int i = 0; i < destinationCount; i++)
                destinations[i] = string.GetHashCode(destinationString[destinationRanges[i]]);

            return Create(moduleType, string.GetHashCode(moduleString[1..splitInx]), destinations);
        }

        static IModule Create(ModuleType type, int name, int[] destinations) =>
            type switch
            {
                ModuleType.Broadcaster => new Broadcaster(destinations),
                ModuleType.FlipFlop => new FlipFlop(name, destinations),
                ModuleType.Conjunction => new Conjunction(name, destinations),
                _ => throw new ArgumentException("Invalid module type", nameof(type))
            };
    }

    class Broadcaster(int[] destinations) : IModule
    {
        public static readonly int ModuleName = "broadcaster".GetHashCode();

        public int Name => ModuleName;

        public int[] Destinations => destinations;

        public void Handle(int source, Pulse pulse, ref PulseSender sender)
        {
            foreach (var destination in Destinations)
                sender.SendPulse(Name, destination, pulse);
        }
    }

    class FlipFlop(int name, int[] destinations) : IModule
    {
        public int Name => name;

        public int[] Destinations => destinations;

        private bool isOn = false;

        public void Handle(int source, Pulse pulse, ref PulseSender sender)
        {
            if (pulse == Pulse.High)
                return;

            isOn = !isOn;
            var nextPulse = isOn ? Pulse.High : Pulse.Low;

            foreach (var destination in Destinations)
                sender.SendPulse(Name, destination, nextPulse);
        }
    }

    class Conjunction(int name, int[] destinations) : IModule
    {
        public int Name => name;

        public int[] Destinations => destinations;

        private readonly Dictionary<int, Pulse> memory = [];

        public void Handle(int source, Pulse pulse, ref PulseSender sender)
        {
            if (!memory.TryAdd(source, pulse))
                memory[source] = pulse;

            foreach (var destination in Destinations)
                sender.SendPulse(Name, destination, GetNextPulse());
        }

        public void AddDependency(int source) => memory.TryAdd(source, Pulse.Low);

        private Pulse GetNextPulse()
        {
            foreach (var item in memory)
                if (item.Value == Pulse.Low)
                    return Pulse.High;
            return Pulse.Low;
        }
    }

    class Button()
    {
        public static readonly int ModuleName = "button".GetHashCode();

        public static readonly int DestinationName = Broadcaster.ModuleName;

        public static readonly Pulse StartingPulse = Pulse.Low;
    }

    enum Pulse
    {
        High,
        Low
    }

    enum ModuleType
    {
        Broadcaster = 'b',
        FlipFlop = '%',
        Conjunction = '&'
    }
}
