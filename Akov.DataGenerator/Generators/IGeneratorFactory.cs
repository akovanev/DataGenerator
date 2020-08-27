using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Generators
{
    public interface IGeneratorFactory
    {
        public GeneratorBase Get(TemplateType type);
    }
}