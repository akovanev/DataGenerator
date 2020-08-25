using System.Collections.Generic;
using System.Linq;
using System;
using Akov.DataGenerator.DataBuilders;

namespace Akov.DataGenerator.Scheme
{
    internal class DataScheme
    {
        public List<Template>? Templates { get; set; }
        public Property? Root { get; set; }
        public List<Definition>? Definitions { get; set; }

        public Template GetTemplate(string templateName)
        {
            Template template = Templates.SingleOrDefault(template => template.Name == templateName);
            template.ThrowIfNull($"Template with the name {templateName} not found");

            if(template.Type == TemplateType.Object ||
               template.Type == TemplateType.Array)
               {
                   template.Pattern.ThrowIfNullOrEmpty($"Template with the name {templateName} must have pattern");
               }

            return template;
        }   

        public Definition GetDefinition(string pattern)
        {
            Definition definition = Definitions.SingleOrDefault(definition => definition.Name == pattern);
            definition.ThrowIfNull($"Definition with the name {pattern} not found");
            definition.Properties.ThrowIfNullOrEmpty($"Definition with the name {pattern} must have at least one property");

            return definition;
        }
    }
}
