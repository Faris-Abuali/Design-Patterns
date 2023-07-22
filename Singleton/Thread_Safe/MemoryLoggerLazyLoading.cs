

namespace Singleton.Thread_Safe;

public class MemoryLoggerLazyLoading
{
    private int _InfoCount;
    private int _WarningCount;
    private int _ErrorCount;

    private List<LogMessage> _logs = new();

    private static readonly Lazy<MemoryLoggerLazyLoading> _instance =
        new(() => new MemoryLoggerLazyLoading());
    /**
     * Read only means that it can be assigned a value only at 
     * - initilization, or
     * - inside the constructor
     */

    // Making the eagerly loaded _instance readonly will assure that even inside
    // this class it cannot be reassigned anywhere else.

    private MemoryLoggerLazyLoading()
    {
    }

    public IReadOnlyCollection<LogMessage> Logs => _logs;


    // Make the Property static so that it can be accessed without instantianting the class
    //public static MemoryLoggerLazyLoading GetLogger
    //{
    //    get
    //    {
    //        return _instance;
    //    }
    //}

    // This is a simplified version of the getter
    public static MemoryLoggerLazyLoading GetLogger { get; } = _instance.Value;

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

