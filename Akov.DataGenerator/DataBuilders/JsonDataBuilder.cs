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
        private const int DefaultArrayCount = 10;
        private readonly StringBuilder _builder = new StringBuilder();
        private readonly GeneratorFactory _generatorFactory;
        private readonly DataScheme _scheme;
        private readonly List<Property> _rootProperties;

        public JsonDataBuilder(GeneratorFactory generatorFactory, DataScheme scheme)
        {
            scheme.ThrowIfNull(nameof(scheme));
            scheme.Templates.ThrowIfNullOrEmpty(nameof(scheme.Templates));
            scheme.Root.ThrowIfNull(nameof(scheme.Root));
            scheme.Root!.Template.ThrowIfNull("Root template name");
            scheme.Definitions.ThrowIfNullOrEmpty(nameof(scheme.Definitions));

            Template template = scheme.GetTemplate(scheme.Root.Template!);
            Definition definition = scheme.GetDefinition(template.Pattern!);

            _generatorFactory = generatorFactory;
            _scheme = scheme;
            _rootProperties = definition.Properties!;
        }

        internal string Build()
        {
            string? name = _scheme.Root!.Name;
            _builder.InsertObject(name, () =>  BuildProperties(_rootProperties), true);
            return _builder.ToString();
        }

        protected internal void BuildProperties(List<Property> properties)
        {
            for (int i = 0; i < properties.Count; i++)
            {
                properties[i].Template.ThrowIfNull("Every property should have a template");

                Template template = _scheme.GetTemplate(properties[i].Template!);
                
                if(template.Type == TemplateType.Object)
                {
                    Definition definition = _scheme.GetDefinition(template.Pattern!);

                    BuildArrayItem(
                        properties[i].Name,
                        definition.Properties!, 
                        i == properties.Count - 1);
                }
                else if(template.Type == TemplateType.Array)
                {
                    Definition definition = _scheme.GetDefinition(template.Pattern!);

                    BuildArray(
                        properties[i].Name, 
                        properties[i].MaxLength,
                        definition.Properties!, 
                        i == properties.Count - 1);
                }
                else
                {
                    BuildProperty(properties[i], template, i == properties.Count - 1);
                }
            }
        }

        protected internal void BuildArray(string? arrayName, int? count, List<Property> properties, bool isLastItem)
        {
            _builder.InsertArray(
                arrayName,
                () =>
                {
                    count ??= DefaultArrayCount;
                    for (int i = 0; i < count.Value; i++)
                        BuildArrayItem(null, properties, i == count - 1);
                }, isLastItem);
        }

        protected internal void BuildArrayItem(string? name, List<Property> properties, bool isLastItem)
        {
            _builder.InsertObject(name, () => BuildProperties(properties), isLastItem);
        }

        
        protected internal void BuildProperty(Property property, Template template, bool isLastItem)
        {
            var generator = _generatorFactory.Get(template.Type);
            object? value = generator.Create(property, template);
            _builder.InsertProperty(property.Name, value, isLastItem);
        }
    }
}
