using System.Collections.Generic;
using Akov.DataGenerator.Attributes;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests.DgModels
{
    /// <summary>
    /// Represents the data generation process for the StudentCollection response model.
    /// </summary>
    public class DgStudentCollection
    {
        public const int Length = 100;

        [DgCalc]
        public int? Count { get; set; }

        [DgLength(Min = Length, Max = Length)]
        public List<DgStudent>? Students { get; set; }
    }
}
