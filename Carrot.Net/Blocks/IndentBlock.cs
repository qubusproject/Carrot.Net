namespace CarrotNet.Blocks;

/// <summary>
///     A block representing some indented content.
/// </summary>
/// <param name="indentationLevel">The level of indentation.</param>
/// <param name="indentedBlock">The indented block.</param>
public sealed class IndentBlock(int indentationLevel, IBlock indentedBlock) : IBlock
{
    /// <inheritdoc />
    public void Render(IForm targetForm)
    {
        indentedBlock.Render(new FormView(targetForm, 0, indentationLevel));
    }
    
    /// <inheritdoc />
    public (int Width, int Height) CalculateExtent()
    {
        var (indentedBlockWidth, indentedBlockHeight) = indentedBlock.CalculateExtent();
        
        return (indentedBlockWidth + indentationLevel, indentedBlockHeight);
    }
}
