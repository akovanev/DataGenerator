using System.Collections.Generic;
using System;
using Akov.DataGenerator.Models;
using Akov.DataGenerator.Common;

namespace Akov.DataGenerator.Generators
{
    public class UIntGenerator : NumberGenerator
    {
        protected override object CreateImpl(PropertyObject propertyObject)
        {
            Random random = GetRandomInstance(propertyObject);
            return random.GetInt(0, 1000);
        }

        protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
        {
            Random random = GetRandomChoiceInstance();
            return random.GetInt(-100, -1);
        }
    }

    public class ExtendedGeneratorFactory : GeneratorFactory
    {
        public override Dictionary<string, GeneratorBase> GetGeneratorDictionary()
        {
            Dictionary<string, GeneratorBase> generators = base.GetGeneratorDictionary();
            generators.Add("uint", new UIntGenerator());
            return generators;
        }
    }
}
