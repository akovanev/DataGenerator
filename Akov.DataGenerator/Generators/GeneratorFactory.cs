using System;
using System.Collections.Generic;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Generators
{
    public class GeneratorFactory : IGeneratorFactory
    {
        public GeneratorBase Get(string type)
        {
            var generatorDictionary = GetGeneratorDictionary();

            return generatorDictionary.ContainsKey(type)
                ? generatorDictionary[type]
                : throw new NotSupportedException($"Generator for {type} is not implemented yet");
        }

        public virtual Dictionary<string, GeneratorBase> GetGeneratorDictionary()
            =>  new Dictionary<string, GeneratorBase>
            {
                {TemplateType.String, new StringGenerator()},
                {TemplateType.Set, new SetGenerator()},
                {TemplateType.File, new SetGenerator()},
                {TemplateType.Guid, new GuidGenerator()},
                {TemplateType.Bool, new BooleanGenerator()},
                {TemplateType.Int, new IntGenerator()},
                {TemplateType.Double, new DoubleGenerator()},
                {TemplateType.DateTime, new DatetimeGenerator()},
            };
    }
}