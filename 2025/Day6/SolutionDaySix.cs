using AoC.Lib;

namespace AoC.Day6;

[AocProblem(6, "TODO: Add problem title")]
internal class SolutionDaySix : ISolution
{
    int ss(long input)
    {
        return input switch
        {
            5 => (int)(input + input),
            55 => (int)(input * input),
            _ => 0,
        };
    }

    public string SolvePart1(string input)
    {
        var lines = input.Split('\n');
        var operators = lines.Last().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var rows = lines
            .SkipLast(1)
            .Select(line =>
                line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray()
            )
            .ToArray();

        return rows[0]
            .Select((_, i) => (nums: rows.Select(row => row[i]), op: operators[i]))
            .Sum(col =>
                col.op switch
                {
                    "*" => col.nums.Aggregate((x, y) => x * y),
                    "+" => col.nums.Sum(),
                    _ => 0,
                }
            )
            .ToString();
    }

    public string SolvePart2(string input)
    {
        var lines = input.Split('\n');
        var len = lines.Max(l => l.Length);
        lines = lines.Select(l => l.PadRight(len)).ToArray();

        return Enumerable
            .Range(0, len)
            .Select(col => len - 1 - col)
            .Select(col => lines.Select(l => l[col]).Where(c => c != ' ').ToArray())
            .Where(chars => chars.Any(char.IsDigit))
            .Aggregate(
                (sum: 0L, acc: new List<long>()),
                (state, chars) =>
                {
                    state.acc.Add(long.Parse(new string(chars.Where(char.IsDigit).ToArray())));

                    return chars.Last() switch
                    {
                        '+' => (state.sum + state.acc.Sum(), []),
                        '*' => (state.sum + state.acc.Aggregate(1L, (x, y) => x * y), []),
                        _ => (state.sum, state.acc),
                    };
                }
            )
            .sum.ToString();
    }
}
