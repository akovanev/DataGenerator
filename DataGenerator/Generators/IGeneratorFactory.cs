using Akov.DataGenerator.Core;

namespace Akov.DataGenerator.Generators;

/// <summary>
/// Defines a factory for generating instances of <see cref="GeneratorBase"/> based on the provided property.
/// </summary>
public interface IGeneratorFactory
{
    /// <summary>
    /// Retrieves a generator instance based on the provided property.
    /// </summary>
    /// <param name="property">The property for which a generator is to be created.</param>
    /// <returns>An instance of <see cref="GeneratorBase"/> that can generate values for the given property.</returns>
    GeneratorBase Get(Property property);
}
