using System;
using System.Linq;

namespace Akov.DataGenerator.Common
{
    public static class RandomExtensions
    {
        public static int GetInt(this Random random, int min, int max)
        {
            return random.Next(min, max + 1);
        }

        public static double GetDouble(this Random random, double min, double max)
        {
            return min + random.NextDouble() * (max - min);
        }

        public static int[]  GetSequence(this Random random, int max, int count)
        {
            return Enumerable.Range(0, count)
                .Select(x => random.Next(max))
                .ToArray();
        }
    }
}
