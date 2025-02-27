using System;
using System.Collections.Generic;
using Akov.DataGenerator.Constants;

namespace Akov.DataGenerator.Generators;

public class GeneratorFactory : IGeneratorFactory
{
    public GeneratorBase Get(string type)
    {
        var generatorDictionary = GetGeneratorDictionary();

        return generatorDictionary.TryGetValue(type, out var value)
            ? value
            : throw new NotSupportedException($"Generator for {type} is not implemented yet");
    }

    protected virtual Dictionary<string, GeneratorBase> GetGeneratorDictionary()
        =>  new()
        {
            {TemplateType.String, new StringGenerator()},
            {TemplateType.Note, new NoteGenerator()},
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
            {TemplateType.CompositeString, new CompositeStringGenerator()},
            {TemplateType.Email, new EmailGenerator()},
            {TemplateType.IpV4, new IpGenerator()}
        };
}