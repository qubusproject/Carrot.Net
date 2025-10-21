namespace CarrotNet.Blocks;

/// <summary>
///     A block representing some underlined content with a psotion marked by a caret.
/// </summary>
/// <param name="underlinedBlock">The underlined block.</param>
/// <param name="position">The position of the caret marker.</param>
public sealed class CaretUnderlineBlock(IBlock underlinedBlock, int position) : IBlock
{
    /// <inheritdoc />
    public void Render(IForm targetForm)
    {
        var (underlinedBlockWidth, underlinedBlockHeight) = underlinedBlock.CalculateExtent();

        underlinedBlock.Render(targetForm);

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
    public (int Width, int Height) CalculateExtent()
    {
        var (underlinedBlockWidth, underlinedBlockHeight) = underlinedBlock.CalculateExtent();
        
        return (underlinedBlockWidth, underlinedBlockHeight + 1);
    }
}
