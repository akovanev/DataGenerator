using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Generators
{
    public interface IGenerator
    {
        public object? Create(PropertyObject propertyObject);
    }
}