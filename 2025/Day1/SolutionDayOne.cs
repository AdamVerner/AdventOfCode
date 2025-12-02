using AoC.Lib;
using MoreLinq;

namespace AoC.Day1;

[AocProblem(1, "Secret Entrance")]
internal class SolutionDayOne : ISolution
{
    private static int Mod(int a, int b)
    {
        return (a % b + b) % b;
    }

    public string SolvePart1(string input)
    {
        return input
            .Split('\n')
            .Select(line => (line[0] == 'R' ? 1 : -1) * int.Parse(line[1..]))
            .Scan(50, (pos, value) => Mod(pos + value, 100))
            .Count(pos => pos == 0)
            .ToString();
    }

    public string SolvePart2(string input)
    {
        return input
            .Split('\n')
            .Select(line =>
                Enumerable.Repeat(line[0] == 'R' ? 1 : -1, int.Parse(line[1..])).ToArray()
            )
            .SelectMany(x => x)
            .Scan(50, (pos, value) => Mod(pos + value, 100))
            .Count(pos => pos == 0)
            .ToString();
    }
}
