using HG.Ecommerce.Core.Common;
using HG.Ecommerce.Core.Contracts;
using HG.Ecommerce.Infrastruction.Presistance.Data.DbContextFile;
using HG.Ecommerce.Infrastruction.Presistance.Data.Presistance.Generic_Repository;
using System.Collections.Concurrent;
namespace HG.Ecommerce.Infrastruction.Presistance.Data.Presistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EcommerceDbContext _context;
        private readonly ConcurrentDictionary<string, object> _repositories;
        public UnitOfWork(EcommerceDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _repositories = new();
        }
        public async Task<int> CompleteAsync()
        => await _context.SaveChangesAsync();

        public void Dispose()
        => _context.Dispose();

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return _repositories.GetOrAdd(
                 $"{typeof(TEntity).Name}_{typeof(TKey).Name}",
                 _ => new GenericRepository<TEntity, TKey>(_context)) as IGenericRepository<TEntity, TKey>
             ?? throw new InvalidOperationException($"Repository for {typeof(TEntity).Name} not found.");
        }
    }
}
