namespace Carrot.Net;

public sealed class IndentedView(int indentationLevel, IView originalView) : IView
{
    public void SetGlyph(Glyph glyph, int row, int column)
    {
        originalView.SetGlyph(glyph, row, column + indentationLevel);
    }
}