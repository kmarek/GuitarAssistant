using GuitarAssistant.ScalesEngine.Enums;

namespace GuitarAssistant.ScalesEngine.Scales
{
    public class ScaleResolver
    {
        public List<Note> GetNotes(Scale scale, Note dominant)
        {
            return new List<Note>();
        }

        private IScale GetScale(Scale scale)
        {
            switch (scale)
            {
                case Scale.Ionian:
                    return new IonianScale();
                default:
                    throw new ArgumentException("");
            }
        }
    }
}
