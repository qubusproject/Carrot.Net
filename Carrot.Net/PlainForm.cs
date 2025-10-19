using System.Text;

namespace Carrot.Net;

public sealed class PlainForm : IForm
{
    public PlainForm(int initialWidth, int initialHeight)
    {
        this.glyphs = new Glyph[initialWidth, initialHeight];
    }
    
    public void SetGlyph(Glyph glyph, int row, int column)
    {
        this.expandIfNecessary(row, column);
        
        this.glyphs[row, column] = glyph;
    }

    public string ToPrintableString()
    {
        StringBuilder builder = new();

        for (int row = 0; row < this.glyphs.GetLength(0); ++row)
        {
            for (int column = 0; column < this.glyphs.GetLength(1); ++column)
            {
                builder.Append(this.glyphs[row, column].Content);
            }
            
            builder.AppendLine();
        }
        
        return builder.ToString();
    }

    private void expandIfNecessary(int row, int column)
    {
        int originalWidth = glyphs.GetLength(0);
        int originalHeight = glyphs.GetLength(1);
        
        if (row >= originalWidth || column >= originalHeight)
        {
            int newWidth = Math.Max(originalWidth, row);
            int newHeight = Math.Max(originalHeight, column);
            
            Glyph[,] newBuffer = new Glyph[newWidth, newHeight];

            for (int i = 0; i < originalWidth; i++)
            {
                for (int j = 0; j < originalHeight; j++)
                {
                    newBuffer[i, j] = this.glyphs[i, j];
                }
            }
            
            this.glyphs = newBuffer;
        }
    }

    private Glyph[,] glyphs;
}