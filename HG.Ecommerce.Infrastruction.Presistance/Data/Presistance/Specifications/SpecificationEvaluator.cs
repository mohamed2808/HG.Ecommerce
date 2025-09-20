using HG.Ecommerce.Core.Common;
using HG.Ecommerce.Core.Contracts;
using Microsoft.EntityFrameworkCore;
namespace HG.Ecommerce.Infrastruction.Presistance.Data.Presistance.Specifications
{
    static class SpecificationEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> specifications)
        {
            var query = inputQuery;
            if (specifications.Criteria != null)
                query = query.Where(specifications.Criteria);
            if (specifications.Includes != null && specifications.Includes.Any())
                query = specifications.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;
        }
    }
}
