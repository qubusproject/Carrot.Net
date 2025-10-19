using System.Globalization;

namespace Carrot.Net.Components;

public sealed class TextComponent(string text) : IComponent
{
    public void Render(IView targetView)
    {
        string[] lines = text.Split('\n');

        int currentRow = 0;
        int currentColumn = 0;
        
        foreach (string line in lines)
        {
            TextElementEnumerator enumerator = StringInfo.GetTextElementEnumerator(line);
            
            while (enumerator.MoveNext())
            {
                targetView.SetGlyph(new Glyph(enumerator.GetTextElement()), currentRow, currentColumn);
                
                ++currentColumn;
            }

            ++currentRow;
        }
    }
}