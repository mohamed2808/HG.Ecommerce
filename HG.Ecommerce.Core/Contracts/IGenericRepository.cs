using HG.Ecommerce.Core.Common;
namespace HG.Ecommerce.Core.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false);
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<List<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec,bool withTracking = false);
        Task<TEntity?> GetByIdWithSpecAsync(int id,ISpecifications<TEntity, TKey> spec,bool withTracking = false);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TKey id);
    }
}
