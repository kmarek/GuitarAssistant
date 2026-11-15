using GuitarAssistant.ScalesEngine.Engine;
using GuitarAssistant.ScalesEngine.Enums;

namespace GuitarAssistant.Tests;

public class NotesCalculatorTests
{
    [Theory]
    [InlineData(Scale.Ionian, new[] { 2, 2, 1, 2, 2, 2, 1 })]
    [InlineData(Scale.Dorian, new[] { 2, 1, 2, 2, 2, 1, 2 })]
    [InlineData(Scale.Phrygian, new[] { 1, 2, 2, 2, 1, 2, 2 })]
    [InlineData(Scale.Lydian, new[] { 2, 2, 2, 1, 2, 2, 1 })]
    [InlineData(Scale.Mixolydian, new[] { 2, 2, 1, 2, 2, 1, 2 })]
    [InlineData(Scale.Aeolian, new[] { 2, 1, 2, 2, 1, 2, 2 })]
    [InlineData(Scale.Locrian, new[] { 1, 2, 2, 1, 2, 2, 2 })]
    public void GetPattern_ReturnsModeIntervals(Scale scale, int[] expected)
    {
        Assert.Equal(expected, NotesCalculator.GetPattern(scale));
    }

    [Theory]
    [MemberData(nameof(AllScales))]
    public void GetPattern_SpansExactlyOneOctave(Scale scale)
    {
        var pattern = NotesCalculator.GetPattern(scale);

        Assert.Equal(7, pattern.Count);
        Assert.Equal(12, pattern.Sum());
    }

    // Every mode built on its "white key" root contains only natural notes.
    [Theory]
    [InlineData(Scale.Ionian, Note.C, "C D E F G A H C")]
    [InlineData(Scale.Dorian, Note.D, "D E F G A H C D")]
    [InlineData(Scale.Phrygian, Note.E, "E F G A H C D E")]
    [InlineData(Scale.Lydian, Note.F, "F G A H C D E F")]
    [InlineData(Scale.Mixolydian, Note.G, "G A H C D E F G")]
    [InlineData(Scale.Aeolian, Note.A, "A H C D E F G A")]
    [InlineData(Scale.Locrian, Note.H, "H C D E F G A H")]
    public void GetNotes_WhiteKeyModes_ReturnNaturalNotes(Scale scale, Note root, string expected)
    {
        Assert.Equal(expected, string.Join(" ", NotesCalculator.GetNotes(scale, root)));
    }

    [Theory]
    [InlineData(Scale.Ionian, Note.G, "G A H C D E Fis G")]
    [InlineData(Scale.Ionian, Note.F, "F G A B C D E F")]
    [InlineData(Scale.Aeolian, Note.E, "E Fis G A H C D E")]
    [InlineData(Scale.Dorian, Note.H, "H Cis D E Fis Gis A H")]
    public void GetNotes_TransposedScales_ReturnCorrectNotes(Scale scale, Note root, string expected)
    {
        Assert.Equal(expected, string.Join(" ", NotesCalculator.GetNotes(scale, root)));
    }

    // Starting from the highest note (H = 12) forces every step to wrap around the octave.
    [Theory]
    [MemberData(nameof(AllScales))]
    public void GetNotes_WrapsAroundOctave_AndEndsOnRoot(Scale scale)
    {
        var notes = NotesCalculator.GetNotes(scale, Note.H);

        Assert.Equal(8, notes.Count);
        Assert.Equal(Note.H, notes[0]);
        Assert.Equal(Note.H, notes[^1]);
        Assert.All(notes, note => Assert.True(Enum.IsDefined(note)));
    }

    [Fact]
    public void GetNotes_UndefinedRoot_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => NotesCalculator.GetNotes(Scale.Ionian, (Note)0));
        Assert.Throws<ArgumentOutOfRangeException>(() => NotesCalculator.GetNotes(Scale.Ionian, (Note)40));
    }

    [Fact]
    public void GetPattern_UndefinedScale_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => NotesCalculator.GetPattern((Scale)9));
    }

    public static IEnumerable<object[]> AllScales() =>
        Enum.GetValues<Scale>().Select(scale => new object[] { scale });
}
