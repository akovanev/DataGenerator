using System;
using System.Linq.Expressions;
using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Profiles;

public class PropertyBuilder<TType>
{
    private readonly DataSchemeTypeBuilder<TType> _parent;
    private readonly Property _property;

    public PropertyBuilder(DataSchemeTypeBuilder<TType> parent, Property property)
    {
        _property = property;
        _parent = parent;
    }

    public PropertyBuilder<TType> WithValue(Property property)
    {
        if (!string.IsNullOrEmpty(property.Name))
        {
            _property.Name = property.Name;
        }
        _property.Type = property.Type;
        _property.Pattern = property.Pattern;
        _property.SubTypePattern = property.SubTypePattern;
        _property.SequenceSeparator = property.SequenceSeparator;
        _property.MinLength = property.MinLength;
        _property.MaxLength = property.MaxLength;
        _property.MinSpaceCount = property.MinSpaceCount;
        _property.MaxSpaceCount = property.MaxSpaceCount;
        _property.MinValue = property.MinValue;
        _property.MaxValue = property.MinValue;
        return this;
    }

    public PropertyBuilder<TType> WithFailure(Failure failure, string? customFailure = null)
    {
        _property.Failure = failure;
        _property.CustomFailure = customFailure;
        return this;
    }
    
    public PropertyBuilder<TType> ForProperty<TProp>(Expression<Func<TType, TProp>> expression)
        => _parent.ForProperty(expression);
}