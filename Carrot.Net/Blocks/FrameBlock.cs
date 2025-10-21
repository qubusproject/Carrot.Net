namespace CarrotNet.Blocks;

/// <summary>
///     A block representing a piece of content framed by a border.
/// </summary>
/// <param name="framedBlock">The framed block.</param>
/// <param name="marginX">The margin between border and block content in x direction.</param>
/// <param name="marginY">The margin between border and block content in y direction.</param>
public sealed class FrameBlock(IBlock framedBlock, int marginX, int marginY) : IBlock
{
    /// <inheritdoc />
    public void Render(IForm targetForm)
    {
        var (framedBlockWidth, framedBlockHeight) = framedBlock.CalculateExtent();

        for (int i = 0; i < framedBlockWidth + 2 * marginX + 2; ++i)
        {
            targetForm.SetGlyph('-', 0, i);
            targetForm.SetGlyph('-',framedBlockHeight + 2 * marginY + 1, i);
        }

        for (int i = 1; i < framedBlockHeight + 2 * marginY + 1; ++i)
        {
            targetForm.SetGlyph('|', i, 0);
            targetForm.SetGlyph('|', i, framedBlockWidth + 2 * marginX + 1);
        }

        FormView view = new(targetForm, 1 + marginY, 1 + marginX);

        framedBlock.Render(view);
    }

    /// <inheritdoc />
    public (int Width, int Height) CalculateExtent()
    {
        var (framedBlockWidth, framedBlockHeight) = framedBlock.CalculateExtent();

        return (framedBlockWidth + 2 * marginX + 2,  framedBlockHeight + 2 * marginY + 2);
    }
}
