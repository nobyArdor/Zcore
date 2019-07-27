using System;
using System.Linq.Expressions;

namespace Zcore.Tools
{
    public static class LambdaExpressionGenerator
    {
        public static Expression<Func<T, bool>> CaptureFuncParameter<T>(string propertyName, object value)
        {
            var constant = Expression.Constant(value);
            var param = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(param, propertyName);
            var predicate = Expression.Equal(property, constant);
            var  lambda = Expression.Lambda<Func<T, bool>>(predicate);
            return lambda;
        }
    }
}
