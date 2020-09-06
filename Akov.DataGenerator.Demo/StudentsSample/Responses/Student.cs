using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Akov.DataGenerator.Demo.StudentsSample.Responses
{
    public class Student : Result
    {
        public Guid? Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        [JsonProperty("test_variant")]
        public Variant Variant { get; set; }

        [JsonProperty("test_answers")]
        public int[]? TestAnswers { get; set; }

        [JsonProperty("encoded_solution")]
        public string? EncodedSolution { get; set; }

        [JsonProperty("last_updated")]
        public DateTime? LastUpdated { get; set; }

        public List<Subject>? Subjects { get; set; }

        public Subject? Subject { get; set; }
    }
}
