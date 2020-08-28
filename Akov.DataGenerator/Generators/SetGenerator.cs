using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Generators
{
    public class SetGenerator : GeneratorBase
    {
        private const string DefaultPattern = "00000";

        protected internal override object CreateImpl(Property property)
        {
            string pattern = property.Pattern ?? DefaultPattern;

            string[] set = pattern.Split(",");

            int random = GetRandom(0, set.Length - 1);

            return set[random];
        }

        protected internal override object CreateRangeFailureImpl(Property property)
        {
            return DefaultPattern;
        }
    }
}