using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Akov.DataGenerator.Demo.StudentsSample.Responses;

/// <summary>
/// The Subject response model.
/// </summary>
public class Subject : Result
{
    [JsonProperty("encoded_description")]
    public string? EncodedDescription { get; set; }

    public int Attempts { get; set; }

    public bool IsPassed { get; set; }

    [JsonProperty("total_prices")]
    public List<double>? TotalPrices { get; set; }

    /// <summary>
    /// Handles errors while JsonConvert parsing (deserializing).
    /// </summary>
    [OnError]
    internal void OnError(StreamingContext context, ErrorContext errorContext)
    {
        //Allows to proceed without throwing an exception.
        errorContext.Handled = true;
    }
}