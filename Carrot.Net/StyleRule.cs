using System.Collections.Immutable;

namespace CarrotNet;

/// <summary>
///     Creates a new style rule.
/// </summary>
/// <param name="BlockSelector">The selector, if any, for the block type. The 'null' value matches any value.</param>
/// <param name="IdSelector">The selector, if any, for the id. The 'null' value matches any value.</param>
/// <param name="TagSelector">The selector, if any, for the tags. The 'null' value matches any value.</param>
/// <param name="Attributes">The attributes set by this style rule.</param>
public sealed record StyleRule(string? BlockSelector, string? IdSelector, string? TagSelector, ImmutableDictionary<string, object> Attributes)
{
    /// <summary>
    ///     Creates a new style rule.
    /// </summary>
    /// <param name="BlockSelector">The selector, if any, for the block type. The 'null' value matches any value.</param>
    /// <param name="IdSelector">The selector, if any, for the id. The 'null' value matches any value.</param>
    /// <param name="TagSelector">The selector, if any, for the tags. The 'null' value matches any value.</param>
    /// <param name="Attributes">The attributes set by this style rule.</param>
    public StyleRule(string?                     BlockSelector,
                     string?                     IdSelector,
                     string?                     TagSelector,
                     IDictionary<string, object> Attributes)
    : this(BlockSelector, IdSelector, TagSelector, Attributes.ToImmutableDictionary())
    {
    }
    
    /// <summary>
    ///     Adds an attribute to this style rule.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <param name="value">Its value.</param>
    /// <returns></returns>
    public StyleRule AddAttribute(string name, object value)
    {
        ImmutableDictionary<string, object> newAttributes = this.Attributes.Add(name, value);

        return this with { Attributes = newAttributes };
    }

    /// <summary>
    ///     Checks if this style rule matches the specified block type, id and tags of a block.
    /// </summary>
    /// <param name="elementId">The type of the block.</param>
    /// <param name="id">The id of the block if it is known, null otherwise.</param>
    /// <param name="tags">The tags of the block.</param>
    /// <returns></returns>
    public bool CheckMatch(string elementId, string? id, IEnumerable<string> tags)
    {
        bool matchesBlockType = this.BlockSelector is null || elementId == this.BlockSelector;
        
        bool matchesId = this.IdSelector is null || id == this.IdSelector;
        
        bool matchesATag = this.TagSelector is null || tags.Contains(this.TagSelector);
        
        return matchesBlockType && matchesId && matchesATag;
    }
}
