using AoC.Lib;
using MoreLinq;

namespace AoC.Day10;

[AocProblem(10, "Factory")]
internal class SolutionDayTen : ISolution
{
    public string SolvePart1(string input)
    {
        var total = 0;
        foreach (var line in input.Split('\n'))
        {
            var target = line.Split(' ')
                .First()[1..^1]
                .Select((c, i) => c == '#' ? 1 << i : 0)
                .Sum();

            var wirings = line.Split(' ')[1..^1]
                .Select(x =>
                    x[1..^1].Split(',').Select(s => 1 << int.Parse(s)).Aggregate(0, (a, b) => a | b)
                )
                .ToArray();

            static IEnumerable<HashSet<int>> BfsLevels(int start, int[] flippers)
            {
                var visited = new HashSet<int>();
                var frontier = new HashSet<int> { start };

                while (frontier.Count > 0)
                {
                    yield return frontier;
                    visited.UnionWith(frontier);
                    frontier = frontier
                        .SelectMany(s => flippers.Select(f => s ^ f))
                        .Where(n => !visited.Contains(n))
                        .ToHashSet();
                }
            }

            total += BfsLevels(0, wirings)
                .Select((level, depth) => (level, depth))
                .First(x => x.level.Contains(target))
                .depth;
        }

        return total.ToString();
    }

    public string SolvePart2(string input)
    {
        var tempFile = Path.GetTempFileName();
        File.WriteAllText(tempFile, input);

        var process = new System.Diagnostics.Process
        {
            StartInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "./Day10/venv/bin/python",
                Arguments = $"Day10/p2Solver.py {tempFile}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            },
        };
        process.Start();
        var output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        File.Delete(tempFile);

        return output.Trim();
    }
}
