namespace Carrot.Net;

public sealed class IndentedForm(int indentationLevel, IForm originalForm) : IForm
{
    public void SetGlyph(Glyph glyph, int row, int column)
    {
        originalForm.SetGlyph(glyph, row, column + indentationLevel);
    }
}
