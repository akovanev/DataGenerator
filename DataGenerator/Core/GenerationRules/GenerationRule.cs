namespace Akov.DataGenerator.Core.GenerationRules;

/// <summary>
/// Defines a rule for generating values with an associated probability.
/// </summary>
/// <remarks>
/// The probability of the main generation flow (Pm) is calculated as:  
/// Pm = 1 - Sum(Pi), where Pi represents the probability of each individual generation rule.
/// </remarks>
/// <param name="RuleName">The name identifier of the generation rule.</param>
/// <param name="Probability">The probability assigned to this rule, influencing its likelihood of being selected.</param>
/// <param name="CreateValue">
/// A function that takes a <see cref="Property"/> as input and generates an optional value.
/// </param>
public record GenerationRule(string RuleName, double Probability, Func<Property, object?> CreateValue);

internal static class InternalRuleNames
{
    public const string None = "None";
    public const string WithNulls = "Nulls";
}