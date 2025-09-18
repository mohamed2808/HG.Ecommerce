using HG.Ecommerce.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HG.Ecommerce.Core.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>;
        Task<int> CompleteAsync();
    }
}
