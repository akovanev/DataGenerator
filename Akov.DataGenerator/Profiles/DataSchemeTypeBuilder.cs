using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Profiles;

public class DataSchemeTypeBuilder<TType> : IPropertiesCollection
{
    public List<Property> Properties { get; } = new();
    
    public PropertyBuilder<TType> ForProperty<TProp>(Expression<Func<TType, TProp>> expression)
    {
        var property = new Property
        {
            Name = ((MemberExpression) expression.Body).Member.Name,
        };
        
        Properties.Add(property);

        return new PropertyBuilder<TType>(this, property);
    }
}