using GuitarAssistant.Common.Services;
using GuitarAssistant.ScalesEngine.Engine;
using GuitarAssistant.ScalesEngine.Enums;

namespace GuitarAssistant.Cli;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"Provided {args.Length} arguments.");
        Printer printer = new Printer(new ConsoleLogger());

        if (args.Length != 2)
        {
            Console.WriteLine(Constants.HelpMessage);

            Console.WriteLine("Available scales:");
            printer.PrintEnumValues<Scale>();

            Console.WriteLine("Available dominants (notes):");
            printer.PrintEnumValues<Note>();

            return;
        }

        var parameters = CommandParser.Parse(args);
        var notes = NotesCalculator.GetNotes(parameters.Scale, parameters.Dominant);

        Console.WriteLine($"Notes for Scale {parameters.Scale}-{parameters.Dominant}");
        notes.ForEach(note =>
        {
            Console.Write($"{note} ");
        });
    }
}