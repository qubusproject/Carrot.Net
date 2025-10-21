using TermInfo;

namespace CarrotNet;

/// <summary>
///     Information about a rendering target.
/// </summary>
/// <param name="SupportsColorizedOutput">True if the target supports colorized output.</param>
/// <param name="TabWidth">The width of a single tab.</param>
public sealed record TargetInfo(
    bool SupportsColorizedOutput,
    int  TabWidth)
{
    /// <summary>
    ///     Gets a target compatible with stdout output.
    /// </summary>
    /// <param name="tabWidth">The width of a single tab.</param>
    /// <returns>the target.</returns>
    public static TargetInfo GetStdoutTarget(int tabWidth = 4)
    {
        bool colorizeOutput = hasColorSupport();

        return new TargetInfo(colorizeOutput, tabWidth);
    }

    /// <summary>
    ///     Gets a target compatible with file output.
    /// </summary>
    /// <param name="tabWidth">The width of a single tab.</param>
    /// <returns>the target.</returns>
    public static TargetInfo GetFileTarget(int tabWidth = 4)
    {
        return new TargetInfo(false, tabWidth);
    }

    /// <summary>
    ///     Gets a target that supports colorized output.
    /// </summary>
    /// <param name="tabWidth">The width of a single tab.</param>
    /// <returns>the target.</returns>
    public static TargetInfo GetColorizedTarget(int tabWidth = 4)
    {
        return new TargetInfo(true, tabWidth);
    }

    /// <summary>
    ///     Checks if the current terminal supports colorized output.
    /// </summary>
    /// <returns>true if colorized output is supported, false otherwise.</returns>
    private static bool hasColorSupport()
    {
        TermInfoDesc? termDesc = TermInfoDesc.Load();

        if (termDesc is null)
        {
            return false;
        }

        return termDesc.MaxColors > 0;
    }
}
