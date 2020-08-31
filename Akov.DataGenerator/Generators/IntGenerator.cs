using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Generators
{
    public class IntGenerator : NumberGenerator
    {
        private const double MinDefault = 0;
        private const double MaxDefault = 100;

        protected override object CreateImpl(PropertyObject propertyObject)
        {
            double value = CreateValue(propertyObject, MinDefault, MaxDefault);
            return (int) value;
        }

        protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
        {
            double value = CreateRangeFailureValue(propertyObject, MinDefault, MaxDefault);
            return (int) value;
        }
    }
}