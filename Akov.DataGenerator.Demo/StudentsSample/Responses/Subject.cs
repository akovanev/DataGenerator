using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Akov.DataGenerator.Demo.StudentsSample.Responses
{
    public class Subject : Result
    {
        [JsonProperty("encoded_description")]
        public string? EncodedDescription { get; set; }

        public int Attempts { get; set; }

        public bool IsPassed { get; set; }

        [JsonProperty("total_prices")]
        public List<double>? TotalPrices { get; set; }
    
        [OnError]
        internal void OnError(StreamingContext context, ErrorContext errorContext)
        {
            errorContext.Handled = true;
        }

    }
}