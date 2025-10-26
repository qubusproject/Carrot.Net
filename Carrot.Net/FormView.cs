namespace CarrotNet;

/// <summary>
///     A form representing a portion of another form.
/// </summary>
/// <param name="baseForm">The original base form.</param>
/// <param name="rowOffset">The row index of the visible portion's top-left corner.</param>
/// <param name="columnOffset">The column index of the visible portion's top-left corner.</param>
internal sealed class FormView(IForm baseForm, int rowOffset, int columnOffset) : IForm
{
    /// <inheritdoc />
    public void SetGlyph(Glyph glyph, int row, int column)
    {
        baseForm.SetGlyph(glyph, row + rowOffset, column + columnOffset);
    }
    
    /// <inheritdoc />
    public TargetInfo Target => baseForm.Target;
}
