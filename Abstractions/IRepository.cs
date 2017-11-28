using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Abstractions
{
    public interface IRepository<in TKey, TEntity, out TViewModel>
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TKey id);
        Task SaveChangesAsync();

        TViewModel ToViewModel(TEntity entity);
    }
}