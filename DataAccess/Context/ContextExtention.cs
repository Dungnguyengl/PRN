using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Model.Context
{
    public static class ContextExtention
    {
        public async static Task CreateAsync<TEntity>(this DbSet<TEntity> entities, TEntity data) where TEntity : class
        {
            if (data is EntityBase @value)
            {
                @value.Id = default;
            }

            await entities.AddAsync(data);
        }

        public async static Task UpdateAsync<TEntity>(this DbSet<TEntity> entities, TEntity entity) where TEntity : class
        {
            if (!await entities.AsNoTracking().AnyAsync(e => e.Equals(entity)))
                throw new Exception("Not found");
            entities.Update(entity);
        }

        public async static Task DeleteAsync<TEntity>(this DbSet<TEntity> entities, TEntity entity) where TEntity : class
        {
            entities.Remove(entity);
        }

        public async static Task DeleteRangeAsync<TEntity>(this DbSet<TEntity> entities, IEnumerable<TEntity> range) where TEntity : class
        {
            foreach(var entity in range)
            {
                await entities.DeleteAsync(entity);
            }
        }

        public static IQueryable<TEntity> NoTracking<TEntity>(this DbSet<TEntity> entities) where TEntity : class
        {
            return entities.AsNoTracking();
        }
    }
}
