using System;

namespace Akov.DataGenerator.Attributes;

/// <summary>
/// Specifies the name of the custom generator.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class DgGeneratorAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}