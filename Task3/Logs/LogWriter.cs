namespace Task3.Logs;

public class LogWriter
{
    #region Methods

    public void WriteStandardizedLogs(string outputFilePath, IEnumerable<string> standardizedLines)
    {
        File.WriteAllLines(outputFilePath, standardizedLines);
    }

    #endregion
}