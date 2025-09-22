namespace GuitarAssistant.Common.Extensions;

public static class EnumExtensions
{
    public static List<string> GetAllValues<TEnum>(this TEnum _) where TEnum : struct, Enum
    {
        return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Select(e => e.ToString()).ToList();
    }
}