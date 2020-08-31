using System.Collections.Generic;
using System;
using Akov.DataGenerator.Models;
using Akov.DataGenerator.Common;

namespace Akov.DataGenerator.Generators
{
    public class UIntGenerator : GeneratorBase
    {
        protected override object CreateImpl(PropertyObject propertyObject)
        {
            Random random = GetRandomInstance(propertyObject, nameof(CreateImpl));
            return random.GetInt(0, 1000);
        }

        protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
        {
            Random random = GetRandomInstance(propertyObject, nameof(CreateRangeFailureImpl));
            return random.GetInt(-100, -1);
        }Í
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
