using System;
using System.Linq.Expressions;
using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Profiles;

public class PropertyBuilder<TType>(DataSchemeTypeBuilder<TType> parent, Property property)
{
    public PropertyBuilder<TType> Failure(double? nullable = null, double? custom = null, double? range = null, string? customFailure = null)
    {
        property.Failure = new Failure {Nullable = nullable, Custom = custom, Range = range};
        property.CustomFailure = customFailure;
        return this;
    }

    public PropertyBuilder<TType> FromFile(string? filename)
    {
        property.Type = TemplateType.File;
        property.Pattern = filename;
        return this;
    }
    
    public PropertyBuilder<TType> FromList(string[] values)
    {
        property.Type = TemplateType.Set;
        property.Pattern = string.Join(",", values);
        return this;
    }
   
    public PropertyBuilder<TType> FromResource(string resourceName)
    {
        property.Type = TemplateType.Resource;
        property.Pattern = resourceName;
        return this;
    }
    
    public PropertyBuilder<TType> HasJsonName(string? name)
    {
        property.Name = name;
        return this;
    }
    
    public PropertyBuilder<TType> IsCalc()
    {
        property.Type = TemplateType.Calc;
        return this;
    }

    public PropertyBuilder<TType> Assign(Expression<Func<TType, object>> expression)
    {
        property.Type = TemplateType.Assign;
        parent.Assign(property.Name!, expression);
        return this;
    }
    
    public PropertyBuilder<TType> UseGenerator(string name)
    {
        property.Type = name;
        return this;
    }

    public PropertyBuilder<TType> Length(int? min, int? max)
    {
        property.MinLength = min;
        return Length(max);
    }
    
    public PropertyBuilder<TType> Length(int? max)
    {
        property.MaxLength = max;
        return this;
    }
    
    public PropertyBuilder<TType> Pattern(string? pattern)
    {
        property.Pattern = pattern;
        return this;
    }
    
    public PropertyBuilder<TType> Property<TProp>(Expression<Func<TType, TProp>> expression)
        => parent.Property(expression);
    
    public PropertyBuilder<TType> Range(object? min, object? max)
    {
        property.MinValue = min;
        return Range(max);
    }
    
    public PropertyBuilder<TType> Range(object? max)
    {
        property.MaxValue = max;
        return this;
    }
    
    public PropertyBuilder<TType> Spaces(int? min, int? max)
    {
        property.MinSpaceCount = min;
        return Spaces(max);
    }
    
    public PropertyBuilder<TType> Spaces(int? max)
    {
        property.MaxSpaceCount = max;
        return this;
    }
    
    public PropertyBuilder<TType> SubTypePattern(string? pattern)
    {
        property.SubTypePattern = pattern;
        return this;
    }
}