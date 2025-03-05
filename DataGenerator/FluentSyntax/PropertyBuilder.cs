using System.Linq.Expressions;
using Akov.DataGenerator.Core;
using Akov.DataGenerator.Core.GenerationRules;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Utilities;

namespace Akov.DataGenerator.FluentSyntax;

/// <summary>
/// Provides a fluent API for configuring property-specific generation rules.
/// </summary>
/// <typeparam name="T">The type that owns the property being configured.</typeparam>
public class PropertyBuilder<T>(TypeBuilder<T> parent, Property property)
{
    private readonly TypeBuilder<T> _parent = parent;
    private readonly Property _property = property;

    /// <summary>
    /// Retrieves a <see cref="PropertyBuilder{T}"/> for another property of the same type.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="expression">An expression selecting the property.</param>
    /// <returns>A <see cref="PropertyBuilder{T}"/> for the selected property.</returns>
    public PropertyBuilder<T> Property<TProperty>(Expression<Func<T, TProperty>> expression)
        => _parent.Property(expression);

    /// <summary>
    /// Assigns a custom generator to this property and optionally configures its settings.
    /// </summary>
    /// <param name="generator">The generator instance to use.</param>
    /// <param name="settings">Optional settings for the generator.</param>
    /// <returns>The current <see cref="PropertyBuilder{T}"/> instance for method chaining.</returns>
    public PropertyBuilder<T> UseGenerator(GeneratorBase generator, object? settings = null)
    {
        _property.CustomGenerator = generator;
        _property.SetValueRule(generator.GetType().Name, settings);
        return this;
    }

    /// <summary>
    /// Defines a construction rule for this property using a provided expression.
    /// </summary>
    /// <param name="expression">The expression defining how the property should be constructed.</param>
    /// <returns>The current <see cref="PropertyBuilder{T}"/> instance for method chaining.</returns>
    public PropertyBuilder<T> Construct(Expression<Func<T, object?>> expression)
    {
        _property.Constructor = expression.ConvertExpressionReturningNullableObject();
        return this;
    }

    /// <summary>
    /// Sets a decorator for the property using the provided expression.
    /// The decorator will be applied to the generated value of the property.
    /// </summary>
    /// <typeparam name="T">The type of the object that the property belongs to.</typeparam>
    /// <param name="decorator">The decorator. It should return a value of type <c>object?</c>, allowing null values.</param>
    /// <returns>The current <see cref="PropertyBuilder{T}"/> instance, allowing for method chaining.</returns>
    public PropertyBuilder<T> Decorate(Func<object?, object?> decorator)
    {
        _property.Decorator = decorator;
        return this;
    }

    /// <summary>
    /// Specifies that the property can be assigned null values with the given probability.
    /// </summary>
    /// <param name="probability">The probability (between 0 and 1) of the property being null.</param>
    /// <returns>The current <see cref="PropertyBuilder{T}"/> instance for method chaining.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the property is not nullable.</exception>
    public PropertyBuilder<T> Nullable(double probability)
        => GenerationRule(InternalRuleNames.WithNulls, probability, _ =>
        {
            if (!_property.IsNullable)
                throw new InvalidOperationException($"Property {_property.Name} should be nullable.");

            return null;
        });

    /// <summary>
    /// Sets a custom value rule for the property.
    /// </summary>
    /// <param name="rule">The name of the rule.</param>
    /// <param name="settings">The settings associated with the rule.</param>
    /// <returns>The current <see cref="PropertyBuilder{T}"/> instance for method chaining.</returns>
    public PropertyBuilder<T> ValueRule(string rule, object settings)
    {
        _property.SetValueRule(rule, settings);
        return this;
    }

    /// <summary>
    /// Adds a generation rule to the property with the specified name, probability, and result function.
    /// </summary>
    /// <param name="ruleName">
    /// The name of the generation rule to be applied to the property.
    /// </param>
    /// <param name="probability">
    /// The probability associated with this generation rule. The sum of all rule probabilities should not exceed 1.
    /// </param>
    /// <param name="getResult">
    /// A function that generates the result based on the property when the rule is applied.
    /// </param>
    /// <returns>
    /// The current <see cref="PropertyBuilder{T}"/> instance, allowing for method chaining.
    /// </returns>
    /// <remarks>
    /// This method allows you to define how values should be generated for the property based on a given rule and probability.
    /// </remarks>
    public PropertyBuilder<T> GenerationRule(string ruleName, double probability, Func<Property, object?> getResult)
    {
        var rule = new GenerationRule(ruleName, probability, getResult);
        _property.GenerationRules.Add(rule);
        return this;
    }
}