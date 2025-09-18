using HG.Ecommerce.Core.Common;
using HG.Ecommerce.Core.Contracts;
using HG.Ecommerce.Infrastruction.Presistance.Data.DbContextFile;
using HG.Ecommerce.Infrastruction.Presistance.Data.Presistance.Specifications;
using Microsoft.EntityFrameworkCore;
namespace HG.Ecommerce.Infrastruction.Presistance.Data.Presistance.Generic_Repository
{
     public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
         where TEntity : BaseEntity<TKey>
         where TKey : IEquatable<TKey>
    {
        private readonly EcommerceDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public GenericRepository(EcommerceDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _dbSet = context.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec)
        {
            var query = SpecificationEvaluator<TEntity, TKey>.GetQuery(_dbSet.AsQueryable(), spec);
            return await query.ToListAsync();
        }
                                 
        public async Task<TEntity?> GetByIdWithSpecAsync(ISpecifications<TEntity, TKey> spec)
        {
            var query = SpecificationEvaluator<TEntity, TKey>.GetQuery(_dbSet.AsQueryable(), spec);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
        => withTracking
            ? await _context.Set<TEntity>().ToListAsync()
            : await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        public Task<TEntity?> GetByIdAsync(TKey id)
      => _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id.Equals(id));
        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
        public async Task DeleteAsync(TKey id)
        {
            await _context.Set<TEntity>().Where(e => e.Id.Equals(id)).ExecuteDeleteAsync();
        }
    }
}
