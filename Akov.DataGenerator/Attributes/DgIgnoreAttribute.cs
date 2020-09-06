using System;

namespace Akov.DataGenerator.Attributes
{
    /// <summary>
    /// If the property has this attribute
    /// then it will be excluded from the data generation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DgIgnoreAttribute : Attribute
    {
    }
}