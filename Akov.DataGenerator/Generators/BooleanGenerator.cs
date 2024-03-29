﻿using System;
using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Generators;

public class BooleanGenerator : GeneratorBase
{
    protected override object CreateImpl(PropertyObject propertyObject)
    {
        return Convert.ToBoolean(
            GetRandomInstance(propertyObject).GetInt(0,1));
    }

    protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
    {
        return GetRandomInstance(propertyObject, nameof(CreateRangeFailureImpl)).GetInt(0, 1);
    }
}