
ILogger logger = new SeriLog(EnumLogLevel.Trace);
try
{
    int a = 1;
    int b = 0;
    logger.LogDebug($"a value is {a}");
    logger.LogDebug($"b value is {b}");
    int result = a / b;
    logger.LogDebug($"result is {result}");
}
catch (Exception ex)
{
    logger.LogError($"An error occurred: {ex.ToString()}");
}

public enum EnumLogLevel
{
    Fatal,
    Error,
    Warn,
    Info,
    Debug,
    Trace
}
public interface ILogger
{
    void LogFatal(string message);
    void LogError(string message);
    void LogWarn(string message);
    void LogInfo(string message);
    void LogDebug(string message);
    void LogTrace(string message);
}




public class SeriLog : ILogger
{
    private readonly EnumLogLevel _loglevel;
    public SeriLog(EnumLogLevel loglevel)
    {
        _loglevel = loglevel;
    }
    public void LogDebug(string message)
    {
        if (_loglevel is EnumLogLevel.Debug || (int)_loglevel >= 4)
            Console.WriteLine($"SeriLog - DEBUG {message}");
    }

    public void LogError(string message)
    {
        if (_loglevel is EnumLogLevel.Error || (int)_loglevel >= 1)
            Console.WriteLine($"SeriLog - Error {message}");
    }

    public void LogFatal(string message)
    {
        if (_loglevel is EnumLogLevel.Fatal || (int)_loglevel <= 5)

            Console.WriteLine($"SeriLog - Fatal {message}");
    }

    public void LogInfo(string message)
    {
        if (_loglevel is EnumLogLevel.Info || (int)_loglevel >= 3)
            Console.WriteLine($"SeriLog - Info {message}");
    }

    public void LogTrace(string message)
    {
        if (_loglevel is EnumLogLevel.Trace || (int)_loglevel >= 5)
            Console.WriteLine($"SeriLog - Trace {message}");
    }

    public void LogWarn(string message)
    {
        if (_loglevel is EnumLogLevel.Warn || (int)_loglevel >= 2)
            Console.WriteLine($"SeriLog - Warn {message}");
    }
}

public class NLog : ILogger
{
    public void LogDebug(string message)
    {
        Console.WriteLine($"NLog - DEBUG {message}");
    }

    public void LogError(string message)
    {
        Console.WriteLine($"NLog - Error {message}");
    }

    public void LogFatal(string message)
    {
        Console.WriteLine($"NLog - Fatal {message}");
    }

    public void LogInfo(string message)
    {
        Console.WriteLine($"NLog - Info {message}");
    }

    public void LogTrace(string message)
    {
        Console.WriteLine($"NLog - Trace {message}");
    }

    public void LogWarn(string message)
    {
        Console.WriteLine($"NLog - Warn {message}");
    }
}