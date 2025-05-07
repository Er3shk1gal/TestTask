using System.Globalization;
using System.Text.RegularExpressions;

namespace Task3.Logs;

public class LogEntryFormatOne : LogEntry
{
    
    #region Fields
    
    private static readonly Regex Format1Regex = new Regex(
        @"^(?<date>\d{2}\.\d{2}\.\d{4})\s+(?<time>\d{2}:\d{2}:\d{2}\.\d{3})\s+(?<level>\w+)\s+(?<message>.+)$", 
        RegexOptions.Compiled);
    
    #endregion
    
    #region Constructor
    
    public LogEntryFormatOne(string logLine)
    {
        try 
        {
            var match = Format1Regex.Match(logLine);
            if (!match.Success)
            {
                IsValid = false;
                return;
            }
            
            Date = DateTime.ParseExact(
                match.Groups["date"].Value, 
                "dd.MM.yyyy", 
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