using GuitarAssistant.Cli;
using GuitarAssistant.ScalesEngine.Enums;

namespace GuitarAssistant.Tests;

public class CommandParserTests
{
    [Fact]
    public void Parse_ValidArguments_ReturnsParameters()
    {
        var parameters = CommandParser.Parse(new[] { "-scale=Dorian", "-root=Fis" });

        Assert.Equal(new Parameters(Scale.Dorian, Note.Fis), parameters);
    }

    [Fact]
    public void Parse_ArgumentOrderDoesNotMatter()
    {
        var parameters = CommandParser.Parse(new[] { "-root=A", "-scale=Aeolian" });

        Assert.Equal(new Parameters(Scale.Aeolian, Note.A), parameters);
    }

    [Theory]
    [InlineData("-scale=lydian", "-root=gis")]
    [InlineData("-SCALE=LYDIAN", "-ROOT=GIS")]
    public void Parse_IsCaseInsensitive(string scaleArg, string rootArg)
    {
        var parameters = CommandParser.Parse(new[] { scaleArg, rootArg });

        Assert.Equal(new Parameters(Scale.Lydian, Note.Gis), parameters);
    }

    [Theory]
    [InlineData("-scale=9", "-root=C", "'9' is not a valid scale")]
    [InlineData("-scale=foo", "-root=C", "'foo' is not a valid scale")]
    [InlineData("-scale=", "-root=C", "'' is not a valid scale")]
    [InlineData("-scale=Ionian", "-root=40", "'40' is not a valid root note")]
    [InlineData("-scale=Ionian", "-root=0", "'0' is not a valid root note")]
    [InlineData("-scale=Ionian", "-root=C,D", "'C,D' is not a valid root note")]
    [InlineData("-scale=Ionian", "-root=X", "'X' is not a valid root note")]
    public void Parse_InvalidValue_Throws(string scaleArg, string rootArg, string expectedMessage)
    {
        var ex = Assert.Throws<ArgumentException>(() => CommandParser.Parse(new[] { scaleArg, rootArg }));

        Assert.Contains(expectedMessage, ex.Message);
    }

    [Theory]
    [InlineData(new[] { "-root=C" }, "Missing required argument -scale")]
    [InlineData(new[] { "-scale=Ionian" }, "Missing required argument -root")]
    [InlineData(new[] { "-scale=Ionian", "-scale=Dorian" }, "-scale was given more than once")]
    [InlineData(new[] { "-root=C", "-root=D", "-scale=Ionian" }, "-root was given more than once")]
    [InlineData(new[] { "-scale=Ionian", "-root=C", "-foo" }, "Unknown argument '-foo'")]
    [InlineData(new[] { "-dominant=C", "-scale=Ionian" }, "Unknown argument '-dominant=C'")]
    [InlineData(new[] { "scale=Ionian", "-root=C" }, "Unknown argument 'scale=Ionian'")]
    public void Parse_InvalidArgumentSet_Throws(string[] args, string expectedMessage)
    {
        var ex = Assert.Throws<ArgumentException>(() => CommandParser.Parse(args));

        Assert.Contains(expectedMessage, ex.Message);
    }
}
