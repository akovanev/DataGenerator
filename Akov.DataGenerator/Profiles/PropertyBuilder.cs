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
    
    public PropertyBuilder<TType> Failure(double? nullable = null, double? custom = null, double? range = null, string? customFailure = null)
    {
        _property.Failure = new Failure {Nullable = nullable, Custom = custom, Range = range};
        _property.CustomFailure = customFailure;
        return this;
    }

    public PropertyBuilder<TType> FromFile(string? filename)
    {
        _property.Type = TemplateType.File;
        _property.Pattern = filename;
        return this;
    }
    
    public PropertyBuilder<TType> FromList(string[] values)
    {
        _property.Type = TemplateType.Set;
        _property.Pattern = string.Join(",", values);
        return this;
    }
   
    public PropertyBuilder<TType> FromResource(string resourceName)
    {
        _property.Type = TemplateType.Resource;
        _property.Pattern = resourceName;
        return this;
    }
    
    public PropertyBuilder<TType> HasJsonName(string? name)
    {
        _property.Name = name;
        return this;
    }
    
    public PropertyBuilder<TType> IsCalc()
    {
        _property.Type = TemplateType.Calc;
        return this;
    }

    public PropertyBuilder<TType> Assign(Expression<Func<TType, object>> expression)
    {
        _property.Type = TemplateType.Assign;
        _parent.Assign(_property.Name!, expression);
        return this;
    }
    
    public PropertyBuilder<TType> UseGenerator(string name)
    {
        _property.Type = name;
        return this;
    }

    public PropertyBuilder<TType> Length(int? min, int? max)
    {
        _property.MinLength = min;
        return Length(max);
    }
    
    public PropertyBuilder<TType> Length(int? max)
    {
        _property.MaxLength = max;
        return this;
    }
    
    public PropertyBuilder<TType> Pattern(string? pattern)
    {
        _property.Pattern = pattern;
        return this;
    }
    
    public PropertyBuilder<TType> Property<TProp>(Expression<Func<TType, TProp>> expression)
        => _parent.Property(expression);
    
    public PropertyBuilder<TType> Range(object? min, object? max)
    {
        _property.MinValue = min;
        return Range(max);
    }
    
    public PropertyBuilder<TType> Range(object? max)
    {
        _property.MaxValue = max;
        return this;
    }
    
    public PropertyBuilder<TType> Spaces(int? min, int? max)
    {
        _property.MinSpaceCount = min;
        return Spaces(max);
    }
    
    public PropertyBuilder<TType> Spaces(int? max)
    {
        _property.MaxSpaceCount = max;
        return this;
    }
    
    public PropertyBuilder<TType> SubTypePattern(string? pattern)
    {
        _property.SubTypePattern = pattern;
        return this;
    }
}