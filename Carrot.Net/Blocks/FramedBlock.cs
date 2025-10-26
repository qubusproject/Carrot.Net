namespace CarrotNet.Blocks;

/// <summary>
///     A block representing a piece of content framed by a border.
/// </summary>
/// <param name="framedBlock">The framed block.</param>
public sealed class FramedBlock(IBlock framedBlock) : IBlock
{
    /// <inheritdoc />
    public void Render(IForm targetForm, IStyle style)
    {
        int marginX = style.GetValueAttribute<int>("framed-block", "", [], "margin-x") ?? 0;
        int marginY = style.GetValueAttribute<int>("framed-block", "", [], "margin-y") ?? 0;
        
        var (framedBlockWidth, framedBlockHeight) = framedBlock.CalculateExtent(targetForm.Target, style);

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

        framedBlock.Render(view, style);
    }

    /// <inheritdoc />
    public (int Width, int Height) CalculateExtent(TargetInfo target, IStyle style)
    {
        int marginX = style.GetAttribute("framed-block", "", [], "margin-x") as int? ?? 0;
        int marginY = style.GetAttribute("framed-block", "", [], "margin-y") as int? ?? 0;
        
        var (framedBlockWidth, framedBlockHeight) = framedBlock.CalculateExtent(target, style);

        return (framedBlockWidth + 2 * marginX + 2,  framedBlockHeight + 2 * marginY + 2);
    }
}
