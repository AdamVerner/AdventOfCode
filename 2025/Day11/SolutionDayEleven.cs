using AoC.Lib;

namespace AoC.Day11;

[AocProblem(11, "Reactor")]
internal class SolutionDayEleven : ISolution
{
    // basic dfs with cache
    private static long CountPaths(
        Dictionary<string, HashSet<string>> graph,
        string current,
        string target,
        HashSet<string> visited,
        Dictionary<(string, string), long> cache
    )
    {
        if (current == target)
            return 1;

        if (cache.ContainsKey((current, target)))
            return cache[(current, target)];

        visited.Add(current);

        var pathCount = graph[current]
            .Where(neighbor => !visited.Contains(neighbor))
            .Sum(neighbor => CountPaths(graph, neighbor, target, visited, cache));

        visited.Remove(current);

        cache[(current, target)] = pathCount;
        return pathCount;
    }

    private static Dictionary<string, HashSet<string>> Parse(string input)
    {
        return input
            .Split('\n')
            .Select(line => (V: line.Split(':').First(), O: line.Split(' ')[1..].ToHashSet()))
            .ToDictionary(k => k.V, v => v.O);
    }

    public string SolvePart1(string input)
    {
        var graph = Parse(input);
        return CountPaths(graph, "you", "out", [], []).ToString();
    }

    public string SolvePart2(string input)
    {
        var graph = Parse(input);

        return (
            CountPaths(graph, "svr", "fft", ["out", "dac"], [])
                * CountPaths(graph, "fft", "dac", ["out"], [])
                * CountPaths(graph, "dac", "out", ["fft", "dac"], [])
            + CountPaths(graph, "svr", "dac", ["out", "dac"], [])
                * CountPaths(graph, "dac", "fft", ["out"], [])
                * CountPaths(graph, "fft", "out", ["out", "dac"], [])
        ).ToString();
    }
}
