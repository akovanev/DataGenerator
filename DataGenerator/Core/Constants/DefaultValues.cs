namespace Akov.DataGenerator.Core.Constants;

/// <summary>
/// The default settings.
/// </summary>
public static class DefaultValues
{
    public static Dictionary<string, object> Values { get; set; } = new()
    {
        { ValueRules.MinValue, 0 },
        { ValueRules.MaxValue, 1000 },
        { ValueRules.MinDateValue, new DateTime(2020, 1, 1) },
        { ValueRules.MaxDateValue, DateTime.Today },
        { ValueRules.MinLength, 10 },
        { ValueRules.MaxLength, 20 },
        { ValueRules.PhoneMask, "+45 ## ## ## ##"}
    };
}