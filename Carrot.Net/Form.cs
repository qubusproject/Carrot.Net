namespace CarrotNet;

/// <summary>
///     A form, representing the rendering target for a block of content.
/// </summary>
public interface IForm
{
    /// <summary>
    ///     Sets the glyph at the given position.
    /// </summary>
    /// <param name="glyph">The new glyph.</param>
    /// <param name="row">The row index.</param>
    /// <param name="column">The column index.</param>
    public void SetGlyph(Glyph glyph, int row, int column);
}

/// <summary>
///     A set of extensions for forms.
/// </summary>
public static class FormExtensions
{
    /// <summary>
    ///     Set the glyph at the given position using a code point as the content.
    /// </summary>
    /// <param name="form">The form.</param>
    /// <param name="codePoint">The code point representing the content.</param>
    /// <param name="row">The row index.</param>
    /// <param name="column">The column index.</param>
    public static void SetGlyph(this IForm form, char codePoint, int row, int column)
    {
        form.SetGlyph(new Glyph(codePoint), row, column);   
    }
}
