using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Generators
{
    public interface IGenerator
    {
        public object? Create(Property property, Template template);
    }
}