namespace CarrotNet;

/// <summary>
///     A single glyph.
/// </summary>
/// <remarks>
///     A glyph in Carrot's notation is a single graphical unit, or in Unicode notation a grapheme,
///     in addition to additional, optional, formating annocations, like a foreground or background color.
///
///     In a monospaced display, a glyph should always occupy a single position in the resulting grid.
/// </remarks>
/// <param name="content">The content of the glyph in form of a Unicode grapheme.</param>
public readonly struct Glyph(string content)
{
    /// <summary>
    ///     The content of the glyph in form of a Unicode grapheme.
    /// </summary>
    public string Content { get; } = content;

    /// <summary>
    ///     Creates a new whitespace glyph.
    /// </summary>
    public Glyph()
    : this(' ')
    {
    }
    
    /// <summary>
    ///     Initializes a new glyph from a single UTF-16 code unit.
    /// </summary>
    /// <param name="codeUnit">The code unit.</param>
    public Glyph(char codeUnit)
    : this(new string(codeUnit, 1))
    {
    }
}
