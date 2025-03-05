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
}
