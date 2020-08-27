using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Generators
{
    public class IntGenerator : GeneratorBase
    {
        private const long MinDefault = 0;
        private const long MaxDefault = 100;

        protected internal override object CreateImpl(Property property, Template template)
        {
            long min = (long?)property.MinValue ?? MinDefault;
            long max = (long?)property.MaxValue ?? MaxDefault;

            return GetRandom((int)min, (int)max);
        }

        protected internal override object CreateRangeFailureImpl(Property property, Template template)
        {
            long min = (long?)property.MinValue ?? MinDefault;
            long max = (long?)property.MaxValue ?? MaxDefault;

            return GetRandom(0, 1) == 0
                ? GetRandom(-2 * (int)min, (int)min - 1)
                : GetRandom((int)max + 1, (int)max * 2);
        }
    }
}