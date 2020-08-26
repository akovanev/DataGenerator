using System;
using System.Collections.Generic;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Processor
{
    internal class GeneratorFactory
    {
        private static readonly Dictionary<TemplateType, GeneratorBase> GeneratorDictionary 
            = new Dictionary<TemplateType, GeneratorBase>
            {
                {TemplateType.String, new StringGenerator()},
                {TemplateType.Set, new SetGenerator()},
                {TemplateType.Bool, new BooleanGenerator()},
                {TemplateType.Int, new IntGenerator()},
                {TemplateType.Double, new DoubleGenerator()},
                {TemplateType.DateTime, new DatetimeGenerator()},
            };

        internal GeneratorBase Get(TemplateType type)
        {
            return GeneratorDictionary.ContainsKey(type)
                ? GeneratorDictionary[type]
                : throw new NotSupportedException($"Generator for {type} is not implemented yet");
        }
    }
}