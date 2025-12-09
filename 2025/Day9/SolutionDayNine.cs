using AoC.Lib;
using MoreLinq;

// ReSharper disable NotAccessedPositionalProperty.Global

namespace AoC.Day9;

[AocProblem(9, "Movie Theatre")]
internal class SolutionDayNine : ISolution
{
    public string SolvePart1(string input)
    {
        var points = input
            .Split('\n')
            .Select(line => new Point(
                X: int.Parse(line.Split(',')[0]),
                Y: int.Parse(line.Split(',')[1])
            ));

        return points
            .Subsets(2)
            .Select(s => new Rect(s[0], s[1]))
            .Select(r => r.Area)
            .Max()
            .ToString();
    }

    public string SolvePart2(string input)
    {
        var points = input
            .Split('\n')
            .Select(line => new Point(
                X: int.Parse(line.Split(',')[0]),
                Y: int.Parse(line.Split(',')[1])
            ))
            .ToArray();

        // bounding polygon
        var edges = points
            .Append(points[0])
            .Pairwise((first, second) => new Rect(first, second))
            .ToArray();

        var nonOverlappingArea = points
            .Subsets(2)
            .Select(s => new Rect(s[0], s[1]))
            .Where(rect =>
                !edges.Any(edge =>
                    rect.MinX < edge.MaxX // r.left is on the left of poly.right
                    && rect.MaxX > edge.MinX // r.right is on the right of poly.left
                    && rect.MinY < edge.MaxY // r.bottom is on the below of poly.top
                    && rect.MaxY > edge.MinY // r.top is on the above of poly.bottom
                )
            )
            .Select(rect => rect.Area)
            .Max();

        return nonOverlappingArea.ToString();
    }
}

internal record Point(int X, int Y);

internal record Rect(Point A, Point B)
{
    internal readonly int MinX = Math.Min(A.X, B.X);
    internal readonly int MinY = Math.Min(A.Y, B.Y);
    public readonly int MaxX = Math.Max(A.X, B.X);
    public readonly int MaxY = Math.Max(A.Y, B.Y);
    public long Area => (long)(MaxX - MinX + 1) * (MaxY - MinY + 1);
}
