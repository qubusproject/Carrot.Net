namespace CarrotNet.Blocks;

/// <summary>
///    The direction of a line.
/// </summary>
public enum Direction
{
    /// <summary>
    ///     A vertical line from top to bottom.
    /// </summary>
    Down,
    /// <summary>
    ///     A horizontal line from left to right.
    /// </summary>
    Right,
}

/// <summary>
///     A block representing a line of content.
/// </summary>
/// <remarks>
///     The direction of the line can either be left-to-right or top-to-bottom.
/// </remarks>
/// <param name="blocks">The list of blocks to render one by one.</param>
/// <param name="direction">The direction of the rendering.</param>
public sealed class LineBlock(IBlock[] blocks, Direction direction) : IBlock
{
    /// <inheritdoc />
    public void Render(IForm targetForm, IStyle style)
    {
        int offset = 0;

        foreach (IBlock block in blocks)
        {
            var (blockWidth, blockHeight) = block.CalculateExtent(targetForm.Target, style);

            switch (direction)
            {
                default:
                case Direction.Right:
                {
                    FormView view = new(targetForm, 0, offset);
                    block.Render(view, style);

                    offset += blockWidth;
                    break;
                }
                case Direction.Down:
                {
                    FormView view = new(targetForm, offset, 0);
                    block.Render(view, style);

                    offset += blockHeight;
                    break;
                }
            }
        }
    }

    /// <inheritdoc />
    public (int Width, int Height) CalculateExtent(TargetInfo target, IStyle style)
    {
        int width = 0;
        int height = 0;

        foreach (IBlock block in blocks)
        {
            var (blockWidth, blockHeight) = block.CalculateExtent(target, style);

            switch (direction)
            {
                default:
                case Direction.Right:
                    height =  Math.Max(height, blockHeight);
                    width  += blockWidth;
                    break;
                case Direction.Down:
                    height += blockHeight;
                    width  =  Math.Max(width, blockWidth);
                    break;
            }
        }

        return (width, height);
    }
}
