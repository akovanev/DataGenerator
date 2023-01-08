namespace Akov.DataGenerator.Generators;

// ReSharper disable once InconsistentNaming
public class DefaultTypeValueGeneratorFactory : IGeneratorFactory
{
    private static readonly DefaultTypeValueGenerator Generator = new();

    public GeneratorBase Get(string type)
        => Generator;
}