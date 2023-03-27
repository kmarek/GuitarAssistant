namespace GuitarAssistant.ScalesEngine.Scales
{
    public class DorianScale : IScale
    {
        public int NoteCount => 7;
        public List<int> Pattern => new() { 2, 1, 2, 2, 2, 1, 2 };
    }
}
