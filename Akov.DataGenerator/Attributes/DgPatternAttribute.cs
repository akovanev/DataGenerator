using System;

namespace Akov.DataGenerator.Attributes;

/// <summary>
/// Represents the pattern.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class DgPatternAttribute(string value) : Attribute
{
    public string Value { get; } = value;
}

/// <summary>
/// Represents the sub pattern.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class DgSubTypePatternAttribute(string value) : DgPatternAttribute(value);