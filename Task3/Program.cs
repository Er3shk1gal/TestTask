using Task3.Logs;

Console.WriteLine("Log standardization tool");
Console.WriteLine("---------------------------");

Console.Write("Enter the path to the input file: ");
string? inputPath = Console.ReadLine();

Console.Write("Enter the path to the output file: ");
string? outputPath = Console.ReadLine();

if (string.IsNullOrWhiteSpace(inputPath) || string.IsNullOrWhiteSpace(outputPath))
{
    Console.WriteLine("Error: Both paths are required");
    return;
}

if (!inputPath.EndsWith(".txt") || !outputPath.EndsWith(".txt"))
{
    Console.WriteLine("Error: Both files must have .txt extension");
    return;
}

if (!File.Exists(inputPath))
{
    Console.WriteLine($"Error: Input file not found at {inputPath}");
    return;
}

var processor = new LogProcessor();
var writer = new LogWriter();

try
{
    Console.WriteLine("Processing started...");
    var standardizedLines = processor.ProcessFile(inputPath);
    writer.WriteStandardizedLogs(outputPath, standardizedLines);
    
    Console.WriteLine($"Successfully processed {standardizedLines.Count} log lines");
    Console.WriteLine($"Results saved to: {outputPath}");
}
catch (Exception ex)
{
    string problemsPath = Path.Combine(Path.GetDirectoryName(outputPath)!, "problems.txt");
    Console.WriteLine($"Invalid entries saved to: {problemsPath}");
    File.Copy(inputPath, Path.Combine(Path.GetDirectoryName(outputPath)!, "problems.txt"), true);
}