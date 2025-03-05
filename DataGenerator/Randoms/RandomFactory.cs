using System.Collections.Concurrent;
using Akov.DataGenerator.Core;

namespace Akov.DataGenerator.Randoms;

internal class RandomFactory : IRandomFactory
{
    private readonly ConcurrentDictionary<string, Random> _randoms = new();

    public Random GetOrCreate(Property property)
    {
        string key = $"{property.ParentType.Name}_{property.Name}";
        _randoms.TryAdd(key, ThreadSafeRandom.Instance);
        return _randoms[key];
    }
}