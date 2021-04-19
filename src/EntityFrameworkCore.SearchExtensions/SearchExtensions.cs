using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using static System.Linq.Expressions.Expression;

[assembly: CLSCompliant(true)]
namespace System.Linq
{
    public static class SearchExtensions
    {
        private static readonly Expression functions = Property(null, typeof(EF).GetProperty(nameof(EF.Functions)));
        private static readonly MethodInfo like = typeof(DbFunctionsExtensions)
            .GetMethod(nameof(DbFunctionsExtensions.Like), new Type[] { functions.Type, typeof(string), typeof(string) });

        public static IQueryable<T> Search<T, TProperty>(
            this IQueryable<T> source,
            Expression<Func<T, TProperty>> properptySelector,
            string searchTerm)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (properptySelector is null)
            {
                throw new ArgumentNullException(nameof(properptySelector));
            }

            if (properptySelector.Body.NodeType != ExpressionType.MemberAccess
                || properptySelector.Body is not MemberExpression propertySelectorMemberExpression
                || propertySelectorMemberExpression.Member is not PropertyInfo)
            {
                throw new ArgumentException(
                    $"'{nameof(properptySelector)}' must be a property access expression.",
                    nameof(properptySelector));
            }

            if (string.IsNullOrEmpty(searchTerm))
            {
                return source;
            }

            searchTerm = $"%{searchTerm}%";
            var itemParameter = properptySelector.Parameters[0];

            var expressionProperty = properptySelector.Body;

            if (typeof(TProperty) != typeof(string))
            {
                expressionProperty = Call(
                    expressionProperty,
                    typeof(object).GetMethod(nameof(object.ToString), Array.Empty<Type>()));
            }

            var selector = Call(
                null,
                like,
                functions,
                expressionProperty,
                Constant(searchTerm));

            return source.Where(Lambda<Func<T, bool>>(selector, itemParameter));
        }

        public static IQueryable<T> Search<T>(this IQueryable<T> source, string searchTerm)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(searchTerm))
            {
                return source;
            }

            var properties = typeof(T).GetProperties();

            if (properties.Length == 0)
            {
                return source;
            }

            searchTerm = $"%{searchTerm}%";

            Expression selector = null;

            var itemParameter = Parameter(typeof(T), "item");

            foreach (var property in properties)
            {
                Expression expressionProperty = Property(itemParameter, property.Name);

                if (property.PropertyType != typeof(string))
                {
                    expressionProperty = Call(
                        expressionProperty,
                        typeof(object).GetMethod(nameof(object.ToString), Array.Empty<Type>()));
                }

                var newSelector = Call(
                    null,
                    like,
                    functions,
                    expressionProperty,
                    Constant(searchTerm));

                if (selector is null)
                {

                    selector = newSelector;
                }
                else
                {
                    selector = Or(selector, newSelector);
                }
            }

            return source.Where(Lambda<Func<T, bool>>(selector, itemParameter));
        }
    }
}
