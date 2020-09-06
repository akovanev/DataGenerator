using System.Collections.Generic;
using Akov.DataGenerator.Attributes;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests.DgModels
{
    public class DgStudentCollection
    {
        public const int Length = 100;

        [DgCalc]
        public int? Count { get; set; }

        [DgLength(Max = Length)]
        public List<DgStudent>? Students { get; set; }
    }
}
