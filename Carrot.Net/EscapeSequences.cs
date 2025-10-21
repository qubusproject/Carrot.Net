namespace CarrotNet;

/// <summary>
///     A color escape sequence.
/// </summary>
/// <param name="ForegroundColor">The numerical code for the foreground color.</param>
/// <param name="BackgroundColor">The numerical code for the background color.</param>
public sealed record ColorEscapeSequence(int ForegroundColor, int BackgroundColor)
{
    /// <summary>
    ///     Gets the escape sequence for the given color.
    /// </summary>
    /// <param name="foregroundColor">The foreground color.</param>
    /// <param name="backgroundColor">The background color.</param>
    /// <param name="cmap">The color map used to map the specified colors to supported values.</param>
    /// <returns>the color escape sequence.</returns>
    public static ColorEscapeSequence GetEscapeSequenceForColor(IColor foregroundColor, IColor backgroundColor, ColorMap cmap)
    {
        int escapeSequenceForForegrundColor = getEscapeCodeForForegroundColor(foregroundColor, cmap);
        
        int escapeSequenceForBackgroundColor = getEscapeCodeForBackgroundColor(backgroundColor, cmap);
        
        return new ColorEscapeSequence(escapeSequenceForForegrundColor, escapeSequenceForBackgroundColor);
    }

    /// <summary>
    ///     Gets the escape code for the given foreground color.
    /// </summary>
    /// <remarks>
    ///     The provided color will be mapped as close as possible to a supported color.
    /// </remarks>
    /// <param name="foregroundColor">The foreground color.</param>
    /// <param name="cmap">The color map used to map the specified color to supported values.</param>
    /// <returns>the escape code for the foreground color.</returns>
    private static int getEscapeCodeForForegroundColor(IColor foregroundColor, ColorMap cmap)
    {
        if (Color.IsDefaultColor(foregroundColor))
        {
            const int foregroundDefaultColorEscapeSequence = 39;

            return foregroundDefaultColorEscapeSequence;
        }

        int foregroundIndex = cmap.MapColor(foregroundColor);

        const int baseColorBoundary = 8;

        const int baseColorPrefix   = 30;
        const int brightColorPrefix = 90;

        if (foregroundIndex < baseColorBoundary)
        {
            return baseColorPrefix + foregroundIndex;
        }

        return brightColorPrefix + foregroundIndex - baseColorBoundary;
    }
    
    /// <summary>
    ///     Gets the escape code for the given backgroundColor color.
    /// </summary>
    /// <remarks>
    ///     The provided color will be mapped as close as possible to a supported color.
    /// </remarks>
    /// <param name="backgroundColor">The backgroundColor color.</param>
    /// <param name="cmap">The color map used to map the specified color to supported values.</param>
    /// <returns>the escape code for the backgroundColor color.</returns>
    private static int getEscapeCodeForBackgroundColor(IColor backgroundColor, ColorMap cmap)
    {
        if (Color.IsDefaultColor(backgroundColor))
        {
            const int backgroundDefaultColorEscapeSequence = 49;

            return backgroundDefaultColorEscapeSequence;
        }

        int backgroundIndex = cmap.MapColor(backgroundColor);

        const int baseColorBoundary = 8;

        const int baseColorPrefix   = 40;
        const int brightColorPrefix = 100;

        if (backgroundIndex < baseColorBoundary)
        {
            return baseColorPrefix + backgroundIndex;
        }

        return brightColorPrefix + backgroundIndex - baseColorBoundary;;
    }
}

/// <summary>
///     A terminal escape sequence.
/// </summary>
/// <param name="ForegroundColor">The numerical code for the foreground color.</param>
/// <param name="BackgroundColor">The numerical code for the background color.</param>
/// <param name="Formatting">The formating escape sequence.</param>
public sealed record TerminalEscapeSequence(int ForegroundColor = 0, int BackgroundColor = 0, int Formatting = 0)
{
    /// <summary>
    ///     Gets the escape sequence for the given colors and formatting options.
    /// </summary>
    /// <param name="foregroundColor">The foreground color.</param>
    /// <param name="backgroundColor">The background color.</param>
    /// <param name="cmap">The color map used to map the specified colors to supported values.</param>
    /// <param name="bold">Whether the text should be bold.</param>
    /// <returns></returns>
    public static TerminalEscapeSequence GetEscapeSequence(IColor   foregroundColor,
                                                           IColor   backgroundColor,
                                                           ColorMap cmap,
                                                           bool     bold)
    {
        ColorEscapeSequence escapeSequencesForColor = ColorEscapeSequence.GetEscapeSequenceForColor(foregroundColor, backgroundColor, cmap);
        
        int escapeSequenceForFormatting = getFormattingEscapeSequence(bold);
        
        return new TerminalEscapeSequence(escapeSequencesForColor.ForegroundColor, escapeSequencesForColor.BackgroundColor, escapeSequenceForFormatting);
    }

    /// <summary>
    ///     Renders this escape sequence into its string representation.
    /// </summary>
    /// <returns></returns>
    public string RenderEscapeSequence() => $"{csi}{this.ForegroundColor};{this.BackgroundColor}m{csi}{this.Formatting}m";
    
    /// <summary>
    ///     The default escape sequence.
    /// </summary>
    public static string DefaultEscapeSequence => "\e[0m";
    
    /// <summary>
    ///     The CSI prefix.
    /// </summary>
    private static string csi => "\e[";
    
    /// <summary>
    ///     Gets the escape sequence for the given formatting options.
    /// </summary>
    /// <param name="bold">Whether the text should be bold.</param>
    /// <returns>the escape code for the formatting.</returns>
    private static int getFormattingEscapeSequence(bool bold)
    {
        const int defaultEscapeSequence = 22;
        const int boldEscapeSequence    = 1;

        return bold ? boldEscapeSequence : defaultEscapeSequence;
    }
}
