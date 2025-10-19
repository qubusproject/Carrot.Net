using System.Globalization;

namespace Carrot.Net.Blocks;

public sealed class TestBlock(string text) : IBlock
{
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
}
