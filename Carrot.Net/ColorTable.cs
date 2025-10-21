namespace CarrotNet;

/// <summary>
///     A table of named colors.
/// </summary>
public sealed class ColorTable
{
    /// <summary>
    ///     Adds a color to the table.
    /// </summary>
    /// <param name="name">The name of the color.</param>
    /// <param name="color">The color to be added under this name.</param>
    public void AddColor(string name, IColor color)
    {
        this.colors.Add(name, color);
    }
    
    /// <summary>
    ///     Tries to look up a color by its name.
    /// </summary>
    /// <param name="name">The color name.</param>
    /// <returns>the actual color if available, null otherwise.</returns>
    public IColor? TryLookUpColor(string name)
    {
        return this.colors.GetValueOrDefault(name);
    }
    
    private readonly Dictionary<string, IColor> colors = new();
}
