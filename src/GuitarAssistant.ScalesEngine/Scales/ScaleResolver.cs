using GuitarAssistant.ScalesEngine.Enums;

namespace GuitarAssistant.ScalesEngine.Scales
{
    public class ScaleResolver
    {
        public static IScale GetScale(Scale scale)
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
