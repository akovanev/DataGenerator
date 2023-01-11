using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Profiles;

public abstract class DgProfileBase
{
    private readonly Dictionary<Type, IPropertiesCollection> _typePropertiesCollections = new();
    private readonly Dictionary<Type, AssignGeneratorBase> _assignGeneratorsCollection = new();
    private readonly bool _createDefaultProfilesForMissingTypes;

    protected DgProfileBase(bool createDefaultProfilesForMissingTypes = false)
    {
        _createDefaultProfilesForMissingTypes = createDefaultProfilesForMissingTypes;
    }

    protected DataSchemeTypeBuilder<T> ForType<T>()
    {
        Type type = typeof(T);
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var typeBuilder = new DataSchemeTypeBuilder<T>(properties);
        _typePropertiesCollections.Add(type, typeBuilder);
        _assignGeneratorsCollection.Add(type, typeBuilder.AssignGenerator);
        return typeBuilder;
    }
    
    internal DataScheme GetDataScheme<T>()
    {
        Type type = typeof(T);
        
        var propsDictionary = new Dictionary<Type, PropertyInfo[]>();
        type.AddToPropsDictionary(propsDictionary);

        var definitions = new List<Definition>();
        foreach (var (key, value) in propsDictionary)
        {
            if (!_typePropertiesCollections.ContainsKey(key))
            {
                if(!_createDefaultProfilesForMissingTypes)
                    throw new ArgumentException($"Profile for type {key.FullName} not defined.");
                
                RegisterType(key);
            }
                    
            definitions.Add(new Definition(key.Name, _typePropertiesCollections[key].Properties));
        }

        return new DataScheme(type.Name, definitions);
    }

    internal IReadOnlyCollection<AssignGeneratorBase> GetAssignGenerators()
        => new ReadOnlyCollection<AssignGeneratorBase>(_assignGeneratorsCollection.Values.ToList());
    
    private void RegisterType(Type type)
    {
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var builderGenericType = typeof(DataSchemeTypeBuilder<>);
        var builderInstance = Activator.CreateInstance(builderGenericType.MakeGenericType(type), (object)properties);
        if (builderInstance is null)
            throw new InvalidOperationException($"Cannot register type {type.FullName}");
        _typePropertiesCollections.Add(type, (IPropertiesCollection)builderInstance);
    }
}