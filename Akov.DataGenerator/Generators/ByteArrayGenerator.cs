using System;
using System.Text;
using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Models;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Generators;

public class ByteArrayGenerator : GeneratorBase
{
    private const int DefaultMinLength = 10;
    private const int DefaultMaxLength = 20;
    
    protected override object CreateImpl(PropertyObject propertyObject)
    {
        Property property = propertyObject.Property;
        int minLength = property.MinLength ?? DefaultMinLength;
        int maxLength = property.MaxLength ?? DefaultMaxLength;

        Random random = GetRandomInstance(propertyObject);
        int length = random.GetInt(minLength, maxLength);
        
        return CreateRandomByteArray(length);
    }

    protected override object CreateRangeFailureImpl(PropertyObject propertyObject)
    {
        Property property = propertyObject.Property;
        int minLength = property.MinLength ?? DefaultMinLength;
        int maxLength = property.MaxLength ?? DefaultMaxLength;
            
        Random random = GetRandomInstance(propertyObject, nameof(CreateRangeFailureImpl));

        int length = minLength > 1 && random.GetInt(0, 1) == 0
            ? random.GetInt(0, minLength - 1)
            : random.GetInt(maxLength + 1, maxLength * 2);

        return CreateRandomByteArray(length);
    }

    private static object CreateRandomByteArray(int length)
    {
        var bytes = new byte[length];
        new Random().NextBytes(bytes);
        return Convert.ToBase64String(bytes);
    }
}