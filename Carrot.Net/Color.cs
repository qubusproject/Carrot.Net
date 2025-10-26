namespace CarrotNet;

/// <summary>
///     A color representation.
/// </summary>
public interface IColor
{
}

/// <summary>
///     The default color of the output target.
/// </summary>
public sealed record DefaultColor() : IColor;

/// <summary>
///     An RGB color value.
/// </summary>
/// <remarks>
///     All components are in the range [0, 255].
/// </remarks>
/// <param name="Red">The red component.</param>
/// <param name="Green">The green component.</param>
/// <param name="Blue">The blue component.</param>
public sealed record RgbColor(short Red, short Green, short Blue) : IColor;

/// <summary>
///     An HSL color value.
/// </summary>
/// <param name="Hue">The hue component.</param>
/// <param name="Saturation">The saturation component.</param>
/// <param name="Lightness">The lightness component.</param>
public sealed record HslColor(float Hue, float Saturation, float Lightness) : IColor;

/// <summary>
///     A named color.
/// </summary>
/// <param name="Name">The name of the color.</param>
public sealed record NamedColor(string Name) : IColor;

/// <summary>
///     A set of functions to work with colors.
/// </summary>
public static class Color
{
    /// <summary>
    ///     Tests if the given color is the default color.
    /// </summary>
    /// <remarks>
    ///     Note that this only check if the color is an instance of <see cref="DefaultColor"/>
    ///     and not if the specified color corresponds to the default color of the output target.
    /// </remarks>
    /// <param name="color">The color to be tested.</param>
    /// <returns>true of the provided color is the default color, false otherwise.</returns>
    public static bool IsDefaultColor(IColor color)
    {
        return color switch
               {
                    DefaultColor => true,
                    _            => false,
               };
    }
    
    /// <summary>
    ///     Converts an RGB color value to an HSL color value.
    /// </summary>
    /// <param name="color">The RGB color value.</param>
    /// <returns>The HSL color value.</returns>
    /// <exception cref="Exception">if the conversion would not be valid.</exception>
    public static HslColor Rgb2Hsl(RgbColor color)
    {
        const short rgbComponentMaximum = 255;

        if (color.Red is < 0 or > rgbComponentMaximum)
        {
            throw new Exception($"Invalid red component. The value {color.Red} is not in the valid range [0, {rgbComponentMaximum}]");
        }
        
        if (color.Green is < 0 or > rgbComponentMaximum)
        {
            throw new Exception($"Invalid red component. The value {color.Red} is not in the valid range [0, {rgbComponentMaximum}]");
        }
        
        if (color.Blue is < 0 or > rgbComponentMaximum)
        {
            throw new Exception($"Invalid red component. The value {color.Red} is not in the valid range [0, {rgbComponentMaximum}]");
        }
        
        float r = color.Red / rgbComponentMaximum;
        float g = color.Green / rgbComponentMaximum;
        float b = color.Blue / rgbComponentMaximum;

        float cMin = new[] { r, b, g }.Min();
        float cMax = new[] { r, b, g }.Max();

        float delta = cMax - cMin;

        float L = (cMax + cMin) / 2;

        float S = delta == 0 ? 0 : delta / (1 - Math.Abs(2 * L - 1));

        const short degreeMaximum = 360;

        const short numberOfHexagonEdges = 6;
        const short angularShiftPerEdge  = 60;
        
        float calculateHue()
        {
            if (delta == 0)
                return 0.0f;

            if (cMax == r)
            {
                float result = 
                    (((g - b) / delta) % numberOfHexagonEdges) * angularShiftPerEdge;

                if (b > g)
                {
                    result += degreeMaximum;
                }

                return result;
            }

            if (cMax == g)
            {
                return ((b - r) / delta + 2) * angularShiftPerEdge;
            }

            return ((r - g) / delta + 4) * angularShiftPerEdge;
        }

        float H = calculateHue();

        return new HslColor(H, S, L);
    }
    
    /// <summary>
    ///     Converts a color value to the closest matching HSL color value.
    /// </summary>
    /// <param name="color">The color value to map.</param>
    /// <returns>the mapped HSL color value.</returns>
    /// <exception cref="Exception">if the conversion would not be valid.</exception>
    public static HslColor Hsl(IColor color)
    {
        return color switch
               {
                    DefaultColor => throw new Exception("Trying to use the default color as a concrete color"),
                    HslColor hsl => hsl,
                    RgbColor rgb => Rgb2Hsl(rgb),
                    NamedColor   => throw new Exception("Unknown named color"),
                    _            => throw new Exception("Unknown color type"),
               };
    }
    
    /// <summary>
    ///     Calculates the distance between two colors.
    /// </summary>
    /// <param name="color1">The first color.</param>
    /// <param name="color2">The second color.</param>
    /// <returns>the distance between the two colors.</returns>
    public static float Distance(IColor color1, IColor color2)
    {
        HslColor color1Hsl = Hsl(color1);
        HslColor color2Hsl = Hsl(color2);

        const float degreeMaximum = 360;

        return Math.Abs(color1Hsl.Hue / degreeMaximum - color2Hsl.Hue / degreeMaximum) +
               Math.Abs(color1Hsl.Saturation - color2Hsl.Saturation) +
               Math.Abs(color1Hsl.Lightness - color2Hsl.Lightness);
    }

    /// <summary>
    ///     The default color.
    /// </summary>
    public static DefaultColor Default => new();

    /// <summary>
    ///     The xterm color table.
    /// </summary>
    public static readonly HslColor[] XTermColorTable =
    [
        new(0.00000000f, 0.0f, 0.0f), new(0.00000000f, 1.0f, 0.25f), new(120.00000000f, 1.0f, 0.25f),
        new(60.00000000f, 1.0f, 0.25f),   new(240.00000000f, 1.0f, 0.25f),  new(300.00000000f, 1.0f, 0.25f),
        new(180.00000000f, 1.0f, 0.25f),  new(0.00000000f, 0.0f, 0.75f),    new(0.00000000f, 0.0f, 0.5f),
        new(0.00000000f, 1.0f, 0.5f),     new(120.00000000f, 1.0f, 0.5f),   new(60.00000000f, 1.0f, 0.5f),
        new(240.00000000f, 1.0f, 0.5f),   new(300.00000000f, 1.0f, 0.5f),   new(180.00000000f, 1.0f, 0.5f),
        new(0.00000000f, 0.0f, 1.0f),     new(0.00000000f, 0.0f, 0.0f),     new(240.00000000f, 1.0f, 0.18f),
    ];
}
