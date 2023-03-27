using GuitarAssistant.ScalesEngine.Enums;

namespace GuitarAssistant.ScalesEngine.Scales
{
    public interface IScale
    {
        public List<int> Pattern { get; }
        public List<Note> GetNotes(Note dominant);
    }
}
