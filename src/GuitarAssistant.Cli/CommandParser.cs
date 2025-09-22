using GuitarAssistant.Common.Services;
using GuitarAssistant.ScalesEngine.Enums;
using GuitarAssistant.Common.Extensions;

namespace GuitarAssistant.Cli;

internal class CommandParser
{
    public static Parameters Parse(string[] args)
    {
        Console.WriteLine($"args {args.Length}");
        var printer = new Printer(new ConsoleLogger());

        Parameters parameters = new();

        foreach (string arg in args)
        {
            if (arg.Contains("-scale"))
            {
                string scale = GetValue(arg);
                Console.WriteLine($"Scale: {scale}");

                bool correct = Enum.TryParse(scale, true, out Scale result);

                if (!correct)
                {
                    throw new ArgumentException($"Scale {scale} is not valid, {Environment.NewLine} VALID VALUES: {Environment.NewLine} {string.Join(Environment.NewLine, Scale.Aeolian.GetAllValues())}");
                }
                    

                parameters.Scale = result;
            }

            if (arg.Contains("-dominant"))
            {
                string dominant = GetValue(arg);
                Console.WriteLine($"Dominant: {dominant}");

                bool correct = Enum.TryParse(dominant, true, out Note result);

                if (!correct)
                    throw new ArgumentException($"Dominant {dominant} is not valid,{Environment.NewLine} VALID VALUES: {Environment.NewLine} {string.Join(Environment.NewLine, Note.A.GetAllValues())}");

                parameters.Dominant = result;
            }
        }

        return parameters;
    }

    private static string GetValue(string arg)
    {
        var array = arg.Split("=");

        return array.Length > 1 ? array[1] : string.Empty;
    }
}
