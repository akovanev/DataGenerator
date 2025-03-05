using Akov.DataGenerator.Core;
using Akov.DataGenerator.Core.Constants;

namespace Akov.DataGenerator.Generators;

public class EmailGenerator : StringGenerator
{
    public override string CreateRandomValue(Property property)
    {
        char delimiter = new Random().Next(0,2) == 1 ? '.' : '-';
        
        property.SetValueRule(
            ValueRules.Template, 
            $"[resource:Firstnames]{delimiter}[resource:Lastnames]@[resource:Domains]");

        return base.CreateRandomValue(property).Replace(" ", "").ToLower();
    }
}