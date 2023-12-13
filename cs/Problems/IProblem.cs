namespace AOC2023.Problems;

public interface IProblem<T>
{
    T Solve(ReadOnlySpan<char> input);
}
