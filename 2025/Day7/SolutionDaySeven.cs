using AoC.Lib;

namespace AoC.Day7;

[AocProblem(7, "Laboratories")]
internal class SolutionDaySeven : ISolution
{
    public string SolvePart1(string input)
    {
        return input
            .Split('\n')
            .Skip(1)
            .Aggregate(
                (beams: new HashSet<int> { input.Split('\n').First().IndexOf('S') }, splits: 0),
                (agg, line) =>
                {
                    var splitBeams = agg.beams.Where(b => line[b] == '^').ToList();
                    var simpleBeams = agg.beams.Where(b => line[b] == '.');
                    var beams = splitBeams
                        .SelectMany(b => new[] { b - 1, b + 1 })
                        .Concat(simpleBeams)
                        .ToHashSet();
                    Console.WriteLine($"{line}, {beams.Count}");
                    return (beams, agg.splits + (splitBeams.Count));
                }
            )
            .splits.ToString();
    }

    public string SolvePart2(string input)
    {
        var currentIndex = input.Split('\n').First().IndexOf('S');
        return (
            SolverRecursive(
                currentIndex,
                0,
                input.Split('\n').Skip(1).ToArray(),
                new Dictionary<(int, int), long>()
            ) + 1
        ).ToString();
    }

    private long SolverRecursive(
        int currentIndex,
        int lineIndex,
        string[] lines,
        Dictionary<(int, int), long> cache
    )
    {
        if (lineIndex >= lines.Length)
            return 0;

        if (cache.TryGetValue((currentIndex, lineIndex), out var cached))
            return cached;

        var currentLine = lines[lineIndex];

        var result = currentLine[currentIndex] switch
        {
            '.' => SolverRecursive(currentIndex, lineIndex + 1, lines, cache),
            '^' => SolverRecursive(currentIndex + 1, lineIndex + 1, lines, cache)
                + SolverRecursive(currentIndex - 1, lineIndex + 1, lines, cache)
                + 1,
            _ => throw new Exception("Invalid state"),
        };

        cache[(currentIndex, lineIndex)] = result;
        return result;
    }
}
