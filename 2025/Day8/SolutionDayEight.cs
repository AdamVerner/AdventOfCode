using AoC.Lib;

namespace AoC.Day8;

[AocProblem(8, "Playground")]
internal class SolutionDayEight : ISolution
{
    private record Box(int X, int Y, int Z);

    private static double DistanceSquared(Box a, Box b)
    {
        return Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2) + Math.Pow(a.Z - b.Z, 2);
    }

    private static (
        Box[] boxes,
        Dictionary<Box, int> bTj,
        Dictionary<int, List<Box>> jTb,
        IEnumerable<(Box p1, Box p2)> pairs
    ) ParseAndSetup(string input)
    {
        var boxes = input
            .Split('\n')
            .Select(j => j.Split(','))
            .Select(p => new Box(int.Parse(p[0]), int.Parse(p[1]), int.Parse(p[2])))
            .ToArray();

        var bTj = boxes.Select((b, i) => (b, i)).ToDictionary(b => b.b, b => b.i);
        var jTb = boxes.Select((b, i) => (b, i)).ToDictionary(b => b.i, b => new List<Box> { b.b });

        var pairs = boxes
            .SelectMany((p1, i) => boxes.Skip(i + 1).Select(p2 => (p1, p2)))
            .OrderBy(pair => DistanceSquared(pair.p1, pair.p2));

        return (boxes, bTj, jTb, pairs);
    }

    public string SolvePart1(string input)
    {
        var conNum = input.Length < 300 ? 10 : 1000;
        var setup = ParseAndSetup(input);

        foreach (var pair in setup.pairs.Take(conNum))
        {
            var j1 = setup.bTj[pair.p1];
            var j2 = setup.bTj[pair.p2];

            if (j1 == j2)
                continue;

            var newJunction = Math.Min(j1, j2);
            var oldJunction = Math.Max(j1, j2);

            setup.jTb[newJunction].AddRange(setup.jTb[oldJunction]);
            setup.jTb[oldJunction].ForEach(b => setup.bTj[b] = newJunction);
            setup.jTb.Remove(oldJunction);
        }

        // find sizes of all junctions
        var sizes = setup.jTb.Values.Select(b => b.Count).OrderByDescending(i => i).ToArray();
        return (sizes[0] * sizes[1] * sizes[2]).ToString();
    }

    public string SolvePart2(string input)
    {
        var setup = ParseAndSetup(input);

        foreach (var pair in setup.pairs)
        {
            var newJunction = Math.Min(setup.bTj[pair.p1], setup.bTj[pair.p2]);
            var oldJunction = Math.Max(setup.bTj[pair.p1], setup.bTj[pair.p2]);

            if (newJunction == oldJunction)
                continue;

            setup.jTb[newJunction].AddRange(setup.jTb[oldJunction]);
            setup.jTb[oldJunction].ForEach(b => setup.bTj[b] = newJunction);
            setup.jTb.Remove(oldJunction);

            if (setup.jTb.Count == 1)
                return (pair.p1.X * pair.p2.X).ToString();
        }
        throw new Exception("No solution found");
    }
}
