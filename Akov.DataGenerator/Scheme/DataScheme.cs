using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Extensions;

namespace Akov.DataGenerator.Scheme
{
    /// <summary>
    /// Represents the scheme for data to be generated.
    /// </summary>
    public class DataScheme
    {
        public DataScheme() {}

        public DataScheme(string root, List<Definition> definitions)
        {
            Root = root;
            Definitions = definitions;
        }

        //Main_Definition_To_Start_From.Name === Root.
        public string? Root { get; set; }

        public List<Definition>? Definitions { get; set; }

        internal Definition GetDefinition(string pattern)
        {
            Definition definition = Definitions.SingleOrDefault(def => def.Name == pattern);
            definition.ThrowIfNull($"Definition with the name {pattern} not found");
            definition.Properties.ThrowIfNullOrEmpty($"Definition with the name {pattern} must have at least one property");

            return definition;
        }
    }
}
