using GuitarAssistant.ScalesEngine.Enums;

namespace GuitarAssistant.ScalesEngine.Engine;

public static class NotesCalculator
{
    private const int NotesInOctave = 12;

    // Interval pattern (in semitones) of the Ionian mode. Every other mode is a rotation
    // of this pattern; the value of the Scale enum is the rotation offset.
    private static readonly int[] IonianPattern = { 2, 2, 1, 2, 2, 2, 1 };

    public static IReadOnlyList<int> GetPattern(Scale scale)
    {
        if (!Enum.IsDefined(scale))
            throw new ArgumentOutOfRangeException(nameof(scale), scale, "Unknown scale.");

        int shift = (int)scale;
        return IonianPattern.Skip(shift).Concat(IonianPattern.Take(shift)).ToArray();
    }

    /// <summary>
    /// Returns the notes of the scale starting from <paramref name="root"/>,
    /// including the root repeated an octave higher at the end.
    /// </summary>
    public static List<Note> GetNotes(Scale scale, Note root)
    {
        if (!Enum.IsDefined(root))
            throw new ArgumentOutOfRangeException(nameof(root), root, "Unknown note.");

        List<Note> notes = new() { root };
        int current = (int)root;

        foreach (int step in GetPattern(scale))
        {
            // Note values are 1..12, so shift to 0..11 for the modulo and back.
            current = (current - 1 + step) % NotesInOctave + 1;
            notes.Add((Note)current);
        }

        return notes;
    }
}
