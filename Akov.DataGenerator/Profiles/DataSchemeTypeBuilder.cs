using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Profiles;

public class DataSchemeTypeBuilder<TType> : IPropertiesCollection
{
    public DataSchemeTypeBuilder(PropertyInfo[] propertyInfos)
    {
        Properties = propertyInfos.Select(GetBy).ToList();
    }
    
    public List<Property> Properties { get; }
    
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