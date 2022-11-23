using System;

namespace Akov.DataGenerator.Attributes;

/// <summary>
/// Represents the separator for sets and files.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class DgSequenceSeparatorAttribute : Attribute
{
    public DgSequenceSeparatorAttribute(string value)
    {
        Value = value;
    }
    
    public string Value { get; }
}