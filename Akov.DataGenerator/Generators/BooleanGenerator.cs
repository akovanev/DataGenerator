using System;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Generators
{
    public class BooleanGenerator : GeneratorBase
    {
        protected internal override object CreateImpl(Property property)
        {
            return Convert.ToBoolean(GetRandom(0,1));
        }

        protected internal override object CreateRangeFailureImpl(Property property)
        {
            return GetRandom(0, 1);
        }
    }
}