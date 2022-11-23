using System;

namespace Akov.DataGenerator.Attributes;

/// <summary>
/// Represents the value for a custom failure.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class DgCustomFailureAttribute : Attribute
{
    public DgCustomFailureAttribute(string value)
    {
        Value = value;
    }
    
    public string Value { get; }
}