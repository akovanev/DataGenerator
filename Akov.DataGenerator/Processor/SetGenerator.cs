using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Processor
{
    internal class SetGenerator : GeneratorBase
    {
        private const string DefaultPattern = "00000";

        protected internal override object CreateImpl(Property property, Template template)
        {
            string pattern = template.Pattern ?? DefaultPattern;

            string[] set = pattern.Split(",");

            int random = GetRandom(0, set.Length - 1);

            return set[random];
        }

        protected internal override object CreateRangeFailureImpl(Property property, Template template)
        {
            return DefaultPattern;
        }
    }
}