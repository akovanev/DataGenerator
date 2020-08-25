using Akov.DataGenerator.Failures;
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

        protected internal override object? CreateFailureImpl(Property property, Template template, FailureType failureType)
        {
            if (failureType == FailureType.Nullable) return null;

            //Todo: add logic here

            return "~~~";
        }
    }
}