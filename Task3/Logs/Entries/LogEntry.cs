using System.Text.RegularExpressions;

namespace Task3.Logs;

public abstract class LogEntry
{

    #region Fields

    private DateTime _date;
    private TimeSpan _time;
    private string _callingMethod;
    private string _message;
    private string _logLevelString;
    private bool _isValid;
    
    #endregion

    #region Properties

    public DateTime Date
    {
        get => _date;
        protected set => _date = value;
    }

    public TimeSpan Time
    {
        get => _time;
        protected set => _time = value;
    }

    public string CallingMethod
    {
        get => _callingMethod;
        protected set => _callingMethod = value;
    }

    public string Message
    {
        get => _message;
        protected set => _message = value;
    }

    public string LogLevelString
    {
        get => _logLevelString;
        protected set => _logLevelString = value;
    }

    public bool IsValid  
    {
        get => _isValid;
        protected set => _isValid = value;
    }

    #endregion

    #region Methods

    protected LogLevel ParseLogLevel(string level)
    {
        return level switch
        {
            "INFORMATION" => LogLevel.INFO,
            "INFO" => LogLevel.INFO,
            "WARNING" => LogLevel.WARN,
            "WARN" => LogLevel.WARN,
            "ERROR" => LogLevel.ERROR,
            "DEBUG" => LogLevel.DEBUG
        };
    }
    public virtual string ToStandardFormat()
    {
        if (!IsValid)
            throw new InvalidOperationException("Cannot format invalid log entry");
    
        string timeFormat = Time.ToString(@"hh\:mm\:ss\.ffff").TrimEnd('0');
        CallingMethod = string.IsNullOrWhiteSpace(CallingMethod)
            ? "DEFAULT"
            : CallingMethod;
        return $"{Date:dd-MM-yyyy}\t{timeFormat}\t{ParseLogLevel(LogLevelString)}\t{CallingMethod}\t{Message}";
    }
    protected bool IsValidLogEntry(Match match)
    {
        if (string.IsNullOrWhiteSpace(LogLevelString) || 
            !(LogLevelString == "INFORMATION" || LogLevelString == "ERROR" || 
              LogLevelString == "WARNING" || LogLevelString == "DEBUG" || LogLevelString == "INFO" || LogLevelString == "WARN" ))
        {
            return false;
        }
        
        if (string.IsNullOrWhiteSpace(Message))
        {
            return false;
        }
        
        if (!Regex.IsMatch(match.Groups["time"].Value, @"^\d{2}:\d{2}:\d{2}(\.\d{1,7})?$"))
        {
            return false;
        }

        return true;
    }

    protected TimeSpan ParseTime(string timeStr)
    {
        var timeMatch = Regex.Match(timeStr, @"^(\d{2}:\d{2}:\d{2})(?:\.(\d{1,7}))?$");
        if (!timeMatch.Success || !TimeSpan.TryParse(timeMatch.Groups[1].Value, out var time))
        {
            return TimeSpan.Zero;
        }

        if (timeMatch.Groups[2].Success)
        {
            var fraction = timeMatch.Groups[2].Value;
            var seconds = double.Parse($"0.{fraction}");
            time = time.Add(TimeSpan.FromSeconds(seconds));
        }

        return time;
    }
    #endregion
}