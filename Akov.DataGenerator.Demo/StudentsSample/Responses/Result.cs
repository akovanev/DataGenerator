using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace Akov.DataGenerator.Demo.StudentsSample.Responses;

/// <summary>
/// The base class for response models or dtos.
/// Adds logic defining whether the object was parsed correctly from http response.
/// Contains several protected methods tightening it with JsonConvert class, which is okay for this example.
/// </summary>
public abstract class Result
{
    public virtual bool IsValid => ParsingErrors.Count == 0;
    public virtual bool HasWarnings => ParsingWarnings.Count > 0;

    public Dictionary<string, string> ParsingErrors { get; set; } = new Dictionary<string, string>();
    public Dictionary<string, string> ParsingWarnings { get; set; } = new Dictionary<string, string>();

    protected virtual void AddError(ErrorContext errorContext)
    {
        string errorKey = GetErrorKey(errorContext.Path);
        string errorValue = errorContext.Error.Message;
        ParsingErrors.Add(errorKey, errorValue);
    }

    protected virtual void AddWarning(ErrorContext errorContext)
    {
        string errorKey = GetErrorKey(errorContext.Path);
        string errorValue = errorContext.Error.Message;
        ParsingWarnings.Add(errorKey, errorValue);
    }

    protected static string GetErrorKey(string path)
        => path.Split(".").Last();

    protected static string GetErrorKeyProperty(string path)
    {
        string errorKey = GetErrorKey(path);
        int arrayIndex = errorKey.IndexOf("[", StringComparison.Ordinal);
        return arrayIndex > 0
            ? errorKey[..arrayIndex]
            : errorKey;
    }
}