﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Profiles;

public class DataSchemeTypeBuilder<TType>(PropertyInfo[] propertyInfos) : IPropertiesCollection
{
    public List<Property> Properties { get; } = propertyInfos.Select(GetBy).ToList();

    internal AssignGenerator<TType> AssignGenerator { get; } = new();
    
    public PropertyBuilder<TType> Property<TProp>(Expression<Func<TType, TProp>> expression)
    {
        string propertyName = ((MemberExpression) expression.Body).Member.Name;
        var property = Properties.Single(p => p.Name == propertyName);
        return new PropertyBuilder<TType>(this, property);
    }

    public DataSchemeTypeBuilder<TType> Ignore<TProp>(Expression<Func<TType, TProp>> expression)
    {
        string propertyName = ((MemberExpression) expression.Body).Member.Name;
        Properties.Remove(Properties.Single(p => p.Name == propertyName));
        return this;
    }
    
    internal void Assign(string propertyName, Expression<Func<TType, object>> expression)
    {
        AssignGenerator.AddProperty(propertyName, expression);
    }
    
    private static Property GetBy(PropertyInfo propertyInfo)
    {
        var property = new Property
        {
            Name = propertyInfo.Name,
            Type = propertyInfo.PropertyType.GetPropertyTemplateType()
        };

        property.Pattern = property.Type switch
        {
            TemplateType.Set => string.Join(",", Enum.GetNames(propertyInfo.PropertyType)),
            TemplateType.Array => propertyInfo.PropertyType.GetArrayPatternTemplateType(),
            TemplateType.Object => propertyInfo.PropertyType.Name,
            _ => default
        };

        return property;
    }
}