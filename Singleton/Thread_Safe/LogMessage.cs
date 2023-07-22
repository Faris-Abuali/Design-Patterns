
namespace Singleton.Thread_Safe;

public class LogMessage
{
    public required string Message { get; set; }
    public LogType LogType { get; set; }
    public DateTime CreatedAt { get; set; }

    public override string ToString()
    {
        var timestamp = CreatedAt.ToString("yyy-MM-dd hh:mm");

        return $"{LogType,7} [{timestamp}] {Message}";
    }
}

