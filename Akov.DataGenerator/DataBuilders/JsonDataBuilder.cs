using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Akov.DataGenerator.Processor;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.DataBuilders
{
    internal class JsonDataBuilder
    {
        private const int DefaultCount = 10;
        private const int DefaultAttributeCount = 3;
        private readonly StringBuilder _builder = new StringBuilder();
        private readonly GeneratorFactory _generatorFactory = new GeneratorFactory();

        internal string Build(DataScheme scheme)
        {
            if (scheme.Properties is null || !scheme.Properties.Any())
                throw new NotSupportedException($"Properties annotation should not be null or empty");

            _builder.InsertObject(() => BuildArray(scheme));
            return _builder.ToString();
        }

        protected internal void BuildArray(DataScheme scheme)
        {
            _builder.InsertArray(
                scheme.OutPropertiesName ?? nameof(DataScheme.Properties),
                () =>
                {
                    int count = scheme.ObjectCount ?? DefaultCount;
                    for (int i = 0; i < count; i++)
                        BuildArrayItem(scheme, i == count - 1);
                });
        }

        protected internal void BuildArrayItem(DataScheme scheme, bool isLastItem)
        {
            _builder.InsertObject(() => BuildProperties(scheme), isLastItem);
        }

        protected internal void BuildProperties(DataScheme scheme)
        {
            for (int i = 0; i < scheme.Properties!.Count; i++)
            {
                Template template = scheme.GetTemplate(scheme.Properties[i].Template);
                BuildProperty(scheme.Properties[i], template, false);
            }

            BuildAttributesArray(scheme);
        }

        protected internal void BuildAttributesArray(DataScheme scheme)
        {
            _builder.InsertArray(
                scheme.OutAttributesName ?? nameof(DataScheme.Attributes),
                () =>
                {
                    int count = scheme.AttributesCount ?? DefaultAttributeCount;
                    for (int i = 0; i < count; i++)
                        BuildAttributesArrayItem(scheme, i == count - 1);
                });
        }

        protected internal void BuildAttributesArrayItem(DataScheme scheme, bool isLastItem)
        {
            _builder.InsertObject(() => BuildAttributes(scheme), isLastItem);
        }

        protected internal void BuildAttributes(DataScheme scheme)
        {
            List<Property> attributes = scheme.Attributes ?? new List<Property>();
            for (int i = 0; i < attributes.Count; i++)
            {
                Template template = scheme.GetTemplate(attributes[i].Template);

                BuildProperty(attributes[i], template, i == attributes.Count - 1);
            }
        }

        protected internal void BuildProperty(Property property, Template template, bool isLastItem)
        {
            var generator = _generatorFactory.Get(template.Type);
            object? value = generator.Create(property, template);
            _builder.InsertProperty(property.Name, value, isLastItem);
        }
    }
}
