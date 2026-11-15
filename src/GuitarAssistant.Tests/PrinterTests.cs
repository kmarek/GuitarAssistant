using GuitarAssistant.Common.Interfaces;
using GuitarAssistant.Common.Services;
using GuitarAssistant.ScalesEngine.Enums;

namespace GuitarAssistant.Tests;

public class PrinterTests
{
    private sealed class FakeLogger : ILogger
    {
        public List<string> Messages { get; } = new();

        public void Log(string message) => Messages.Add(message);
    }

    [Fact]
    public void PrintEnumValues_LogsEveryValueAsListItem()
    {
        var logger = new FakeLogger();

        new Printer(logger).PrintEnumValues<Scale>();

        Assert.Equal(
            new[] { "- Ionian", "- Dorian", "- Phrygian", "- Lydian", "- Mixolydian", "- Aeolian", "- Locrian" },
            logger.Messages);
    }
}
