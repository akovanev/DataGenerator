using System;
using System.Collections.Generic;
using System.Linq;
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
    
    public static class CalcGeneratorExtensions
    {
        public static bool Owns(this CalcPropertyObject propertyObject, string propertyName,  params Type[] definitions)
            => string.Equals(propertyObject.Property.Name, propertyName, StringComparison.OrdinalIgnoreCase) &&
              (!definitions.Any() || definitions.Select(d => d.Name).Contains(propertyObject.DefinitionName, StringComparer.OrdinalIgnoreCase));
        
        public static object? ValueOf(this CalcPropertyObject propertyObject, string propertyName)
            => propertyObject.Values
                .Single(v => String.Equals(v.Name, propertyName, StringComparison.OrdinalIgnoreCase))
                .Value;
    }
}