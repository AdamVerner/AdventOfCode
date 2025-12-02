using AoC.Lib;
using MoreLinq;

namespace AoC.Day2;

[AocProblem(2, "Gift Shop")]
internal class SolutionDayTwo : ISolution
{
    private static bool IsIdValid1(string id)
    {
        var mid = id.Length / 2;
        return id[..mid] == id[mid..];
    }

    public string SolvePart1(string input)
    {
        return Parse(input)
            .Select(p =>
                p.Item1.Range(p.Item2 - p.Item1 + 1)
                    .Choose(a => (IsIdValid1(a.ToString()), a))
                    .Sum()
            )
            .Sum()
            .ToString();
    }

    private static bool IsIdValid2(string id)
    {
        return Enumerable
            .Range(1, id.Length / 2)
            .Select(i => string.Join("", id[..i].Repeat(id.Length / i)))
            .Any(c => c == id);
    }

    public string SolvePart2(string input)
    {
        return Parse(input)
            .Select(
                (p) =>
                    p
                        .Item1.Range(p.Item2 - p.Item1 + 1)
                        .Choose(a => (IsIdValid2(a.ToString()), a))
                        .Sum()
            )
            .Sum()
            .ToString();
    }

    private static IEnumerable<(long, long)> Parse(string input) =>
        input.Split(',').Select(x => (long.Parse(x.Split('-')[0]), long.Parse(x.Split('-')[1])));
}

public static class LongEnumerable
{
    public static IEnumerable<long> Range(this long start, long count) => Rangelong(start, count);

    private static IEnumerable<long> Rangelong(long start, long count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        var end = start + count;
        for (var v = start; v < end; v++)
            yield return v;
    }
}
