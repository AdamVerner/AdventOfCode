using AoC.Lib;
using MoreLinq.Extensions;

namespace AoC.Day12;

[AocProblem(12, "Christmas Tree Farm")]
internal class SolutionDayTwelve : ISolution
{
    public string SolvePart1(string input)
    {
        return input
            .Split("\n\n")
            .Last()
            .Split('\n')
            .Select(l =>
                (
                    W: int.Parse(l.Split(':').First().Split('x').First()),
                    H: int.Parse(l.Split(':').First().Split('x').Last()),
                    R: l.Split(": ").Last().Split(" ").Select(int.Parse).ToArray()
                )
            )
            .Count(r => r.W * r.H >= r.R.Sum(x => x * 9))
            .ToString();
    }

    public string SolvePart2(string input)
    {
        throw new NotImplementedException();
    }
}
