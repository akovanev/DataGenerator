using System;
using Akov.DataGenerator.Models;
using Akov.DataGenerator.Common;

namespace Akov.DataGenerator.Generators
{
    public class UIntGenerator : GeneratorBase
    {
        protected override object CreateImpl(PropertyObject propertyObject)
        {
            Random random = GetRandomInstance(propertyObject, nameof(CreateImpl));
            return random.GetInt(0, 1000);
        }

        protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
        {
            Random random = GetRandomInstance(propertyObject, nameof(CreateRangeFailureImpl));
            return random.GetInt(-100, -1);
        }
    }
}
