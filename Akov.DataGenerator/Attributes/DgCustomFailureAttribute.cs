using System;

namespace Akov.DataGenerator.Attributes;

/// <summary>
/// Represents the value for a custom failure.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class DgCustomFailureAttribute(string value) : Attribute
{
    public string Value { get; } = value;
}