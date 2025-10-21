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
/// <param name="foregroundColor">The foreground color.</param>
/// <param name="backgroundColor">The background color.</param>
/// <param name="bold">Whether the glyph should be rendered in bold.</param>
public readonly struct Glyph(string content,
                             IColor foregroundColor,
                             IColor backgroundColor,
                             bool bold)
{
    /// <summary>
    ///     The content of the glyph in form of a Unicode grapheme.
    /// </summary>
    public string Content { get; } = content;
    
    /// <summary>
    ///     The foreground color.
    /// </summary>
    public IColor ForegroundColor { get; } = foregroundColor;
    
    /// <summary>
    ///     The background color.
    /// </summary>
    public IColor BackgroundColor { get; } = backgroundColor;
    
    /// <summary>
    ///     Whether the glyph should be rendered in bold.
    /// </summary>
    public bool Bold { get; } = bold;

    /// <summary>
    ///     Creates a new whitespace glyph.
    /// </summary>
    public Glyph()
    : this(' ')
    {
    }
    
    /// <summary>
    ///     Initializes a new glyph.
    /// </summary>
    /// <param name="content">The content of the glyph in form of a Unicode grapheme.</param>
    public Glyph(string content)
        : this(content, new DefaultColor(), new DefaultColor(), false)
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
