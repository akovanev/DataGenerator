using System;
using System.Collections.Generic;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Factories
{
    internal class RandomFactory
    {
        private static readonly Random _seedRandom = new Random();
        private static readonly Dictionary<string, Random> _randoms = new Dictionary<string, Random>();

        public Random GetOrCreate(string definitionName, string propertyName, string step)
        {
            string key = $"{definitionName}.{propertyName}_{step}";
            if (!_randoms.ContainsKey(key))
            {
                _randoms.Add(key, new Random(_seedRandom.Next()));
            }
            return _randoms[key];
        }
    }
}
