namespace CarrotNet;

/// <summary>
///     A style.
/// </summary>
public interface IStyle
{
    /// <summary>
    ///     Gets the specified attribute for the given element id, id and tags of block, if it is set.
    /// </summary>
    /// <param name="elementId">The element id of the block.</param>
    /// <param name="id">The id of the block if it is known, null otherwise.</param>
    /// <param name="tags">The tags of the block.</param>
    /// <param name="attributeName">The name of the queried attribute.</param>
    /// <returns>the value of the attribute if set, null otherwise.</returns>
    public object? GetAttribute(string elementId, string? id, IList<string> tags, string attributeName);
}

/// <summary>
///     A user-defined style.
/// </summary>
/// <remarks>
///     Rules are checked in the order they were added.
/// 
///     If none of the specified rules matches, the base style is used.
/// </remarks>
/// <param name="baseStyle">The base style, if any.</param>
public sealed class UserDefinedStyle(IStyle? baseStyle = null) : IStyle
{
    /// <inheritdoc />
    public object? GetAttribute(string elementId, string? id, IList<string> tags, string attributeName)
    {
        StyleRule? selectedRule = this.rules
                                      .FirstOrDefault(rule => rule.CheckMatch(elementId, id, tags));

        if (selectedRule == null)
        {
            return this.baseStyle?.GetAttribute(elementId, id, tags, attributeName);
        }
        
        return selectedRule.Attributes.GetValueOrDefault(attributeName);
    }

    /// <summary>
    ///     Adds a new rule to this style.
    /// </summary>
    /// <param name="rule">The rule.</param>
    /// <returns>this style.</returns>
    public UserDefinedStyle AddRule(StyleRule rule)
    {
        this.rules.Add(rule);

        return this;
    }
    
    /// <summary>
    ///     Adds a new rule to this style.
    /// </summary>
    /// <param name="blockSelector">The selector for the block type, if any.</param>
    /// <param name="idSelector">The selector for the id, if any.</param>
    /// <param name="tagSelector">The selector for the tags, if any.</param>
    /// <param name="attributes">The attributes set by the rule, if any.</param>
    /// <returns>this style.</returns>
    public UserDefinedStyle AddRule(string? blockSelector = null, string? idSelector = null, string? tagSelector = null, IDictionary<string, object>? attributes = null)
    {
        return this.AddRule(new StyleRule(blockSelector, idSelector, tagSelector, attributes ?? new Dictionary<string, object>()));
    }
    
    /// <summary>
    ///     The base style, if any.
    /// </summary>
    private readonly IStyle? baseStyle = baseStyle;

    /// <summary>
    ///     The ordered list of rules.
    /// </summary>
    private readonly List<StyleRule> rules = [];
}

/// <summary>
///     Some set of utility functions for working with styles.
/// </summary>
public static class Style
{
    /// <summary>
    ///     Gets the default style.
    /// </summary>
    /// <returns>the default style.</returns>
    public static IStyle GetDefault()
    {
        const short hightestColorValue = 255;
        const short lowestColorValue   = 0;

        const int defaultIndent = 4;

        UserDefinedStyle s = new();

        StyleRule defaultRule = new(null,
                                    null,
                                    null,
                                    new Dictionary<string, object>
                                    {
                                        ["color"]            = Color.Default,
                                        ["background-color"] = Color.Default,
                                        ["bold"]             = false,
                                        ["indent"]           = defaultIndent,
                                        ["content"]          = string.Empty,
                                    });

        s.AddRule(defaultRule);

        StyleRule checkBoxListRule = new("checkbox-list",
                                         null,
                                         null,
                                         new Dictionary<string, object>
                                         {
                                             ["symbol.color"] =
                                                 new RgbColor(lowestColorValue, hightestColorValue, lowestColorValue),
                                             ["symbol"] = "x",
                                         });

        s.AddRule(checkBoxListRule);

        StyleRule caretUnderlineRule = new("caret-underline",
                                           null,
                                           null,
                                           new Dictionary<string, object>
                                           {
                                               ["caret.color"] =
                                                   new RgbColor(lowestColorValue,
                                                                hightestColorValue,
                                                                lowestColorValue),
                                               ["caret.bold"] = false,
                                           });

        s.AddRule(caretUnderlineRule);

        StyleRule boldRule = new(null,
                                 null,
                                 "bold",
                                 new Dictionary<string, object> { ["bold"] = true, });
        
        s.AddRule(boldRule);

        return s;
    }
    
    /// <summary>
    ///     Gets the specified typed attribute for the given element id, id and tags of block, if it is set.
    /// </summary>
    /// <param name="style">The style.</param>
    /// <param name="blockName">The name of the block type.</param>
    /// <param name="id">The id of the block if it is known, null otherwise.</param>
    /// <param name="tags">The tags of the block.</param>
    /// <param name="attributeName">The name of the queried attribute.</param>
    /// <returns>the value of the attribute if set, null otherwise.</returns>
    /// <typeparam name="T">The type of the attribute.</typeparam>
    /// <returns>the value of the attribute if set, null otherwise.</returns>
    /// <exception cref="Exception">if the specified type and the type of the actual attribute value do not match.</exception>
    public static T? GetAttribute<T>(this IStyle style, string blockName, string? id, IList<string> tags, string attributeName) where T : class
    {
        object? attributeValue = style.GetAttribute(blockName, id, tags, attributeName);

        if (attributeValue == null)
        {
            return null;
        }
        
        if (attributeValue is T typedValue)
        {
            return typedValue;
        }
        
        throw new Exception($"The attribute {attributeName} is not of the expected type {typeof(T)}");
    }
    
    /// <summary>
    ///     Gets the specified typed attribute for the given element id, id and tags of block, if it is set.
    /// </summary>
    /// <param name="style">The style.</param>
    /// <param name="blockName">The name of the block type.</param>
    /// <param name="id">The id of the block if it is known, null otherwise.</param>
    /// <param name="tags">The tags of the block.</param>
    /// <param name="attributeName">The name of the queried attribute.</param>
    /// <returns>the value of the attribute if set, null otherwise.</returns>
    /// <typeparam name="T">The type of the attribute.</typeparam>
    /// <returns>the value of the attribute if set, null otherwise.</returns>
    /// <exception cref="Exception">if the specified type and the type of the actual attribute value do not match.</exception>
    public static T? GetValueAttribute<T>(this IStyle style, string blockName, string? id, IList<string> tags, string attributeName) where T : struct
    {
        object? attributeValue = style.GetAttribute(blockName, id, tags, attributeName);

        if (attributeValue == null)
        {
            return null;
        }
        
        if (attributeValue is T typedValue)
        {
            return typedValue;
        }
        
        throw new Exception($"The attribute {attributeName} is not of the expected type {typeof(T)}");
    }
}
