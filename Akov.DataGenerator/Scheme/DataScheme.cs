using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Common;

namespace Akov.DataGenerator.Scheme
{
    public class DataScheme
    {
        public List<Template>? Templates { get; set; }
        public Property? Root { get; set; }
        public List<Definition>? Definitions { get; set; }

        public Template GetTemplate(string templateName)
        {
            Template template = Templates.SingleOrDefault(t => t.Name == templateName);
            template.ThrowIfNull($"Template with the name {templateName} not found");
            template.Type.ThrowIfNull($"Type for the template {templateName} not found");

            if (template.Type == TemplateType.Object ||
               template.Type == TemplateType.Array)
               {
                   template.Pattern.ThrowIfNullOrEmpty($"Template with the name {templateName} must have pattern");
               }

            return template;
        }   

        public Definition GetDefinition(string pattern)
        {
            Definition definition = Definitions.SingleOrDefault(def => def.Name == pattern);
            definition.ThrowIfNull($"Definition with the name {pattern} not found");
            definition.Properties.ThrowIfNullOrEmpty($"Definition with the name {pattern} must have at least one property");

            return definition;
        }
    }
}
