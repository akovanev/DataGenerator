using System;
using System.Collections.Generic;

namespace Akov.DataGenerator.Factories
{
    internal class RandomFactory
    {
        private static readonly Random SeedRandom = new Random();
        private static readonly Dictionary<string, Random> Randoms = new Dictionary<string, Random>();

        public Random GetOrCreate(string definitionName, string propertyName, string step)
        {
            string key = $"{definitionName}.{propertyName}_{step}";
            if (!Randoms.ContainsKey(key))
            {
                Randoms.Add(key, new Random(SeedRandom.Next()));
            }
            return Randoms[key];
        }
    }
}
