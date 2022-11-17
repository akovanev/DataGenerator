using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Profiles;

namespace Akov.DataGenerator.Generators
{
    public class GeneratorFactory : IGeneratorFactory
    {
        private readonly IReadOnlyCollection<AssignGeneratorBase> _assignGenerators;

        public GeneratorFactory(DgProfileBase? profile = null)
        {
            _assignGenerators = profile?.GetAssignGenerators() 
                                ?? new ReadOnlyCollection<AssignGeneratorBase>(Enumerable.Empty<AssignGeneratorBase>().ToList());
        }
        
        public GeneratorBase Get(string type)
        {
            var generatorDictionary = GetGeneratorDictionary();

            return generatorDictionary.ContainsKey(type)
                ? generatorDictionary[type]
                : throw new NotSupportedException($"Generator for {type} is not implemented yet");
        }

        protected virtual Dictionary<string, GeneratorBase> GetGeneratorDictionary()
            =>  new()
            {
                {TemplateType.String, new StringGenerator()},
                {TemplateType.Set, new SetGenerator()},
                {TemplateType.File, new SetGenerator()},
                {TemplateType.Guid, new GuidGenerator()},
                {TemplateType.Bool, new BooleanGenerator()},
                {TemplateType.Int, new IntGenerator()},
                {TemplateType.Double, new DoubleGenerator()},
                {TemplateType.DateTime, new DatetimeGenerator()},
            };

        AssignGeneratorBase IGeneratorFactory.GetAssign(string id)
            => _assignGenerators.Single(a => string.Equals(a.Id,id, StringComparison.OrdinalIgnoreCase));
    }
}