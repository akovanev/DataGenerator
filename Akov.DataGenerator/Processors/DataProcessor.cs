using System;
using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Common;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Processors
{
    public class DataProcessor : IDataPcocessor
    {
        protected const int DefaultArrayCount = 10;

        protected readonly DataScheme Scheme;
        protected readonly IGeneratorFactory GeneratorFactory;

        public DataProcessor(DataScheme scheme, IGeneratorFactory generatorFactory)
        {
            scheme.ThrowIfNull(nameof(scheme));
            scheme.Root.ThrowIfNull(nameof(scheme.Root));
            scheme.Definitions.ThrowIfNullOrEmpty(nameof(scheme.Definitions));

            Scheme = scheme;
            GeneratorFactory = generatorFactory ?? throw new ArgumentNullException(nameof(generatorFactory));
        }

        public ValueObject CreateData()
        {
            Definition definition = Scheme.GetDefinition(Scheme.Root!);
            return new ValueObject(null, CreateProperties(definition.Properties!));
        }

        internal List<ValueObject> CreateProperties(List<Property> properties)
        {
            return properties.Select(CreateProperty).ToList();
        }

        internal ValueObject CreateProperty(Property property)
        {
            property.Name.ThrowIfNull($"Property does not have a name");
            property.Type.ThrowIfNull($"Property {property.Name} does not have the type");

            if (property.Type == TemplateType.Object)
            {
                return CreateObjectProperty(property);
            }
            if (property.Type == TemplateType.Array)
            {
                int count = property.MaxLength ?? DefaultArrayCount;

                var arrayOfValues = new List<ValueObject>();
                for (int i = 0; i < count; i++)
                    arrayOfValues.Add(CreateObjectProperty(property));

                return new ValueObject(property.Name, arrayOfValues);
            }

            return CreateValue(property);
        }

        internal ValueObject CreateObjectProperty(Property property)
        {
            List<ValueObject> values = CreateObjectValues(property);
            return new ValueObject(property.Name, values);
        }

        internal List<ValueObject> CreateObjectValues(Property property)
        {
            if (property.Type != TemplateType.Object && property.Type != TemplateType.Array)
                throw new AggregateException($"Property type expected " +
                                             $"{TemplateType.Object} or {TemplateType.Array}" +
                                             $"but actual {property.Type}");

            property.Pattern.ThrowIfNull($"Property {property.Name} does not have a pattern");

            Definition definition = Scheme.GetDefinition(property.Pattern!);

            return CreateProperties(definition.Properties!);
        }

        internal ValueObject CreateValue(Property property)
        {
            var generator = GeneratorFactory.Get(property.Type!);
            object? value = generator.Create(property);
            return new ValueObject(property.Name, value);
        }
    }
}
