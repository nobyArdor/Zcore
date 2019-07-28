using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Zcore.Tools
{
    public static class LambdaExpressionT
    {
        public delegate void Updater<T>(T source, T dest);
        public static Expression<Func<T, bool>> CaptureFuncParameter<T>(string propertyName, object value)
        {
            var constant = Expression.Constant(value);
            var param = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(param, propertyName);
            var predicate = Expression.Equal(property, Expression.Convert( constant, property.Type));
            var  lambda = Expression.Lambda<Func<T, bool>>(predicate, param);
            return lambda;
        }



        public static Updater<T> CreateUpdater<T>()
        {
            var typeT = typeof(T);
            var source = Expression.Parameter(typeof(T), "source");
            var dest = Expression.Parameter(typeof(T), "destination");

            //TODO придумать более элегантный подход обхода EntityProxyGetter

            var properties = typeT.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.CanRead && x.CanWrite && !(x.SetMethod.IsVirtual));
                ;
            IEnumerable<Expression> expressions = properties.Select(x =>
            {
                var dProp = Expression.Property(dest, x);
                var tProp = Expression.Property(source, x);
                var equal = Expression.Equal(dProp, tProp);
                var assign = Expression.Assign(dProp, tProp);
                var check = Expression.IfThen(equal, assign);

                return check;
            });
            var block = Expression.Block(new [] {source, dest}, expressions);
            var lambda = Expression.Lambda<Updater<T>>(block, source, dest);
            return lambda.Compile();
        }
    }
}
