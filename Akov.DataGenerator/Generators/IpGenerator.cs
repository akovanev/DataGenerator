using System;
using System.Text;
using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Generators;

public class IpGenerator : GeneratorBase
{
    protected override object CreateImpl(PropertyObject propertyObject)
    {
        var builder = new StringBuilder();
        Random random = GetRandomInstance(propertyObject);
        
        builder.Append(random.GetInt(1, 255));
        builder.Append('.');
        builder.Append(random.GetInt(0, 255));
        builder.Append('.');
        builder.Append(random.GetInt(0, 255));
        builder.Append('.');
        builder.Append(random.GetInt(0, 255));

        return builder.ToString();
    }

    protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
    {
        return "0.0.0.0";
    }
}