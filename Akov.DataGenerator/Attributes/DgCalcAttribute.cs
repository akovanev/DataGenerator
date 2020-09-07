using System;

namespace Akov.DataGenerator.Attributes
{
    /// <summary>
    /// Considered a property to be calculated.
    /// The details can be found on https://akovanev.com/2020/08/31/calculated-properties-with-data-generator/
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DgCalcAttribute : Attribute
    {
    }
}