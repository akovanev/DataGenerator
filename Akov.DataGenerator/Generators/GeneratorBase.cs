using System;
using System.Collections.Generic;
using Akov.DataGenerator.Failures;
using Akov.DataGenerator.Scheme;
using Akov.DataGenerator.Models;
using Akov.DataGenerator.Common;
using Akov.DataGenerator.Factories;

namespace Akov.DataGenerator.Generators
{
    public abstract class GeneratorBase : IGenerator
    {
        private static readonly RandomFactory RandomFactory = new RandomFactory();

        public object? Create(PropertyObject propertyObject)
        {
            FailureType failureType = GetFailureType(propertyObject);

            return failureType == FailureType.None
                ? CreateImpl(propertyObject)
                : CreateFailureImpl(propertyObject, failureType);
        }

        protected abstract object CreateImpl(PropertyObject propertyObject);
        protected abstract object CreateRangeFailureImpl(PropertyObject propertyObject);
        protected virtual object CreateCustomFailureImpl(PropertyObject propertyObject)
            => propertyObject.Property.CustomFailure ?? "ERROR!!!";

        protected Random GetRandomInstance(PropertyObject propertyObject, string step = nameof(CreateImpl))
        {
            return RandomFactory.GetOrCreate( 
                propertyObject.DefinitionName,
                propertyObject.Property.Name!,
                step);
        }

        private object? CreateFailureImpl(PropertyObject propertyObject, FailureType failureType)
        {
            return failureType switch
            {
                FailureType.Nullable => null,
                FailureType.Range => CreateRangeFailureImpl(propertyObject),
                _ => CreateCustomFailureImpl(propertyObject)
            };
        }

        private  FailureType GetFailureType(PropertyObject propertyObject)
        {
            Failure? failure = propertyObject.Property.Failure;

            if(failure is null) return FailureType.None;

            List<FailureObject> failureObjectList = failure.ToFailureObjectList();

            Random random = RandomFactory.GetOrCreate(
                propertyObject.DefinitionName,
                propertyObject.Property.Name!,
                nameof(failure));

            double value = random.GetDouble(0, 1);
            return failureObjectList.GetFailureType(value);
        }
    }
}