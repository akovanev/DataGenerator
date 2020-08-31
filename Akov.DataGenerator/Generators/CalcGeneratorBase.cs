using System;
using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Generators
{
    public abstract class CalcGeneratorBase : GeneratorBase
    {
        protected abstract object CreateImpl(CalcPropertyObject propertyObject);
        protected override object CreateImpl(PropertyObject propertyObject)
        {
            if(!(propertyObject is CalcPropertyObject calcPropertyObject))
                throw new ArgumentException($"{nameof(propertyObject)} " +
                                            $"should be of type {nameof(CalcPropertyObject)}");

            return CreateImpl(calcPropertyObject);
        }

        protected abstract object CreateRangeFailureImpl(CalcPropertyObject propertyObject);
        protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
        {
            if (!(propertyObject is CalcPropertyObject calcPropertyObject))
                throw new ArgumentException($"{nameof(propertyObject)} " +
                                            $"should be of type {nameof(CalcPropertyObject)}");

            return CreateRangeFailureImpl(calcPropertyObject);
        }
    }
}