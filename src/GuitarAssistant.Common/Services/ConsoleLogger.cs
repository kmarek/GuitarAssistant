using GuitarAssistant.Common.Interfaces;

namespace GuitarAssistant.Common.Services;

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}
