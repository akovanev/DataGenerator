using Akov.DataGenerator.Core;

namespace Akov.DataGenerator.Generators;

/// <summary>
/// A factory class that creates instances of <see cref="GeneratorBase"/> based on the provided property type.
/// </summary>
public class GeneratorFactory : IGeneratorFactory
{
    /// <summary>
    /// Retrieves a generator instance based on the provided property.
    /// </summary>
    /// <param name="property">The property for which a generator is to be created.</param>
    /// <returns>An instance of <see cref="GeneratorBase"/> for the given property.</returns>
    /// <exception cref="NotSupportedException">Thrown if no generator is available for the specified property type.</exception>
    public GeneratorBase Get(Property property)
    {
        if (property.CustomGenerator is not null)
            return property.CustomGenerator;

        if (property.Type.IsEnum)
            return new EnumGenerator();

        var generatorDictionary = GetGeneratorsMapping();

        return generatorDictionary.TryGetValue(property.Type.Name, out var value)
            ? value
            : throw new NotSupportedException($"Generator for {property.Type.Name} is not implemented yet");
    }

    /// <summary>
    /// Provides a dictionary mapping type names to their corresponding generators.
    /// </summary>
    /// <returns>A dictionary mapping property type names to <see cref="GeneratorBase"/> instances.</returns>
    protected virtual Dictionary<string, GeneratorBase> GetGeneratorsMapping()
        => new()
        {
            {nameof(Boolean), new BooleanGenerator()},
            {nameof(Decimal), new DecimalGenerator()},
            {nameof(Double), new DoubleGenerator()},
            {nameof(DateTime), new DatetimeGenerator()},
            {nameof(DateTimeOffset), new DateTimeOffsetGenerator()},
            {nameof(Guid), new GuidGenerator()},
            {nameof(Int32), new IntGenerator()},
            {nameof(Int64), new LongGenerator()},
            {nameof(String), new StringGenerator()},
        };
}
