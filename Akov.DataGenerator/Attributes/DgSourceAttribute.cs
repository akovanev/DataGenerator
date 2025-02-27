using System;

namespace Akov.DataGenerator.Attributes;

/// <summary>
/// Represents the source for the property.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class DgSourceAttribute(string path, bool embedded = false) : Attribute
{
    public string Path { get; } = path;

    public bool Embedded { get; } = embedded;
}