using System;
using System.Collections.Generic;
using Akov.DataGenerator.Constants;

namespace Akov.DataGenerator.Generators;

public class GeneratorFactory : IGeneratorFactory
{
    public GeneratorBase Get(string type)
    {
        var generatorDictionary = GetGeneratorDictionary();

        return generatorDictionary.ContainsKey(type)
            ? generatorDictionary[type]
            : throw new NotSupportedException($"Generator for {type} is not implemented yet");
    }

    protected virtual Dictionary<string, GeneratorBase> GetGeneratorDictionary()
        =>  new()
        {
            {TemplateType.String, new StringGenerator()},
            {TemplateType.ByteArray, new ByteArrayGenerator()},
            {TemplateType.Set, new SetGenerator()},
            {TemplateType.File, new SetGenerator()},
            {TemplateType.Resource, new SetGenerator()},
            {TemplateType.Guid, new GuidGenerator()},
            {TemplateType.Bool, new BooleanGenerator()},
            {TemplateType.Int, new IntGenerator()},
            {TemplateType.Double, new DoubleGenerator()},
            {TemplateType.Decimal, new DecimalGenerator()},
            {TemplateType.DateTime, new DatetimeGenerator()},
            {TemplateType.Phone, new PhoneGenerator()},
            {TemplateType.Email, new EmailGenerator()}
        };
}