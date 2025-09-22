using GuitarAssistant.ScalesEngine.Engine;
using GuitarAssistant.ScalesEngine.Enums;

namespace GuitarAssistant.Cli;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"Provided {args.Length} arguments.");

        if (args.Length != 2)
        {
            Console.WriteLine(Constants.HelpMessage);

            Console.WriteLine("Available scales:");
            foreach (var scale in Enum.GetValues<Scale>())
            {
                Console.WriteLine($"- {scale}");
            }

            Console.WriteLine("Available dominants (notes):");
            foreach (var note in Enum.GetValues<Note>())
            {
                Console.WriteLine($"- {note}");
            }

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