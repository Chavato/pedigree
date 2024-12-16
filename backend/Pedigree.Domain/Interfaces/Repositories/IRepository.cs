using System.Linq.Expressions;
using Pedigree.Domain.Entities;

namespace Pedigree.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition);
        Task<TEntity?> GetOrDefaultAsync(Expression<Func<TEntity, bool>> condition);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? condition = null);
        Task<TEntity> CreateAsync(TEntity entity);
        Task CreateRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
        Task<bool> HasAnyData();
        Task<int> SaveChangesAsync();
    }
}