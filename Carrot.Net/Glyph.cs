namespace Carrot.Net;

public readonly struct Glyph(string content = " ")
{
    public string Content { get; } = content;

    public Glyph(char character)
    : this(new string(character, 1))
    {
    }
}