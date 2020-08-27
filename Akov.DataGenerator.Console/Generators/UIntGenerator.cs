using System.Collections.Generic;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Generators
{
    public class UIntGenerator : GeneratorBase
    {
        protected override object CreateImpl(Property property, Template template)
        {
            return GetRandom(0, 1000);
        }

        protected override object CreateRangeFailureImpl(Property property, Template template)
        {
            return GetRandom(-100, -1);
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
