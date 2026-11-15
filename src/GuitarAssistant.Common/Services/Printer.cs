using GuitarAssistant.Common.Interfaces;

namespace GuitarAssistant.Common.Services;

public class Printer
{
    private readonly ILogger _logger;

    public Printer(ILogger logger)
    {
        _logger = logger;
    }

    public void PrintEnumValues<TEnum>() where TEnum : struct, Enum
    {
        foreach (TEnum value in Enum.GetValues<TEnum>())
            _logger.Log($"- {value}");
    }
}
