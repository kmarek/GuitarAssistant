using GuitarAssistant.ScalesEngine.Enums;

namespace GuitarAssistant.ScalesEngine.Scales;

public class ScaleResolver
{
    public static IScale GetScale(Scale scale)
    {
        return scale switch
        {
            Scale.Ionian => new IonianScale(),
            Scale.Dorian => new DorianScale(),
            Scale.Phrygian => new PhrygianScale(),
            Scale.Lydian => new LydianScale(),
            Scale.Mixolydian => new MixolydianScale(),
            Scale.Aeolian => new AeolianScale(),
            Scale.Locrian => new LocrianScale(),
            _ => throw new ArgumentException(""),
        };
    }
}
