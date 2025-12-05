using System.Collections.Immutable;
using AoC.Day2;
using AoC.Lib;

namespace AoC.Day5;

[AocProblem(5, "Cafeteria")]
public class SolutionDayFive : ISolution
{
    public string SolvePart1(string input)
    {
        var ranges = input
            .Split("\n\n")[0]
            .Split('\n')
            .Select(r =>
                (
                    start: r.Split('-').Select(long.Parse).ToArray()[0],
                    end: r.Split('-').Select(long.Parse).ToArray()[1]
                )
            )
            .ToArray();
        var ids = input.Split("\n\n")[1];

        return ids.Split('\n')
            .Select(long.Parse)
            .Count(id => ranges.Any(r => r.start <= id && id <= r.end))
            .ToString();
    }

    public string SolvePart2(string input)
    {
        return input
            .Split("\n\n")[0]
            .Split('\n')
            .Select(r =>
                (
                    start: r.Split('-').Select(long.Parse).ToArray()[0],
                    end: r.Split('-').Select(long.Parse).ToArray()[1]
                )
            )
            .OrderBy(r => r.start)
            .Aggregate(
                ImmutableList<(long start, long end)>.Empty,
                (consolidated, r) =>
                {
                    if (consolidated.IsEmpty || r.start > consolidated[^1].end + 1)
                        return consolidated.Add(r);

                    return consolidated.SetItem(
                        consolidated.Count - 1,
                        (consolidated[^1].start, Math.Max(consolidated[^1].end, r.end))
                    );
                }
            )
            .Sum(r => r.end - r.start + 1)
            .ToString();
    }
}
