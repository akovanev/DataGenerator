using System;

namespace Akov.DataGenerator.Attributes;

/// <summary>
/// Represents the separator for sets and files.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class DgSequenceSeparatorAttribute(string value) : Attribute
{
    public string Value { get; } = value;
}