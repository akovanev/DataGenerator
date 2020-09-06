using System.Collections.Generic;

namespace Akov.DataGenerator.Demo.StudentsSample.Responses
{
    /// <summary>
    /// The StudentCollection response model.
    /// </summary>
    public class StudentCollection
    {
        public int? Count { get; set; }

        public List<Student>? Students { get; set; }
    }
}
