namespace GuitarAssistant.ScalesEngine.Scales;

public interface IScale
{
    public int NoteCount { get; }
    public List<int> Pattern { get; }
}
