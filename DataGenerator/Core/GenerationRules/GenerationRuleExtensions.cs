namespace Akov.DataGenerator.Core.GenerationRules;

internal static class GenerationRuleExtensions
{
    public static string GetRandomGenerationRuleFor(this List<GenerationRule> rules, Property property)
    {
        var random = Dependencies.Factories.RandomFactory.GetOrCreate(property);
        double value = random.NextDouble();
        return rules.BuildRanges().First(f => f.Value.In(value)).Key;
    }

    private static Dictionary<string, Range> BuildRanges(this List<GenerationRule> rules)
    {
        var rulesWithRanges = new Dictionary<string, Range>();

        double[] probabilities = rules.Select(g => g.Probability).ToArray();
        if (probabilities.Any(x => x is < 0 or > 1))
            throw new ArgumentException("Probability should not be less than zero or greater than 1");

        double noneProbability = 1 - probabilities.Sum();
        if (noneProbability < 0)
            throw new ArgumentException("Sum of all probabilities should not be greater than 1");
        
        double sum = noneProbability;
        rulesWithRanges[InternalRuleNames.None] = new Range(0, sum);
        for (int i = 0; i < probabilities.Length; i++)
        {
            rulesWithRanges.Add(rules[i].RuleName, new Range(sum, sum + probabilities[i]));
            sum += probabilities[i];
        }

        return rulesWithRanges;
    }
}