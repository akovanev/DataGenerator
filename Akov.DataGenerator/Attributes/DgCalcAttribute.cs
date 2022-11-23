using System;

namespace Akov.DataGenerator.Attributes;

/// <summary>
/// Considered a property to be calculated.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class DgCalcAttribute : Attribute
{
}