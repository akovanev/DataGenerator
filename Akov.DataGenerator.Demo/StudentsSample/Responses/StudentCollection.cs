using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Serialization;

namespace Akov.DataGenerator.Demo.StudentsSample.Responses
{
    public class StudentCollection
    {
        public int? Count { get; set; }

        public List<Student>? Students { get; set; }

        
    }
}
