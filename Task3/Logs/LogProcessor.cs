namespace Task3.Logs;

public class LogProcessor
{
    #region Methods

    public List<string> ProcessFile(string inputFilePath)
    {
        var validLines = new List<string>();
        int lineNumber = 0;

        foreach (var line in File.ReadLines(inputFilePath))
        {
            lineNumber++;
        
            if (string.IsNullOrWhiteSpace(line))
            {
                Console.WriteLine($"Skipping empty line {lineNumber}");
                continue;
            }

            LogEntry entry;
            try
            {
                entry = line.Contains('|') 
                    ? new LogEntryFormatTwo(line) 
                    : new LogEntryFormatOne(line);

                if (!entry.IsValid || entry.Date.Year <= 1 || string.IsNullOrEmpty(entry.LogLevelString))
                {
                    Console.WriteLine($"Invalid log entry at line {lineNumber}");
                    throw new FormatException($"Invalid log entry at line {lineNumber}");
                }

                validLines.Add(entry.ToStandardFormat());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing line {lineNumber}: {ex.Message}");
                throw new FormatException(ex.Message);
            }
        }

        if (validLines.Count == 0)
        {
            throw new InvalidDataException("No valid log entries found in file");
        }

        return validLines;
    }

    #endregion
}