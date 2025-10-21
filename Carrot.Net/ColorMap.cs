namespace CarrotNet;

/// <summary>
///     A color map, used to map a color to the closest supported one.
/// </summary>
/// <param name="AvailableColors">All supported colors.</param>
public sealed record ColorMap(IList<HslColor> AvailableColors)
{
    /// <summary>
    ///     Maps the given color to the closest supported color.
    /// </summary>
    /// <param name="color">The color which should be mapped.</param>
    /// <returns>the index of the mapped color.</returns>
    public int MapColor(IColor color)
    {
        HslColor colorHsl = Color.Hsl(color);
        
        int bestColorIndex    = 0;
        float bestColorDistance = Color.Distance(colorHsl, this.AvailableColors.First());
        
        foreach (var (index, value) in this.AvailableColors.Select((value, index) => (index, value)))
        {
            float dist = Color.Distance(colorHsl, value);

            if (dist < bestColorDistance)
            {
                bestColorIndex    = index;
                bestColorDistance = dist;
            }
        }

        return bestColorIndex;
    }
}
