using System;
using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Common;
using Akov.DataGenerator.Models;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Processors
{
    public class DataProcessor : IDataPcocessor
    {
        private const int DefaultArrayCount = 10;
        private readonly DataScheme _scheme;
        private readonly IGeneratorFactory _generatorFactory;
        private readonly IOHelper _ioHelper;

        public DataProcessor(DataScheme scheme, IGeneratorFactory generatorFactory)
        {
            scheme.ThrowIfNull(nameof(scheme));
            scheme.Root.ThrowIfNull(nameof(scheme.Root));
            scheme.Definitions.ThrowIfNullOrEmpty(nameof(scheme.Definitions));

            _scheme = scheme;
            _generatorFactory = generatorFactory ?? throw new ArgumentNullException(nameof(generatorFactory));
            _ioHelper = new IOHelper();
        }

        public NameValueObject CreateData()
        {
            Definition definition = _scheme.GetDefinition(_scheme.Root!);
            return new NameValueObject(null, CreateFromDefinition(definition));
        }

        private List<NameValueObject> CreateFromDefinition(Definition definition)
        {
            return definition.Properties
                .Select(p => CreateFromDefinitionProperty(definition.Name!, p))
                .ToList();
        }

        private NameValueObject CreateFromDefinitionProperty(string definitionName, Property property)
        {
            property.Name.ThrowIfNull($"Property does not have a name");
            property.Type.ThrowIfNull($"Property {property.Name} does not have the type");

            PropertyObject propertyObject = CreatePropertyObject(definitionName, property);
            return CreateFromPropertyObject(propertyObject);
        }

        private NameValueObject CreateFromPropertyObject(PropertyObject propertyObject)
        {
            if (propertyObject.Property.Type == TemplateType.Object)
            {
                return CreateFromObjectTemplate(propertyObject);
            }
            if (propertyObject.Property.Type == TemplateType.Array)
            {
                int count = propertyObject.Property.MaxLength ?? DefaultArrayCount;

                var arrayOfValues = new List<NameValueObject>();
                for (int i = 0; i < count; i++)
                    arrayOfValues.Add(CreateFromObjectTemplate(propertyObject));

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

        private PropertyObject CreatePropertyObject(string definitionName, Property property)
        {
            if(property.Type == TemplateType.File)
            {
                property.Pattern.ThrowIfNull($"Property {property.Name} does not have a pattern");
                string fileContent = _ioHelper.GetFileContent(property.Pattern!);
                return new PropertyObject(definitionName, property, fileContent);
            }

            return new PropertyObject(definitionName, property);
        }
    }
}
