using LinqKit;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace AgentCheckApi;

public static class QueryableExtensions
{
    public static Expression<Func<T, bool>> BuildBinaryPredicateQuery<T>(
        this IQueryable<T> query,
        string columnName,
        string searchTerm,
        ExpressionType comparisonType = ExpressionType.Equal)
        where T : class
    {
        var parameter = Expression.Parameter(typeof(T), "e");
        var property = Expression.Property(parameter, columnName);
        var constant = Expression.Constant(searchTerm);

        Expression comparison = Expression.MakeBinary(comparisonType, property, constant);

        Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);

        // Apply PredicateBuilder and return the query
        return lambda;
    }


    // Predicate Builder for EF.Like
    public static Expression<Func<T, bool>> BuildLike<T>(string propertyName, string pattern)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);
        var method = typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.Like), new[] { typeof(DbFunctions), typeof(string), typeof(string) });

        var likeExpression = Expression.Call(method, Expression.Constant(EF.Functions), property, Expression.Constant(pattern));
        return Expression.Lambda<Func<T, bool>>(likeExpression, parameter);
    }
    public static IQueryable<T> ApplyLikePredicate<T>(this IQueryable<T> source, string propertyName, string pattern)
    {
        var predicate = BuildLike<T>(propertyName, pattern);
        return source.Where(predicate);
    }



    public static IQueryable<T> BuildStringQuery<T>(this IQueryable<T> source, string column, string filterType, string filterValue)
    {

        return filterType switch
        {
            "startswith" => source.ApplyLikePredicate(column, $"{filterValue}%"),
            "endswith" => source.ApplyLikePredicate(column, $"%{filterValue}"),
            "equals" => source.ApplyLikePredicate(column, filterValue),
            //"does not equal" => source.Where(source.BuildBinaryPredicateQuery( column, filterValue, ExpressionType.NotEqual)), 
            "blank" => source.ApplyLikePredicate(column, null),
            "notblank" => source.ApplyLikePredicate(column, $"%"),
            "contains" => source.ApplyLikePredicate(column, $"%{filterValue}%"),
            "notcontains" => source.ApplyLikePredicate(column, $"%{filterValue}%")
        };
    }

}
