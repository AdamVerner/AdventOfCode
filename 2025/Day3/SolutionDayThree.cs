using System.Text;
using AoC.Lib;
using MoreLinq;

namespace AoC.Day3;

[AocProblem(3, "TODO: Add problem title")]
internal class SolutionDayThree : ISolution
{
    public string SolvePart1(string input)
    {
        return input
            .Split('\n')
            .Select(line =>
            {
                var b1 = line.Select((c, i) => (value: c - '0', index: i))
                    .Take(line.Length - 1)
                    .OrderByDescending(d => d.value)
                    .ThenBy(d => d.index)
                    .First();

                var b2 = line.Select((c, i) => (value: c - '0', index: i))
                    .Where((_, i) => i > b1.index)
                    .OrderByDescending(d => d.value)
                    .ThenBy(d => -d.index)
                    .First();

                var num = b1.value * 10 + b2.value;
                return num;
            })
            .Sum()
            .ToString();
    }

    private static string SelectMax(string s, int k)
    {
        var drop = s.Length - k;
        var stack = new Stack<char>();

        foreach (var c in s)
        {
            while (stack.Count > 0 && drop > 0 && stack.Peek() < c)
            {
                stack.Pop();
                drop--;
            }
            stack.Push(c);
        }
        return string.Join("", stack.Reverse().Take(k));
    }

    public string SolvePart2(string input)
    {
        return input
            .Split('\n')
            .Select(line => long.Parse(string.Join("", SelectMax(line, 12))))
            .Sum()
            .ToString();
    }
}
