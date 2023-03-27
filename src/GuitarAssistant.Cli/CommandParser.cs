using GuitarAssistant.ScalesEngine.Enums;

namespace GuitarAssistant.Cli
{
    internal class CommandParser
    {
        public static Parameters Parse(string[] args)
        {
            Console.WriteLine($"args {args.Length}");

            Parameters parameters = new();

            foreach (string arg in args)
            {
                if (arg.Contains("-scale"))
                {
                    string scale = GetValue(arg);
                    Console.WriteLine($"Scale: {scale}");

                    bool correct = Enum.TryParse(scale, true, out Scale result);

                    if (correct)
                        parameters.Scale = result;
                }

                if (arg.Contains("-dominant"))
                {
                    string dominant = GetValue(arg);
                    Console.WriteLine($"Dominant: {dominant}");

                    bool correct = Enum.TryParse(dominant, true, out Note result);

                    if (correct)
                        parameters.Dominant = result;
                }
            }

            return parameters;
        }

        private static string GetValue(string arg)
        {
            var array = arg.Split("=");

            return array.Length > 1 ? array[1] : string.Empty;
        }
    }
}
