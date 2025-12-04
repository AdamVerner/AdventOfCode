using AoC.Lib;

namespace AoC.Day4;

[AocProblem(4, "TODO: Add problem title")]
internal class SolutionDayFour : ISolution
{
    private readonly (int x, int y)[] _neighborOffsets =
    [
        (-1, -1),
        (-1, 0),
        (-1, 1),
        (0, -1),
        (0, 1),
        (1, -1),
        (1, 0),
        (1, 1),
    ];

    public string SolvePart1(string input)
    {
        var grid = input.Split('\n').Select(l => l.Select(c => c == '@').ToArray()).ToArray();

        var total = Enumerable
            .Range(0, grid.Length)
            .SelectMany(r =>
                Enumerable
                    .Range(0, grid[r].Length)
                    .Where(c => grid[r][c])
                    .Select(c => _neighborOffsets.Count(o => IsAt(r + o.x, c + o.y)))
            )
            .Count(n => n < 4);
        return total.ToString();

        bool IsAt(int r, int c) =>
            r >= 0 && r < grid.Length && c >= 0 && c < grid[r].Length && grid[r][c];
    }

    public string SolvePart2(string input)
    {
        var grid = input.Split('\n').Select(l => l.Select(c => c == '@').ToArray()).ToArray();

        var changes = true;
        var total = 0;
        while (changes)
        {
            var indexes = Enumerable
                .Range(0, grid.Length)
                .SelectMany(y =>
                    Enumerable
                        .Range(0, grid[y].Length)
                        .Where(x => grid[y][x])
                        .Where(x => _neighborOffsets.Count(o => IsAt(y + o.x, x + o.y)) < 4)
                        .Select(x => (x: y, y: x))
                )
                .ToArray();
            total += indexes.Length;
            changes = indexes.Any();
            foreach (var (x, y) in indexes)
                grid[x][y] = false;
        }
        return total.ToString();

        bool IsAt(int r, int c) =>
            r >= 0 && r < grid.Length && c >= 0 && c < grid[r].Length && grid[r][c];
    }
}
