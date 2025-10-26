namespace CarrotNet.Blocks;

/// <summary>
///     A block representing some underlined content with a psotion marked by a caret.
/// </summary>
/// <param name="underlinedBlock">The underlined block.</param>
/// <param name="position">The position of the caret marker.</param>
public sealed class CaretUnderlineBlock(IBlock underlinedBlock, int position) : IBlock
{
    /// <inheritdoc />
    public void Render(IForm targetForm, IStyle style)
    {
        var (underlinedBlockWidth, underlinedBlockHeight) = underlinedBlock.CalculateExtent(targetForm.Target, style);

        underlinedBlock.Render(targetForm, style);

        for (int i = 0; i < underlinedBlockWidth; ++i)
        {
            if (i == position)
            {
                targetForm.SetGlyph('^',underlinedBlockHeight, i);
            }
            else
            {
                targetForm.SetGlyph('~', underlinedBlockHeight, i);
            }
        }
    }

    /// <inheritdoc />
    public (int Width, int Height) CalculateExtent(TargetInfo target, IStyle style)
    {
        var (underlinedBlockWidth, underlinedBlockHeight) = underlinedBlock.CalculateExtent(target, style);
        
        return (underlinedBlockWidth, underlinedBlockHeight + 1);
    }
}
