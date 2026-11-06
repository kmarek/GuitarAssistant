using GuitarAssistant.Common.Services;
using GuitarAssistant.ScalesEngine.Engine;
using GuitarAssistant.ScalesEngine.Enums;

namespace GuitarAssistant.Cli;

internal static class Program
{
    private static int Main(string[] args)
    {
        var printer = new Printer(new ConsoleLogger());

        if (args.Length == 0 || args.Any(a => a is "-h" or "-help" or "--help"))
        {
            PrintHelp(printer);
            return 0;
        }

        Parameters parameters;
        try
        {
            parameters = CommandParser.Parse(args);
        }
        catch (ArgumentException ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            Console.Error.WriteLine();
            PrintHelp(printer);
            return 1;
        }

        var notes = NotesCalculator.GetNotes(parameters.Scale, parameters.Root);

        Console.WriteLine($"Notes for scale {parameters.Scale} {parameters.Root}:");
        Console.WriteLine(string.Join(" ", notes));
        return 0;
    }

    private static void PrintHelp(Printer printer)
    {
        Console.WriteLine(Constants.HelpMessage);
        Console.WriteLine();

        Console.WriteLine("Available scales:");
        printer.PrintEnumValues<Scale>();

        Console.WriteLine("Available root notes:");
        printer.PrintEnumValues<Note>();
    }
}
