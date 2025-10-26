namespace CarrotNet;

/// <summary>
///     A block of content.
/// </summary>
/// <remarks>
///     A block forms the basic unit of content in Carrot's notation.
///
///     Blocks can often be nested to form more complex content, making them inherently
///     composable.
/// </remarks>
public interface IBlock
{
    /// <summary>
    ///     Renders this block to the given form.
    /// </summary>
    /// <param name="targetForm">The form to which the block is rendered.</param>
    /// <param name="style">The style to be applied to the rendered content.</param>
    public void Render(IForm targetForm, IStyle style);

    /// <summary>
    ///     Calculates the extent of this block in monospace grid units.
    /// </summary>
    /// <param name="target">The target to which this block will be rendered.</param>
    /// <param name="style">The style to be applied to the rendered content.</param>
    /// <returns>the width and height of the extent.</returns>
    public (int Width, int Height) CalculateExtent(TargetInfo target, IStyle style);
}
