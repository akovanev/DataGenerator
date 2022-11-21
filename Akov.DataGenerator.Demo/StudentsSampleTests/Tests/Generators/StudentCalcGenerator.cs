using System;
using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Demo.StudentsSample.Responses;
using Akov.DataGenerator.Demo.StudentsSampleTests.Tests.DgModels;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Generators;

/// <summary>
/// Creates calculated values for the StudentCollection and the Student class.
/// The logic is just an example that does not match all the best practices.
/// </summary>
public class StudentCalcGenerator : CalcGeneratorBase
{
    protected override object CreateImpl(CalcPropertyObject propertyObject)
    {
        if (propertyObject.Owns(nameof(Student.FullName), typeof(DgStudent)))
        {
            return $"{propertyObject.ValueOf(nameof(Student.FirstName))} {propertyObject.ValueOf(nameof(Student.LastName))}";
        }
        if(propertyObject.Owns(nameof(StudentCollection.Count), typeof(DgStudentCollection), typeof(StudentCollection)))
        {
            return (propertyObject.ValueOf(nameof(StudentCollection.Students)) as List<NameValueObject>)!.Count;
        }

        throw new NotSupportedException("Not expected calculated property");
    }

    protected override object CreateRangeFailureImpl(CalcPropertyObject propertyObject)
    {
        throw new NotSupportedException("Range failure not supported");
    }
}