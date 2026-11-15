using GuitarAssistant.ScalesEngine.Enums;

namespace GuitarAssistant.Cli;

internal static class CommandParser
{
    private const string ScalePrefix = "-scale=";
    private const string RootPrefix = "-root=";

    /// <exception cref="ArgumentException">Arguments are missing, duplicated, unknown or invalid.</exception>
    public static Parameters Parse(string[] args)
    {
        Scale? scale = null;
        Note? root = null;

        foreach (string arg in args)
        {
            if (arg.StartsWith(ScalePrefix, StringComparison.OrdinalIgnoreCase))
            {
                if (scale is not null)
                    throw new ArgumentException("Argument -scale was given more than once.");

                scale = ParseName<Scale>(arg[ScalePrefix.Length..], "scale");
            }
            else if (arg.StartsWith(RootPrefix, StringComparison.OrdinalIgnoreCase))
            {
                if (root is not null)
                    throw new ArgumentException("Argument -root was given more than once.");

                root = ParseName<Note>(arg[RootPrefix.Length..], "root note");
            }
            else
            {
                throw new ArgumentException($"Unknown argument '{arg}'.");
            }
        }

        if (scale is null)
            throw new ArgumentException("Missing required argument -scale.");

        if (root is null)
            throw new ArgumentException("Missing required argument -root.");

        return new Parameters(scale.Value, root.Value);
    }

    // Accepts only exact (case-insensitive) enum names. Enum.TryParse is not used because it
    // also accepts numbers ("9") and comma-separated combinations ("C,D").
    private static TEnum ParseName<TEnum>(string value, string argumentName) where TEnum : struct, Enum
    {
        foreach (TEnum candidate in Enum.GetValues<TEnum>())
        {
            if (string.Equals(candidate.ToString(), value, StringComparison.OrdinalIgnoreCase))
                return candidate;
        }

        throw new ArgumentException($"Value '{value}' is not a valid {argumentName}.");
    }
}
