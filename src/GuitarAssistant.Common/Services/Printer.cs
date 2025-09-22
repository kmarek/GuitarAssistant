using GuitarAssistant.Common.Interfaces;

namespace GuitarAssistant.Common.Services
{
    public class Printer
    {
        private readonly ILogger _logger;

        public Printer(ILogger logger)
        {
            _logger = logger;
        }

        public void PrintEnumValues<TEnum>() where TEnum : struct, Enum
        {
            Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToList().ForEach(x => _logger.Log($"- {x}"));
        }
    }
}
