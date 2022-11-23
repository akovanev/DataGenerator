using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Akov.DataGenerator.Demo.StudentsSample.Responses
{
    /// <summary>
    /// The Student response model.
    /// </summary>
    public class Student : Result
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }
        
        public int Year { get; set; }
        
        public Address? CompanyAddress { get; set; }

        [JsonProperty("test_variant")]
        public Variant Variant { get; set; }

        [JsonProperty("test_answers")]
        public int[]? TestAnswers { get; set; }

        [JsonProperty("encoded_solution")]
        public string? EncodedSolution { get; set; }

        [JsonProperty("last_updated")]
        public DateTime LastUpdated { get; set; }

        public List<Subject>? Subjects { get; set; }

        public Decimal Discount { get; set; }
        
        public byte[]? Signature { get; set; }

        /// <summary>
        /// The object is valid only if all inner objects are parsed correctly.
        /// </summary>
        public override bool IsValid => base.IsValid &&
            (CompanyAddress is null || CompanyAddress.IsValid) &&
            (Subjects is null || Subjects.All(s => s.IsValid));

        /// <summary>
        /// Handles errors while JsonConvert parsing (deserializing).
        /// </summary>
        [OnError]
        internal void OnError(StreamingContext context, ErrorContext errorContext)
        {
            if ("last_updated" == GetErrorKeyProperty(errorContext.Path))
            {
                LastUpdated = DateTime.Today;
                AddWarning(errorContext);
            }
            else
            {
                AddError(errorContext);
            }

            //Allows to proceed without throwing an exception.
            errorContext.Handled = true;
        }
    }
}
