using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Common;

namespace Akov.DataGenerator.Scheme
{
    public class DataScheme
    {
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
