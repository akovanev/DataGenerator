using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Akov.DataGenerator.Demo.StudentsSample.Responses
{
    public class Student : Result
    {
        public Guid Id { get; set; }

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
        public DateTime LastUpdated { get; set; }

        public List<Subject>? Subjects { get; set; }

        public Subject? Subject { get; set; }

        public override bool IsValid => base.IsValid &&
            (Subject is null || Subject.IsValid) &&
            (Subjects is null || Subjects.All(s => s.IsValid));

        [OnError]
        internal void OnError(StreamingContext context, ErrorContext errorContext)
        {
            if ("last_updated" != GetErrorKeyProperty(errorContext.Path))
            {
                AddError(errorContext);
            }
            else
            {
                LastUpdated = DateTime.Today;
                AddWarning(errorContext);
            }
            
            errorContext.Handled = true;
        }
    }
}
