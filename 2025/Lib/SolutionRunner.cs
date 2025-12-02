using System.Reflection;

namespace AoC.Lib;

[AttributeUsage(AttributeTargets.Class)]
internal class AocProblemAttribute(int day, string name) : Attribute
{
    public int Day { get; } = day;
    public string Name { get; } = name;
}

public class SolutionRunner
{
    public void RunToday()
    {
        var today = DateTime.Now.Day;
        RunDay(today);
    }

    private void RunDay(int day)
    {
        var solutionType = FindSolutionForDay(day);

        if (solutionType is null)
        {
            Console.WriteLine($"No solution found for day {day}");
            return;
        }

        var attribute = solutionType.GetCustomAttribute<AocProblemAttribute>();
        Console.WriteLine($"Day {attribute!.Day}: {attribute.Name}");
        Console.WriteLine(new string('-', 40));

        var solution = (ISolution)Activator.CreateInstance(solutionType)!;

        RunSolver(day, 1, solution.SolvePart1);
        Console.WriteLine();
        RunSolver(day, 2, solution.SolvePart2);
    }

    private static string? LoadFromFile(string inputPath)
    {
        // var inputPath = $"Day{day}/input.txt";
        if (File.Exists(inputPath))
        {
            var content = File.ReadAllText(inputPath).Trim();
            if (content.Length > 0)
                return content;
            Console.WriteLine($"File is empty: {inputPath}");
            return null;
        }
        Console.WriteLine($"File not found: {inputPath}");
        return null;
    }

    private void RunSolver(int day, int part, Func<string, string> solver)
    {
        var refInput = LoadFromFile($"Day{day}/ref.input.txt");
        var refOutput = LoadFromFile($"Day{day}/ref.output.p{part}.txt");
        if (refInput == null || refOutput == null)
        {
            Console.WriteLine("Skipping part {part} as reference input or output is missing");
            return;
        }

        try
        {
            var refResult = solver(refInput);
            var match =
                refResult.Trim() == refOutput
                    ? "  ✓ Matches reference output"
                    : $"  ✗ Expected: {refOutput}";
            Console.WriteLine($"Part {part} reference: {refResult} {match}");

            if (refResult.Trim() != refOutput)
            {
                return;
            }
        }
        catch (NotImplementedException)
        {
            Console.WriteLine($"Part {part}: Not implemented");
            return;
        }

        var input = LoadFromFile($"Day{day}/input.txt");
        if (input == null)
        {
            Console.WriteLine("Skipping part {part} as input is missing");
            return;
        }
        var finalResult = solver(input);
        Console.WriteLine($"Part {part} final:     {finalResult}");
    }

    private Type? FindSolutionForDay(int day)
    {
        var allAttributedTypes = typeof(SolutionRunner)
            .Assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<AocProblemAttribute>() is not null)
            .ToList();

        foreach (
            var type in allAttributedTypes.Where(type => !typeof(ISolution).IsAssignableFrom(type))
        )
        {
            throw new InvalidOperationException(
                $"Type '{type.FullName}' is marked with [AocProblem] but does not implement ISolution"
            );
        }

        return allAttributedTypes
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .FirstOrDefault(t =>
            {
                var attr = t.GetCustomAttribute<AocProblemAttribute>();
                return attr?.Day == day;
            });
    }
}
