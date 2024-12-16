using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pedigree.Domain.Entities;
using Pedigree.Domain.Exceptions;
using Pedigree.Domain.Interfaces.Repositories;
using Pedigree.Infra.Data.Context;

namespace Pedigree.Infra.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition)
        {
            var entity = await _context.Set<TEntity>()
                                       .AsNoTracking()
                                       .FirstAsync(condition);
            if (entity == null)
            {
                throw new EntityNotFoundException("Entity does not exist.");
            }
            return entity;
        }

        public virtual async Task<TEntity?> GetOrDefaultAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await _context.Set<TEntity>()
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(condition);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? condition = null)
        {
            if (condition != null)
                return await _context.Set<TEntity>()
                                     .AsNoTracking()
                                     .Where(condition)
                                     .ToListAsync();

            return await _context.Set<TEntity>()
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            entity.CreateTime = DateTime.Now;
            entity.ModifyTime = DateTime.Now;

            _context.Set<TEntity>().Add(entity);
            await SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            entity.ModifyTime = DateTime.Now;

            _context.Set<TEntity>().Update(entity);
            await SaveChangesAsync();
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.ModifyTime = DateTime.Now;
            }

            _context.Set<TEntity>().UpdateRange(entities);
            await SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            await SaveChangesAsync();
        }

        public virtual async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            await SaveChangesAsync();
        }

        public async Task<bool> HasAnyData()
        {
            if (await _context.Set<TEntity>().AnyAsync())
                return true;

            return false;
        }
    }
}