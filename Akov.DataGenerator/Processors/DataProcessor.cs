using System;
using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Factories;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Models;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Processors;

public class DataProcessor : IDataProcessor
{
    private readonly DataScheme _scheme;
    private readonly IGeneratorFactory _generatorFactory;
    private readonly PropertyObjectFactory _propertyObjectFactory;
    private readonly RandomFactory _randomFactory;

    public DataProcessor(DataScheme scheme, IGeneratorFactory generatorFactory)
    {
        scheme.ThrowIfNull(nameof(scheme));
        scheme.Root.ThrowIfNull(nameof(scheme.Root));
        scheme.Definitions.ThrowIfNullOrEmpty(nameof(scheme.Definitions));

        _scheme = scheme;
        _generatorFactory = generatorFactory ?? throw new ArgumentNullException(nameof(generatorFactory));
        _propertyObjectFactory = new PropertyObjectFactory();
        _randomFactory = new RandomFactory();
    }

    public NameValueObject CreateData()
    {
        Definition definition = _scheme.GetDefinition(_scheme.Root!);
        return new NameValueObject(null, CreateFromDefinition(definition));
    }
        
    protected CalcPropertyObject CreateCalcPropertyObject(string definitionName, Property property, List<NameValueObject> values)
        => _propertyObjectFactory.CreateCalcPropertyObject(definitionName, property, values);
        
    protected virtual NameValueObject CreateFromCustomProperty(string definitionName, Property property, List<NameValueObject> values)
        => throw new NotSupportedException("Support was not found for custom properties");
        
    protected virtual bool IsBasicProperty(Property property)
        => property.Type != TemplateType.Calc;

    private bool IsCalcProperty(Property property)
        => property.Type == TemplateType.Calc;
        
    private List<NameValueObject> CreateFromDefinition(Definition definition)
    {
        definition.Properties!.ThrowIfAnyGeneralError();

        List<Property> properties = definition.Properties!
            .Where(IsBasicProperty)
            .ToList();

        List<Property> calcProperties = definition.Properties!
            .Where(IsCalcProperty)
            .ToList();
            
        List<Property> customProperties = definition.Properties!
            .Except(properties)
            .Except(calcProperties)
            .ToList();

        List<NameValueObject> values = properties
            .Select(p => CreateFromDefinitionProperty(definition.Name!, p))
            .ToList();

        values.AddRange(calcProperties
            .Select(p => CreateFromCalculatedProperty(definition.Name!, p, values))
            .ToList());
            
        values.AddRange(customProperties
            .Select(p => CreateFromCustomProperty(definition.Name!, p, values))
            .ToList());

        var propertiesByOrder = definition.Properties!.Select(p => p.Name!).ToList();
        return values.OrderBy(v => propertiesByOrder.IndexOf(v.Name)).ToList();
    }

    private NameValueObject CreateFromDefinitionProperty(string definitionName, Property property)
    {
        PropertyObject propertyObject = _propertyObjectFactory.CreatePropertyObject(definitionName, property);
        return CreateFromPropertyObject(propertyObject);
    }

    private NameValueObject CreateFromCalculatedProperty(
        string definitionName, Property property, List<NameValueObject> values)
    {
        CalcPropertyObject propertyObject = CreateCalcPropertyObject(definitionName, property, values);
        return CreateValue(propertyObject);
    }

    private NameValueObject CreateFromPropertyObject(PropertyObject propertyObject)
    {
        if (propertyObject.Property.Type == TemplateType.Object)
        {
            return CreateFromObjectTemplate(propertyObject);
        }
        if (propertyObject.Property.Type == TemplateType.Array)
        {
            int count = _randomFactory.GetArraySize(propertyObject);

            var arrayOfValues = new List<NameValueObject>();

            if (propertyObject.Property.Pattern!.IsInExistingTemplates())
            {
                var newPropertyObject = PropertyObject.CreateWithTypeAndPattern(
                    propertyObject,
                    propertyObject.Property.Pattern!,
                    propertyObject.Property.SubTypePattern);

                for (int i = 0; i < count; i++)
                    arrayOfValues.Add(CreateValue(newPropertyObject));
            }
            else
            {
                for (int i = 0; i < count; i++)
                    arrayOfValues.Add(CreateFromObjectTemplate(propertyObject));
            }

            return new NameValueObject(propertyObject.Property.Name, arrayOfValues);
        }

        return CreateValue(propertyObject);
    }

    private NameValueObject CreateFromObjectTemplate(PropertyObject propertyObject)
    {
        List<NameValueObject> values = CreateValues(propertyObject);
        return new NameValueObject(propertyObject.Property.Name, values);
    }

    private List<NameValueObject> CreateValues(PropertyObject propertyObject)
    {
        if (propertyObject.Property.Type != TemplateType.Object && 
            propertyObject.Property.Type != TemplateType.Array)
            throw new AggregateException($"Property type expected " +
                                         $"{TemplateType.Object} or {TemplateType.Array}" +
                                         $"but actual {propertyObject.Property.Type}");

        propertyObject.Property.Pattern
            .ThrowIfNull($"Property {propertyObject.Property.Name} does not have a pattern");

        Definition definition = _scheme.GetDefinition(propertyObject.Property.Pattern!);
        return CreateFromDefinition(definition);
    }

    private NameValueObject CreateValue(PropertyObject propertyObject)
    {
        var generator = _generatorFactory.Get(propertyObject.Property.Type!);
        object? value = generator.Create(propertyObject);
        return new NameValueObject(propertyObject.Property.Name, value);
    }
}