using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Generators;

internal class AssignGenerator<T>: AssignGeneratorBase
{
    private readonly Dictionary<string, Expression<Func<T, object>>> _assignProperties = new();

    public override string Id => typeof(T).Name;

    public void AddProperty(string propertyName, Expression<Func<T, object>> expression)
    {
        if (_assignProperties.ContainsKey(propertyName))
            throw new InvalidOperationException("Expression for assign property can be defined only once");
        
        _assignProperties.Add(propertyName, expression);
    }

    protected override object CreateImpl(CalcPropertyObject propertyObject)
    {
        if (propertyObject.Property.Name is null)
            throw new ArgumentNullException(nameof(propertyObject.Property.Name));
        
        if (!_assignProperties.ContainsKey(propertyObject.Property.Name))
            throw new NotSupportedException("Not expected calculated property");
        
        var expression = _assignProperties[propertyObject.Property.Name];
        var compiledLambda = expression.Compile();
        return compiledLambda.DynamicInvoke(propertyObject.Cast<T>());
    }
    
    protected override object CreateRangeFailureImpl(CalcPropertyObject propertyObject)
    {
        throw new NotImplementedException();
    }
}