using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Factories;

internal class DefaultTypeValueRandomFactory : RandomFactory
{
    public override int GetArraySize(PropertyObject propertyObject) => 1;
}