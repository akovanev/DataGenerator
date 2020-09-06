using System.Collections.Generic;
using Newtonsoft.Json;

namespace Akov.DataGenerator.Demo.StudentsSample.Responses
{
    public class Subject : Result
    {
        [JsonProperty("encoded_description")]
        public string? EncodedDescription { get; set; }

        public int Attempts { get; set; }

        public bool IsPassed { get; set; }

        [JsonProperty("total_price")]
        public List<double>? TotalPrices { get; set; }
    }
}