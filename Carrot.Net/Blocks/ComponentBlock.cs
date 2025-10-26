namespace CarrotNet.Blocks;

/// <summary>
///     A block which renders a dynamically generated block.
/// </summary>
/// <remarks>
///     This class is meant as the base class for blocks which
///     generate their content dynamically via combining other pre-existing blocks.
/// </remarks>
public abstract class ComponentBlock : IBlock
{
    /// <summary>
    ///     Generates the rendered block.
    /// </summary>
    /// <returns>the generated block.</returns>
    protected abstract IBlock Generate();
    
    /// <inheritdoc />
    public void Render(IForm targetForm, IStyle style)
    {
        this.Generate().Render(targetForm, style);
    }

    /// <inheritdoc />
    public (int Width, int Height) CalculateExtent(TargetInfo target, IStyle style)
    {
        return this.Generate().CalculateExtent(target, style);
    }
}
