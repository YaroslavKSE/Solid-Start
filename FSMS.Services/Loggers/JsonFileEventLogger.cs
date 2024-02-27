using System.Text.Json;
using FSMS.Core.Events;
using FSMS.Core.Interfaces;

namespace FSMS.Services.Loggers;

public class JsonFileEventLogger : IEventLogger
{
    private readonly string _logFilePath;
    private const string LogFilesDirectory = "LogFiles";
    private const string LogFileName = "eventLog.json";

    public JsonFileEventLogger()
    {
        if (!Directory.Exists(LogFilesDirectory))
        {
            Directory.CreateDirectory(LogFilesDirectory);
        }

        _logFilePath = Path.Combine(LogFilesDirectory, LogFileName);
    }

    public void LogEvent(EventLogEntry eventLogEntry)
    {
        var json = JsonSerializer.Serialize(eventLogEntry, new JsonSerializerOptions {WriteIndented = true});
        File.AppendAllText(_logFilePath, json + Environment.NewLine);
    }
}