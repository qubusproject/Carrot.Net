namespace CarrotNet.Blocks;

/// <summary>
///     A block representing a grid of sub-blocks.
/// </summary>
/// <param name="rows">The number of rows.</param>
/// <param name="columns">The number of columns.</param>
public sealed class GridBlock(int rows, int columns) : IBlock
{
    /// <summary>7
    ///     The block at the given position.
    /// </summary>
    /// <param name="row">The row index.</param>
    /// <param name="column">The column index.</param>
    public IBlock this[int row, int column]
    {
        get => this.grid[row, column];
        set => this.grid[row, column] = value;
    }
    
    /// <inheritdoc />
    public void Render(IForm targetForm, IStyle style)
    {
        var (rowHeights, columnWidths) = this.computeLayout(targetForm.Target, style);

        int rowOffset = 0;

        for (int row = 0; row < this.grid.GetLength(0); ++row)
        {
            int columnOffset = 0;

            for (int column = 0; column < this.grid.GetLength(1); ++column)
            {
                FormView view = new (targetForm, rowOffset, columnOffset);

                this.grid[row, column].Render(view, style);

                columnOffset += columnWidths[column];
            }

            rowOffset += rowHeights[row];
        }
    }

    /// <inheritdoc />
    public (int Width, int Height) CalculateExtent(TargetInfo target, IStyle style)
    {
        var (rowHeights, columnWidths) = this.computeLayout(target, style);

        int height = rowHeights.Sum();
        int width  = columnWidths.Sum();

        return (width, height);
    }

    /// <summary>
    ///     Computes the layout of the grid.
    /// </summary>
    /// <returns>the heights of each glyph row and the width of each glyph column.</returns>
    private (List<int> RowHeights, List<int> ColumnWidths) computeLayout(TargetInfo target, IStyle style)
    {
        List<int> rowHeights   = Enumerable.Repeat(0, this.grid.GetLength(0)).ToList();
        List<int> columnWidths = Enumerable.Repeat(0, this.grid.GetLength(1)).ToList();

        for (int row = 0; row < this.grid.GetLength(0); ++row)
        {
            for (int column = 0; column < this.grid.GetLength(1); ++column)
            {
                var (width, height) = this.grid[row, column].CalculateExtent(target, style);

                rowHeights[row]      = Math.Max(rowHeights[row], height);
                columnWidths[column] = Math.Max(columnWidths[column], width);
            }
        }

        return (rowHeights, columnWidths);
    }

    /// <summary>
    ///     The grid of blocks.
    /// </summary>
    private readonly IBlock[,] grid = new IBlock[rows, columns];
}
