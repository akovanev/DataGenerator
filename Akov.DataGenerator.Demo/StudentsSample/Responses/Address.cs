using System.Runtime.Serialization;
using Newtonsoft.Json.Serialization;

namespace Akov.DataGenerator.Demo.StudentsSample.Responses;

/// <summary>
/// The Address response model.
/// </summary>
public class Address : Result
{
    public string? Company { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? AddressLine { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
        
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