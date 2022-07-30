using System;
using System.Collections.Generic;
using System.Reflection;
using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Profiles;

public abstract class DgProfileBase
{
    private readonly Dictionary<Type, IPropertiesCollection> _typePropertiesCollections = new();

    protected DataSchemeTypeBuilder<T> ForType<T>()
    {
        var typeBuilder = new DataSchemeTypeBuilder<T>();
        _typePropertiesCollections.Add(typeof(T), typeBuilder);
        return typeBuilder;
    }

    public DataScheme GetDataScheme<T>()
    {
        Type type = typeof(T);
        
        var propsDictionary = new Dictionary<Type, PropertyInfo[]>();
        type.AddToPropsDictionary(propsDictionary);

        var definitions = new List<Definition>();
        foreach (var (key, value) in propsDictionary)
        {
            if (!_typePropertiesCollections.ContainsKey(key))
                throw new ArgumentException($"Profile for type {key.FullName} not defined.");
                    
            definitions.Add(new Definition(key.Name, _typePropertiesCollections[key].Properties));
        }

        return new DataScheme(type.Name, definitions);
    }
}