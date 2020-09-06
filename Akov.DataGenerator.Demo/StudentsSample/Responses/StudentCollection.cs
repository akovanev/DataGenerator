using System.Collections.Generic;

namespace Akov.DataGenerator.Demo.StudentsSample.Responses
{
    public class StudentCollection : Result
    {
        public int? Count { get; set; }

        public List<Student>? Students { get; set; }
    }
}
