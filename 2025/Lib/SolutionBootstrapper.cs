using System.Text;

namespace AoC.Lib;

public class SolutionBootstrapper
{
    private const string CookieFile = ".aoc_cookie.txt";
    private const string AocBaseUrl = "https://adventofcode.com";
    private readonly HttpClient _httpClient;

    public SolutionBootstrapper()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "AoC Solution Runner");
    }

    public void BootstrapToday()
    {
        var today = DateTime.Now.Day;
        Bootstrap(today);
    }

    public void Bootstrap(int day)
    {
        var dayFolder = $"Day{day}";

        if (Directory.Exists(dayFolder))
        {
            Console.WriteLine($"Day {day} already exists at {dayFolder}");
            return;
        }

        Directory.CreateDirectory(dayFolder);
        Console.WriteLine($"Created directory: {dayFolder}");

        // Create solution file
        var solutionFileName = $"SolutionDay{GetDayName(day)}.cs";
        var solutionPath = Path.Combine(dayFolder, solutionFileName);
        var solutionContent = GenerateSolutionTemplate(day);
        File.WriteAllText(solutionPath, solutionContent);
        Console.WriteLine($"Created: {solutionPath}");

        // Download input from AoC
        DownloadInput(day, dayFolder);

        // Create empty reference files
        var refFiles = new[] { "ref.input.txt", "ref.output.p1.txt", "ref.output.p2.txt" };
        foreach (var file in refFiles)
        {
            var filePath = Path.Combine(dayFolder, file);
            File.WriteAllText(filePath, "");
            Console.WriteLine($"Created: {filePath}");
        }

        Console.WriteLine($"\nBootstrap complete for Day {day}!");
        Console.WriteLine($"Next steps:");
        Console.WriteLine($"1. (Optional) Add reference input/output for validation");
        Console.WriteLine($"2. Implement SolvePart1 and SolvePart2 in {solutionFileName}");
    }

    private void DownloadInput(int day, string dayFolder)
    {
        try
        {
            if (!File.Exists(CookieFile))
            {
                Console.WriteLine($"Warning: {CookieFile} not found. Skipping input download.");
                Console.WriteLine(
                    $"Create {CookieFile} with your AoC session cookie to enable automatic downloads."
                );
                // Create empty input file
                File.WriteAllText(Path.Combine(dayFolder, "input.txt"), "");
                return;
            }

            var cookie = File.ReadAllText(CookieFile).Trim();
            if (string.IsNullOrWhiteSpace(cookie))
            {
                Console.WriteLine("Warning: Cookie file is empty. Skipping input download.");
                File.WriteAllText(Path.Combine(dayFolder, "input.txt"), "");
                return;
            }

            var year = DateTime.Now.Year;
            var inputUrl = $"{AocBaseUrl}/{year}/day/{day}/input";

            var request = new HttpRequestMessage(HttpMethod.Get, inputUrl);
            request.Headers.Add("Cookie", $"session={cookie}");

            var response = _httpClient.Send(request);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(
                    $"Warning: Failed to download input (Status: {response.StatusCode})"
                );
                Console.WriteLine($"Manually download from: {inputUrl}");
                File.WriteAllText(Path.Combine(dayFolder, "input.txt"), "");
                return;
            }

            var inputContent = response.Content.ReadAsStringAsync().Result;
            var inputPath = Path.Combine(dayFolder, "input.txt");
            File.WriteAllText(inputPath, inputContent);
            Console.WriteLine($"Downloaded: {inputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error downloading input: {ex.Message}");
            File.WriteAllText(Path.Combine(dayFolder, "input.txt"), "");
        }
    }

    private string GenerateSolutionTemplate(int day)
    {
        var dayName = GetDayName(day);
        var sb = new StringBuilder();
        sb.AppendLine("using AoC.Lib;");
        sb.AppendLine();
        sb.AppendLine($"namespace AoC.Day{day};");
        sb.AppendLine();
        sb.AppendLine($"[AocProblem({day}, \"TODO: Add problem title\")]");
        sb.AppendLine($"internal class SolutionDay{dayName} : ISolution");
        sb.AppendLine("{");
        sb.AppendLine("    public string SolvePart1(string input)");
        sb.AppendLine("    {");
        sb.AppendLine("        throw new NotImplementedException();");
        sb.AppendLine("    }");
        sb.AppendLine();
        sb.AppendLine("    public string SolvePart2(string input)");
        sb.AppendLine("    {");
        sb.AppendLine("        throw new NotImplementedException();");
        sb.AppendLine("    }");
        sb.AppendLine("}");
        sb.AppendLine();

        return sb.ToString();
    }

    private string GetDayName(int day)
    {
        return day switch
        {
            1 => "One",
            2 => "Two",
            3 => "Three",
            4 => "Four",
            5 => "Five",
            6 => "Six",
            7 => "Seven",
            8 => "Eight",
            9 => "Nine",
            10 => "Ten",
            11 => "Eleven",
            12 => "Twelve",
            13 => "Thirteen",
            14 => "Fourteen",
            15 => "Fifteen",
            16 => "Sixteen",
            17 => "Seventeen",
            18 => "Eighteen",
            19 => "Nineteen",
            20 => "Twenty",
            21 => "TwentyOne",
            22 => "TwentyTwo",
            23 => "TwentyThree",
            24 => "TwentyFour",
            25 => "TwentyFive",
            _ => throw new ArgumentOutOfRangeException(nameof(day), "Day must be between 1 and 25"),
        };
    }
}
