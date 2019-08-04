using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LibCore;

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
                var equal = Expression.NotEqual(dProp, tProp);
                var assign = Expression.Assign(dProp, tProp);
                var check = Expression.IfThen(equal, assign);
                return check;
            });
            var block = Expression.Block(new [] {source, dest}, expressions);
            var lambda = Expression.Lambda<Updater<T>>(block, source, dest);
            return lambda.Compile();
        }

        public static Expression<Func<T, bool>> CreateSameObjectChecker<T>(T value)
        {
            var typeT = typeof(T);

            var constant = Expression.Constant(value);
            var param = Expression.Parameter(typeT, "x");

            var properties = typeT.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.CanRead 
                            && x.CanWrite 
                            && x.Name != nameof(IPrimaryKeyContainer.Id) 
                            && !(x.SetMethod.IsVirtual)).ToArray();

            if (properties.Length == 0)
                return x => false;

            var propertyDB = Expression.Property(param, properties[0]);
            var propertyConst = Expression.Property(constant, properties[0]);
            var predicate = Expression.Equal(propertyDB, propertyConst);
            foreach (var propertyInfo in properties.Skip(1))
            {
                propertyDB = Expression.Property(param, propertyInfo);
                propertyConst = Expression.Property(constant, propertyInfo);
                var additional = Expression.Equal(propertyDB, propertyConst);
                predicate = Expression.AndAlso(predicate, additional);
            }
            var lambda = Expression.Lambda<Func<T, bool>>(predicate, param);

            return lambda;
        }
    }
}
