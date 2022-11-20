using System;

namespace Akov.DataGenerator.Attributes;

/// <summary>
/// Specifies the name of the custom generator.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class DgGeneratorAttribute : Attribute
{
    public DgGeneratorAttribute(string name)
    {
        Name = name;
    }
    
    public string Name { get; }
}