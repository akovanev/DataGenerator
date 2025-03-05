using System.Linq.Expressions;

namespace Akov.DataGenerator.Utilities;

internal static class ConvertExtensions
{
    public static Expression<Func<object, object?>> ConvertExpressionReturningNullableObject<T>(this Expression<Func<T, object?>> expression)
    {
        var param = Expression.Parameter(typeof(object), "obj");
        var body = Expression.Invoke(expression, Expression.Convert(param, typeof(T)));
        return Expression.Lambda<Func<object, object?>>(body, param);
    }
}