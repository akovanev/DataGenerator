using System;
using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Models;
using Akov.DataGenerator.Profiles;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Processors;

public class TypeDataProcessor<TType>(DgProfileBase profile, IGeneratorFactory generatorFactory)
    : DataProcessor(profile.GetDataScheme<TType>(), generatorFactory)
{
    private readonly IReadOnlyCollection<AssignGeneratorBase> _assignGenerators = profile.GetAssignGenerators();

    protected override bool IsBasicProperty(Property property)
        => property.Type != TemplateType.Calc &&
           property.Type != TemplateType.Assign;
        
    protected override NameValueObject CreateFromCustomProperty(
        string definitionName, Property property, List<NameValueObject> values)
    {
        var propertyObject = CreateCalcPropertyObject(definitionName, property, values);
        var generator = _assignGenerators.Single(a => string.Equals(a.Id, definitionName, StringComparison.OrdinalIgnoreCase));
        object? value = generator.Create(propertyObject);
        return new NameValueObject(propertyObject.Property.Name, value);
    }
}