using System.Collections.Generic;
using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Generators;

namespace Akov.DataGenerator.Demo.StudentsSampleTests.Tests.Generators;

/// <summary>
/// Extends the GeneratorFactory with the StudentCalcGenerator.
/// </summary>
public class StudentGeneratorFactory : GeneratorFactory
{
    public const string UintGenerator = "uint";
        
    protected override Dictionary<string, GeneratorBase> GetGeneratorDictionary()
    {
        Dictionary<string, GeneratorBase> generators = base.GetGeneratorDictionary();
        generators.Add(TemplateType.Calc, new StudentCalcGenerator());
        generators.Add(UintGenerator, new UIntGenerator());
        return generators;
    }
}