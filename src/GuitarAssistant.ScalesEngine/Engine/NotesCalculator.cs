using GuitarAssistant.ScalesEngine.Enums;
using GuitarAssistant.ScalesEngine.Scales;

namespace GuitarAssistant.ScalesEngine.Engine;

public class NotesCalculator
{
    public static List<Note> GetNotes(Scale scale, Note dominant)
    {
        List<Note> notes = new() { dominant };

        IScale scaleObj = ScaleResolver.GetScale(scale);

        Note currentNote = dominant;
        scaleObj.Pattern.ForEach(x =>
        {
            Console.WriteLine($"Adding {x} to Note {currentNote}");
            int newNoteValue = (int)currentNote + x;

            if (newNoteValue > 12)
                newNoteValue -= 12;

            Note newNote = (Note)newNoteValue;

            Console.WriteLine($"newNote {newNote} from value {newNoteValue}");
            notes.Add(newNote);
            currentNote = newNote;
        });

        return notes;
    }
}
