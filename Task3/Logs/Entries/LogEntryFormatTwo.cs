using System.Globalization;
using System.Text.RegularExpressions;

namespace Task3.Logs;

public class LogEntryFormatTwo : LogEntry
{
    #region Regex

    private static readonly Regex Format2Regex = new Regex(
        @"^(?<date>\d{4}-\d{2}-\d{2})\s+(?<time>\d{2}:\d{2}:\d{2}\.\d{4})\s*\|\s*(?<level>\w+)\s*\|\d+\s*\|(?<method>[^|]+)\s*\|\s*(?<message>.+)$",
        RegexOptions.Compiled);
    

    #endregion


    #region Constructor

    public LogEntryFormatTwo(string logLine)
    {
        try 
        {
            var match = Format2Regex.Match(logLine);
            if (!match.Success)
            {
                IsValid = false;
                return;
            }
            
            Date = DateTime.ParseExact(
                match.Groups["date"].Value, 
                "yyyy-MM-dd", 
                CultureInfo.InvariantCulture,
                DateTimeStyles.None);
            
          
            var timeStr = match.Groups["time"].Value;
            Time = ParseTime(timeStr);
            
          
            LogLevelString = match.Groups["level"].Value;
            
            CallingMethod = match.Groups["method"].Value.Trim();
           
            Message = match.Groups["message"].Value.Trim();
            
            IsValid = IsValidLogEntry(match);
        }
        catch
        {
            IsValid = false;
        }
    }

    #endregion
    
}