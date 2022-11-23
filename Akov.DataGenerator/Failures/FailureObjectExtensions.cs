using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Constants;

namespace Akov.DataGenerator.Failures;

internal static class FailureObjectExtensions
{
    public static FailureType GetFailureType(this List<FailureObject> list, double value)
        => list.First(f => f.RandomRage.In(value)).FailureType;
}