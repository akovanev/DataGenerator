using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Factories;

// ReSharper disable once InconsistentNaming
internal class DefaultTypeValueRandomFactory : RandomFactory
{
    public override int GetArraySize(PropertyObject propertyObject) => 1;
}