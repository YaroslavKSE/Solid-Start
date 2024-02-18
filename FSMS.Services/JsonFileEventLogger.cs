using System.Text.Json;
using FSMS.Core.Interfaces;
using FSMS.Core.Models;

namespace FSMS.Services;

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