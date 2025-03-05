using Akov.DataGenerator.Core;
using Akov.DataGenerator.Core.GenerationRules;

namespace Akov.DataGenerator.Generators;

/// <summary>
/// The base class for generators that create random values for properties of various types.
/// </summary>
public abstract class GeneratorBase
{
    /// <summary>
    /// Gets an instance of a random number generator, using the factory to create or retrieve an existing instance.
    /// </summary>
    /// <param name="property">The property for which a random value is to be generated.</param>
    /// <returns>An instance of <see cref="Random"/> to be used for generating random values.</returns>
    protected Random GetRandomInstance(Property property)
        => Dependencies.Factories.RandomFactory.GetOrCreate(property);

    /// <summary>
    /// Creates a boxed random value for the specified property.
    /// </summary>
    /// <param name="property">The property for which the random value is being generated.</param>
    /// <returns>A boxed random value for the given property.</returns>
    public abstract object? CreateBoxedRandomValue(Property property);
}

/// <summary>
/// A base class for generators that create random values of a specific type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of value the generator will create.</typeparam>
public abstract class GeneratorBase<T> : GeneratorBase
{
    /// <summary>
    /// Creates a random value of type <typeparamref name="T"/> for the specified property.
    /// </summary>
    /// <param name="property">The property for which the random value is being generated.</param>
    /// <returns>A random value of type <typeparamref name="T"/> or null if the property allows nullable values.</returns>
    public abstract T? CreateRandomValue(Property property);

    /// <inheritdoc />
    public override object? CreateBoxedRandomValue(Property property)
    {
        if (property.GenerationRules.Count == 0)
            return CreateRandomValue(property);

        var ruleName = property.GenerationRules.GetRandomGenerationRuleFor(property);

        if (ruleName == InternalRuleNames.None)
            return CreateRandomValue(property);

        var generationRule = property.GenerationRules.Single(b => b.RuleName == ruleName);
        return generationRule.CreateValue(property);
    }
}
