using System.Text;

namespace CarrotNet;

/// <summary>
///     A plain form represented as a grid of glyphs.
/// </summary>
/// <remarks>
///     The grid of glyphs representing the content of the form will expand automatically of positions
///     outside the grid are accessed.
/// </remarks>
public sealed class PlainForm : IForm
{
    /// <summary>
    ///     Creates a new empty form.
    /// </summary>
    /// <param name="initialWidth">The initial width of the form.</param>
    /// <param name="initialHeight">The intial height of the form.</param>
    public PlainForm(int initialWidth, int initialHeight)
    {
        this.glyphs = new Glyph[initialHeight, initialWidth];

        for (int row = 0; row < this.glyphs.GetLength(0); ++row)
        {
            for (int column = 0; column < this.glyphs.GetLength(1); ++column)
            {
                this.glyphs[row, column] = new Glyph();
            }
        }
    }
    
    /// <inheritdoc />
    public void SetGlyph(Glyph glyph, int row, int column)
    {
        this.expandIfNecessary(row, column);
        
        this.glyphs[row, column] = glyph;
    }

    /// <summary>
    ///     Renders the form into a printable string.
    /// </summary>
    /// <param name="renderSparsely">If true, any trailing whitespace in each row will not be rendered.</param>
    /// <returns>the printable string.</returns>
    public string ToPrintableString(bool renderSparsely = true)
    {
        StringBuilder builder = new();

        for (int row = 0; row < this.glyphs.GetLength(0); ++row)
        {
            int rowStop = renderSparsely ? this.determineRowStop(row) : this.glyphs.GetLength(1);
            
            for (int column = 0; column < rowStop; ++column)
            {
                builder.Append(this.glyphs[row, column].Content);
            }
            
            builder.AppendLine();
        }
        
        return builder.ToString();
    }

    /// <summary>
    ///     Determines the column index at which the rendering of the row can be stopped since only whitespace follows.
    /// </summary>
    /// <param name="row">The raw which should be scanned.</param>
    /// <returns>the column index for this row at which the rendering can be stopped.</returns>
    private int determineRowStop(int row)
    {
        for (int column = this.glyphs.GetLength(1) - 1; column >= 0; --column)
        {
            if (this.glyphs[row, column].Content != " ")
            {
                return column + 1;
            }
        }

        return this.glyphs.GetLength(1);
    }

    /// <summary>
    ///     Expands the grid of glyphs to make the specified indices valid.
    /// </summary>
    /// <remarks>
    ///     The grid is expanded to the right and the bottom, and
    ///     the content of the previous grid is copied to the top-left portion of the new grid.
    /// </remarks>
    /// <param name="row">The row index.</param>
    /// <param name="column">The column index.</param>
    private void expandIfNecessary(int row, int column)
    {
        int originalHeight = glyphs.GetLength(0);
        int originalWidth  = glyphs.GetLength(1);
        
        if (row >= originalHeight || column >= originalWidth)
        {
            int newWidth  = Math.Max(originalWidth, column + 1);
            int newHeight = Math.Max(originalHeight, row + 1);
            
            Glyph[,] newBuffer = new Glyph[newHeight, newWidth];

            for (int i = 0; i < newHeight; ++i)
            {
                for (int j = 0; j < newWidth; ++j)
                {
                    newBuffer[i, j] = new Glyph();
                }
            }
            
            for (int i = 0; i < originalWidth; i++)
            {
                for (int j = 0; j < originalHeight; j++)
                {
                    newBuffer[j, i] = this.glyphs[j, i];
                }
            }
            
            this.glyphs = newBuffer;
        }
    }

    /// <summary>
    ///     The grid of glyphs.
    /// </summary>
    private Glyph[,] glyphs;
}
