using System.Globalization;

namespace CarrotNet.Blocks;

/// <summary>
///     A block representing a (multi-line) text.
/// </summary>
/// <param name="text">The text represented by the block.</param>
public sealed class TextBlock(string text) : IBlock
{
    /// <inheritdoc />
    public void Render(IForm targetForm)
    {
        string[] lines = text.Split('\n');

        int currentRow = 0;
        int currentColumn = 0;
        
        foreach (string line in lines)
        {
            TextElementEnumerator enumerator = StringInfo.GetTextElementEnumerator(line);
            
            while (enumerator.MoveNext())
            {
                targetForm.SetGlyph(new Glyph(enumerator.GetTextElement()), currentRow, currentColumn);
                
                ++currentColumn;
            }

            ++currentRow;
        }
    }

    /// <inheritdoc />
    public (int Width, int Height) CalculateExtent()
    {
        string[] lines = text.Split('\n');
        
        int maxLineLength = lines.Max(line => line.Length);

        return (maxLineLength, lines.Length);
    }
}
