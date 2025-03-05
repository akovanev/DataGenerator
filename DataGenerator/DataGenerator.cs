using System.Collections;
using System.Reflection;
using Akov.DataGenerator.Core;
using Akov.DataGenerator.FluentSyntax;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Randoms;
using Akov.DataGenerator.Utilities;

namespace Akov.DataGenerator;

/// <summary>
/// Responsible for generating random data for a given type using specified rules and dependencies.
/// </summary>
public class SimpleDataGenerator
{
    private readonly DataScheme _dataScheme;

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleDataGenerator"/> class.
    /// </summary>
    /// <param name="scheme">The data scheme to use for property lookups (optional).</param>
    /// <param name="generatorFactory">The factory responsible for creating generators (optional).</param>
    /// <param name="randomFactory">The factory responsible for creating random instances (optional).</param>
    /// <param name="fileReadConfig">The configuration for file reading (optional).</param>
    public SimpleDataGenerator(
        DataScheme? scheme = null, 
        IGeneratorFactory? generatorFactory = null, 
        IRandomFactory? randomFactory = null,
        FileReadConfig? fileReadConfig = null)
    {
        if (generatorFactory is not null)
            Dependencies.Factories.GeneratorFactory = generatorFactory;

        if(randomFactory is not null)
            Dependencies.Factories.RandomFactory = randomFactory;
        
        if(fileReadConfig is not null)
            Dependencies.Factories.FileHelper = new Lazy<FileHelper>(() => new FileHelper(fileReadConfig));
        
        _dataScheme = scheme ?? new DataScheme();
    }

    /// <summary>
    /// Generates an instance of the specified type with randomly generated values for its properties.
    /// </summary>
    /// <typeparam name="T">The type of the object to generate.</typeparam>
    /// <returns>An instance of the specified type with random values for its properties.</returns>
    public T GenerateForType<T>() where T : new()
    {
        var obj = new T();
        var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var constructedProperties = new List<(PropertyInfo, Property)>();

        foreach (var propertyInfo in propertyInfos)
        {
            if (!propertyInfo.CanWrite) continue;
            if (propertyInfo.DeclaringType is null) throw new ArgumentNullException(nameof(propertyInfo), $"Declaring type is null for {propertyInfo.Name}");
            var property = CreateProperty(propertyInfo);
            if (property.Constructor is not null)
            {
                constructedProperties.Add((propertyInfo, property));
            }
            else
            {
                var randomValue = property.ExecuteDecorator(GenerateRandomValue(property));
                propertyInfo.SetValue(obj, randomValue);
            }
        }

        foreach (var (propertyInfo, property) in constructedProperties)
        {
            var randomValue = property.ExecuteDecorator(property.ExecuteConstructor(obj));
            propertyInfo.SetValue(obj, randomValue);
        }

        return obj;
    }

    private object? GenerateRandomValue(Property property)
    {
        if (property.IsBulk)
            return GenerateRandomList(property);

        if (property.IsObject)
            return GenerateRandomObject(property);
        
        var generator = Dependencies.Factories.GeneratorFactory.Get(property);
        return generator.CreateBoxedRandomValue(property);
    }

    private object? GenerateRandomObject(Property property)
        => typeof(SimpleDataGenerator)
            .GetMethod(nameof(SimpleDataGenerator.GenerateForType))
            ?.MakeGenericMethod(property.Type)
            .Invoke(new SimpleDataGenerator(_dataScheme, Dependencies.Factories.GeneratorFactory, Dependencies.Factories.RandomFactory), null);

    private object GenerateRandomList(Property property)
    {
        var (elementType, elementProperty) = CreateElementTypeProperty(property);
        var list = (IList?)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
        if (list is null)
            throw new InvalidOperationException($"Could not create list for type {elementType.Name}");
        
        int randomLength = new IntGenerator().CreateRandomValue(property);
        for (int i = 0; i < randomLength; i++)
        {
            var item = GenerateRandomValue(elementProperty);
            list.Add(item);
        }
        return list;
    }

    private Property CreateProperty(PropertyInfo propertyInfo)
    {
        if (propertyInfo.DeclaringType is null) throw new ArgumentNullException(nameof(propertyInfo), $"Declaring type is null for {propertyInfo.Name}");
        return _dataScheme.LookupFor(propertyInfo.DeclaringType, propertyInfo.Name) ?? Property.CreateProperty(propertyInfo);
    }

    private static (Type, Property) CreateElementTypeProperty(Property property)
    {
        var elementType = property.Type.GetGenericArguments()[0];
        return (elementType, Property.CreateElementTypeProperty(property, elementType));
    }
}
