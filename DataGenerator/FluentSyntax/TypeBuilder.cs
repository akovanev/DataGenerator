using System.Linq.Expressions;

namespace Akov.DataGenerator.FluentSyntax;

/// <summary>
/// Provides methods for defining and configuring properties of a type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type that the builder is constructing.</typeparam>
public class TypeBuilder<T>() : TypeBuilderBase(typeof(T))
{
    /// <summary>
    /// Adds or configures a property for the type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="expression">An expression that represents the property to be configured.</param>
    /// <returns>A <see cref="PropertyBuilder{T}"/> to configure additional property details.</returns>
    /// <example>
    /// <code>
    /// typeBuilder.Property(x => x.PropertyName);
    /// </code>
    /// </example>
    public PropertyBuilder<T> Property<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        string propertyName = ((MemberExpression)expression.Body).Member.Name;
        return new PropertyBuilder<T>(this, TypeProperties[propertyName]);
    }
    
    /// <summary>
    /// Removes the specified property from the type configuration, ignoring it during type construction.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property to ignore.</typeparam>
    /// <param name="expression">An expression that represents the property to ignore.</param>
    /// <returns>The current <see cref="TypeBuilder{T}"/> instance for method chaining.</returns>
    /// <example>
    /// <code>
    /// typeBuilder.Ignore(x => x.PropertyName);
    /// </code>
    /// </example>
    public TypeBuilder<T> Ignore<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        string propertyName = ((MemberExpression)expression.Body).Member.Name;
        TypeProperties[propertyName].SkipGeneration = true;
        return this;
    }
}
