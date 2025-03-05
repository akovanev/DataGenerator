using Akov.DataGenerator.Core;
using Akov.DataGenerator.Core.Constants;
using Akov.DataGenerator.Generators;

namespace DataGenerator.Demo.Console;

public class DomainGenerator : StringGenerator
{
    public override string CreateRandomValue(Property property)
    {
        property.SetValueRule(ValueRules.Template, "@[resource:Domains]");
        return base.CreateRandomValue(property);
    }
}