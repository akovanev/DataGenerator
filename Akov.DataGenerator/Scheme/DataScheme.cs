using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Common;
using Akov.DataGenerator.Extensions;

namespace Akov.DataGenerator.Scheme
{
    public class DataScheme
    {
        public DataScheme() {}

        public DataScheme(string root, List<Definition> definitions)
        {
            Root = root;
            Definitions = definitions;
        }

        public string? Root { get; set; }
        public List<Definition>? Definitions { get; set; }

        public Definition GetDefinition(string pattern)
        {
            Definition definition = Definitions.SingleOrDefault(def => def.Name == pattern);
            definition.ThrowIfNull($"Definition with the name {pattern} not found");
            definition.Properties.ThrowIfNullOrEmpty($"Definition with the name {pattern} must have at least one property");

            return definition;
        }
    }
}
