using Akov.DataGenerator.Core.Constants;

namespace Akov.DataGenerator.FluentSyntax;

/// <summary>
/// Extension methods for the <see cref="PropertyBuilder{T}"/> class, providing additional rules and configurations 
/// for property generation, such as setting ranges, lengths, phone masks, and templates.
/// </summary>
public static class PropertyBuilderExtensions 
{
    /// <summary>
    /// Specifies a maximum length for a collection property.
    /// </summary>
    /// <typeparam name="T">The type that owns the property being configured.</typeparam>
    /// <param name="propertyBuilder">The <see cref="PropertyBuilder{T}"/> instance for the property.</param>
    /// <param name="max">The maximum length for a collection property.</param>
    /// <returns>The current <see cref="PropertyBuilder{T}"/> instance for method chaining.</returns>
    public static PropertyBuilder<T> Count<T>(this PropertyBuilder<T> propertyBuilder, int max)
        => Range(propertyBuilder, max: max);

    /// <summary>
    /// Specifies a range of lengths for a collection property.
    /// </summary>
    /// <typeparam name="T">The type that owns the property being configured.</typeparam>
    /// <param name="propertyBuilder">The <see cref="PropertyBuilder{T}"/> instance for the property.</param>
    /// <param name="min">The minimum length value for a collection property.</param>
    /// <param name="max">The maximum length value for a collection property.</param>
    /// <returns>The current <see cref="PropertyBuilder{T}"/> instance for method chaining.</returns>
    public static PropertyBuilder<T> Count<T>(this PropertyBuilder<T> propertyBuilder, int min, int max)
        => Range(propertyBuilder, min, max);

    /// <summary>
    /// Specifies the maximum length for a string property.
    /// </summary>
    /// <typeparam name="T">The type that owns the property being configured.</typeparam>
    /// <param name="propertyBuilder">The <see cref="PropertyBuilder{T}"/> instance for the property.</param>
    /// <param name="max">The maximum length for a string property.</param>
    /// <returns>The current <see cref="PropertyBuilder{T}"/> instance for method chaining.</returns>
    public static PropertyBuilder<T> Length<T>(this PropertyBuilder<T> propertyBuilder, int max)
        => Length(propertyBuilder, min: 1, max: max);

    /// <summary>
    /// Specifies a length range for a string property.
    /// </summary>
    /// <typeparam name="T">The type that owns the property being configured.</typeparam>
    /// <param name="propertyBuilder">The <see cref="PropertyBuilder{T}"/> instance for the property.</param>
    /// <param name="min">The minimum length for a string property.</param>
    /// <param name="max">The maximum length for a string property.</param>
    /// <returns>The current <see cref="PropertyBuilder{T}"/> instance for method chaining.</returns>
    public static PropertyBuilder<T> Length<T>(this PropertyBuilder<T> propertyBuilder, int min, int max)
    {
        propertyBuilder.ValueRule(ValueRules.MinLength, min);
        propertyBuilder.ValueRule(ValueRules.MaxLength, max);
        return propertyBuilder;
    }

    /// <summary>
    /// Specifies the maximum range for the property.
    /// </summary>
    /// <typeparam name="T">The type that owns the property being configured.</typeparam>
    /// <param name="propertyBuilder">The <see cref="PropertyBuilder{T}"/> instance for the property.</param>
    /// <param name="max">The maximum range for the property.</param>
    /// <returns>The current <see cref="PropertyBuilder{T}"/> instance for method chaining.</returns>
    public static PropertyBuilder<T> Range<T>(this PropertyBuilder<T> propertyBuilder, double max)
        => Range(propertyBuilder, min: 0, max: max);

    /// <summary>
    /// Specifies a range for the property.
    /// </summary>
    /// <typeparam name="T">The type that owns the property being configured.</typeparam>
    /// <param name="propertyBuilder">The <see cref="PropertyBuilder{T}"/> instance for the property.</param>
    /// <param name="min">The minimum value for the range.</param>
    /// <param name="max">The maximum value for the range.</param>
    /// <returns>The current <see cref="PropertyBuilder{T}"/> instance for method chaining.</returns>
    public static PropertyBuilder<T> Range<T>(this PropertyBuilder<T> propertyBuilder, double min, double max)
    {
        propertyBuilder.ValueRule(ValueRules.MinValue, min);
        propertyBuilder.ValueRule(ValueRules.MaxValue, max + 1);
        return propertyBuilder;
    }

    /// <summary>
    /// Specifies a date range for the property.
    /// </summary>
    /// <typeparam name="T">The type that owns the property being configured.</typeparam>
    /// <param name="propertyBuilder">The <see cref="PropertyBuilder{T}"/> instance for the property.</param>
    /// <param name="min">The minimum date value for the range.</param>
    /// <param name="max">The maximum date value for the range.</param>
    /// <returns>The current <see cref="PropertyBuilder{T}"/> instance for method chaining.</returns>
    public static PropertyBuilder<T> Range<T>(this PropertyBuilder<T> propertyBuilder, DateTime min, DateTime max)
    {
        propertyBuilder.ValueRule(ValueRules.MinDateValue, min);
        propertyBuilder.ValueRule(ValueRules.MaxDateValue, max);
        return propertyBuilder;
    }

    /// <summary>
    /// Specifies a phone mask for the property, using # as a placeholder.
    /// </summary>
    /// <typeparam name="T">The type that owns the property being configured.</typeparam>
    /// <param name="propertyBuilder">The <see cref="PropertyBuilder{T}"/> instance for the property.</param>
    /// <param name="mask">The phone mask format for the property.</param>
    /// <returns>The current <see cref="PropertyBuilder{T}"/> instance for method chaining.</returns>
    public static PropertyBuilder<T> PhoneMask<T>(this PropertyBuilder<T> propertyBuilder, string mask)
    {
        propertyBuilder.ValueRule(ValueRules.PhoneMask, mask);
        return propertyBuilder;
    }

    /// <summary>
    /// Specifies a template format for a string property.
    /// </summary>
    /// <typeparam name="T">The type that owns the property being configured.</typeparam>
    /// <param name="propertyBuilder">The <see cref="PropertyBuilder{T}"/> instance for the property.</param>
    /// <param name="template">The template format string.</param>
    /// <returns>The current <see cref="PropertyBuilder{T}"/> instance for method chaining.</returns>
    /// <example>
    /// Template example: "Note word [number:100-999] [resource:Firstnames] [file:lastnames.txt]"
    /// </example>
    public static PropertyBuilder<T> Template<T>(this PropertyBuilder<T> propertyBuilder, string template)
    {
        propertyBuilder.ValueRule(ValueRules.Template, template);
        return propertyBuilder;
    }
}
