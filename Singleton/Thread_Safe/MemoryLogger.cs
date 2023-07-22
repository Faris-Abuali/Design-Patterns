

namespace Singleton.Thread_Safe;

public class MemoryLogger
{
    private int _InfoCount;
    private int _WarningCount;
    private int _ErrorCount;

    private List<LogMessage> _logs = new List<LogMessage>();

    private static MemoryLogger? _instance;
    private static readonly object _lock = new object();
    /**
     * Read only means that it can be assigned a value only at 
     * - initilization, or
     * - inside the constructor
     */

    private MemoryLogger()
    {
    }

    public IReadOnlyCollection<LogMessage> Logs => _logs;


    // Make the Property static so that it can be accessed without instantianting the class
    public static MemoryLogger GetLogger
    {
        get
        {
            // The locking is only needed in case the instance is null.
            if (_instance == null)
            {
                // lock() requires a shared resource to be passed to it.
                // Shared resource can be a static field in your class
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new MemoryLogger();
                }
            }
            return _instance;
        }
    }

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

