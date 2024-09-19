using GuitarAssistant.ScalesEngine.Engine;

namespace GuitarAssistant.Cli;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        if (args.Length == 0)
        {
            Console.WriteLine("HELP");
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