using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Randoms;
using Akov.DataGenerator.Utilities;

namespace Akov.DataGenerator.Dependencies;

internal static class Factories
{
    public static IGeneratorFactory GeneratorFactory { get; set; } = new GeneratorFactory();
    public static IRandomFactory RandomFactory { get; set; } = new RandomFactory();
    public static Lazy<FileHelper> FileHelper { get; set; } = new(() => new FileHelper());
}