using GuitarAssistant.ScalesEngine.Enums;

namespace GuitarAssistant.ScalesEngine.Scales
{
    public class IonianScale : IScale
    {
        public int NoteCount => 7;
        public List<int> Pattern => new() { 2, 2, 1, 2, 2, 2, 1 };

        public List<Note> GetNotes(Note dominant)
        {
            throw new NotImplementedException();
        }
    }
}