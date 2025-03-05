namespace Akov.DataGenerator.Core.Constants;

/// <summary>
/// Defines a set of rules and constraints for value generation.
/// </summary>
public static class ValueRules
{
    /// <summary>
    /// Represents the minimum allowed numeric value.
    /// </summary>
    public const string MinValue = nameof(MinValue);

    /// <summary>
    /// Represents the maximum allowed numeric value.
    /// </summary>
    public const string MaxValue = nameof(MaxValue);

    /// <summary>
    /// Represents the minimum allowed date value.
    /// </summary>
    public const string MinDateValue = nameof(MinDateValue);

    /// <summary>
    /// Represents the maximum allowed date value.
    /// </summary>
    public const string MaxDateValue = nameof(MaxDateValue);

    /// <summary>
    /// Represents the minimum allowed length for a value.
    /// </summary>
    public const string MinLength = nameof(MinLength);

    /// <summary>
    /// Represents the maximum allowed length for a value.
    /// </summary>
    public const string MaxLength = nameof(MaxLength);

    /// <summary>
    /// Represents a phone number formatting mask. Uses # as a placeholder.
    /// </summary>
    public const string PhoneMask = nameof(PhoneMask);

    /// <summary>
    /// Represents a template used for string value generation.
    /// </summary>
    public const string Template = nameof(Template);

    /// <summary>
    /// Represents a rule used along with collections.
    /// </summary>
    public const string Set = nameof(Set);
}
