using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Profiles;

public class DataSchemeTypeBuilder<TType> : IPropertiesCollection
{
    public List<Property> Properties { get; } = new();
    
    public PropertyBuilder<TType> Property<TProp>(Expression<Func<TType, TProp>> expression)
    {
        var member = ((PropertyInfo)((MemberExpression) expression.Body).Member);
        var property = new Property
        {
            Name = member.Name,
            Type = member.PropertyType.GetPropertyTemplateType()
        };

        property.Pattern = property.Type switch
        {
            TemplateType.Set => string.Join(",", Enum.GetNames(member.PropertyType)),
            TemplateType.Array => member.PropertyType.GetArrayPatternTemplateType(),
            TemplateType.Object => member.PropertyType.Name,
            _ => default
        };

        Properties.Add(property);

        return new PropertyBuilder<TType>(this, property);
    }
}