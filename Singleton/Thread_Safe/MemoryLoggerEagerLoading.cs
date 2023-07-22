

namespace Singleton.Thread_Safe;

public class MemoryLoggerEagerLoading
{
    private int _InfoCount;
    private int _WarningCount;
    private int _ErrorCount;

    private List<LogMessage> _logs = new();

    private static readonly MemoryLoggerEagerLoading _instance = new();
    /**
     * Read only means that it can be assigned a value only at 
     * - initilization, or
     * - inside the constructor
     */

    // Making the eagerly loaded _instance readonly will assure that even inside
    // this class it cannot be reassigned anywhere else.

    private MemoryLoggerEagerLoading()
    {
    }

    public IReadOnlyCollection<LogMessage> Logs => _logs;


    // Make the Property static so that it can be accessed without instantianting the class
    //public static MemoryLoggerEagerLoading GetLogger
    //{
    //    get
    //    {
    //        return _instance;
    //    }
    //}

    // This is a simplified version of the getter
    public static MemoryLoggerEagerLoading GetLogger { get; } = _instance;

    private void Log(string message, LogType logType)
        => _logs.Add(new LogMessage
        {
            Message = message,
            LogType = logType,
            CreatedAt = DateTime.Now,
        });

    public void LogInfo(string message)
    {
        ++_InfoCount;
        Log(message, LogType.INFO);
    }

    public void LogWarning(string message)
    {
        _WarningCount++;
        Log(message, LogType.WARNING);
    }

    public void LogError(string message)
    {
        _ErrorCount++;
        Log(message, LogType.ERROR);
    }

    public void ShowLog()
    {
        _logs.ForEach(x => Console.WriteLine(x));
        Console.WriteLine("-------------------------------");

        Console.WriteLine($"Info ({_InfoCount}), Warning ({_WarningCount}), Error ({_ErrorCount})");
    }
}

