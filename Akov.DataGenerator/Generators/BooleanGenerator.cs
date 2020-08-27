using System;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Generators
{
    public class BooleanGenerator : GeneratorBase
    {
        protected internal override object CreateImpl(Property property, Template template)
        {
            return Convert.ToBoolean(GetRandom(0,1));
        }

        protected internal override object CreateRangeFailureImpl(Property property, Template template)
        {
            return GetRandom(0, 1);
        }
    }
}