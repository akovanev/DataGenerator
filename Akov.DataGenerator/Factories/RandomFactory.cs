using System;
using System.Collections.Concurrent;
using Akov.DataGenerator.Common;

namespace Akov.DataGenerator.Factories
{
    internal class RandomFactory
    {
        private readonly ConcurrentDictionary<string, Random> _randoms = new();

        public Random GetOrCreate(string definitionName, string propertyName, string step)
        {
            string key = $"{definitionName}.{propertyName}_{step}";
            _randoms.TryAdd(key, ThreadSafeRandom.Instance);
            return _randoms[key];
        }
    }
}
