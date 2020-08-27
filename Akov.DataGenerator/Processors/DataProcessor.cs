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
            scheme.Templates.ThrowIfNullOrEmpty(nameof(scheme.Templates));
            scheme.Root.ThrowIfNull(nameof(scheme.Root));
            scheme.Root!.Template.ThrowIfNull("Root template name");
            scheme.Definitions.ThrowIfNullOrEmpty(nameof(scheme.Definitions));

            Scheme = scheme;
            GeneratorFactory = generatorFactory ?? throw new ArgumentNullException(nameof(generatorFactory));
        }

        public ValueObject CreateData()
        {
            Template template = Scheme.GetTemplate(Scheme.Root!.Template!);
            template.Pattern.ThrowIfNull($"Template {template.Name} should have a pattern");
            Definition definition = Scheme.GetDefinition(template.Pattern!);
            return new ValueObject(Scheme.Root!.Name, CreateProperties(definition.Properties!));
        }

        internal List<ValueObject> CreateProperties(List<Property> properties)
        {
            return properties.Select(CreateProperty).ToList();
        }

        internal ValueObject CreateProperty(Property property)
        {
            property.Template.ThrowIfNull($"Property {property.Name} does not have a template");

            Template template = Scheme.GetTemplate(property.Template!);

            if (template.Type == TemplateType.Object)
            {
                return CreateObjectProperty(property, template);
            }
            if (template.Type == TemplateType.Array)
            {
                int count = property.MaxLength ?? DefaultArrayCount;

                var arrayOfValues = new List<ValueObject>();
                for (int i = 0; i < count; i++)
                    arrayOfValues.Add(CreateObjectProperty(property, template));

                return new ValueObject(property.Name, arrayOfValues);
            }

            return CreateValue(property, template);
        }

        internal ValueObject CreateObjectProperty(Property property, Template template)
        {
            List<ValueObject> values = CreateObjectValues(property, template);
            return new ValueObject(property.Name, values);
        }

        internal List<ValueObject> CreateObjectValues(Property property, Template template)
        {
            if (template.Type != TemplateType.Object && template.Type != TemplateType.Array)
                throw new AggregateException($"Template type expected " +
                                             $"{TemplateType.Object} or {TemplateType.Array}" +
                                             $"but actual {template.Type}");

            template.Pattern.ThrowIfNull($"Template {template.Name} should have a pattern");

            Definition definition = Scheme.GetDefinition(template.Pattern!);

            return CreateProperties(definition.Properties!);
        }

        internal ValueObject CreateValue(Property property, Template template)
        {
            var generator = GeneratorFactory.Get(template.Type!);
            object? value = generator.Create(property, template);
            return new ValueObject(property.Name, value);
        }
    }
}
