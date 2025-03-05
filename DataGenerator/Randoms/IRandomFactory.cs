using Akov.DataGenerator.Core;

namespace Akov.DataGenerator.Randoms;

/// <summary>
/// Defines the contract for a factory that provides or creates a <see cref="Random"/> instance.
/// </summary>
public interface IRandomFactory
{
    /// <summary>
    /// Retrieves an existing <see cref="Random"/> instance or creates a new one based on the given property.
    /// </summary>
    /// <param name="property">The property associated with the random instance to be retrieved or created.</param>
    /// <returns>A <see cref="Random"/> instance.</returns>
    Random GetOrCreate(Property property);
}
