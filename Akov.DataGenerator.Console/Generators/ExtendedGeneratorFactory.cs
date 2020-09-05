using System.Collections.Generic;
using Akov.DataGenerator.Constants;

namespace Akov.DataGenerator.Generators
{
    public class ExtendedGeneratorFactory : GeneratorFactory
    {
        public override Dictionary<string, GeneratorBase> GetGeneratorDictionary()
        {
            Dictionary<string, GeneratorBase> generators = base.GetGeneratorDictionary();
            generators.Add("uint", new UIntGenerator());
            generators.Add(TemplateType.Calc, new CalcGenerator());
            return generators;
        }
    }
}