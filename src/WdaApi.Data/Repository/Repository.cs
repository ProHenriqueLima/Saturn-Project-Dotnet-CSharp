using WdaApi.Business.Interfaces;
using WdaApi.Business.Models;
using WdaApi.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WdaApi.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly SaturnApiDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(SaturnApiDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await DbSet.AsNoTracking().SingleOrDefaultAsync(c => c.Id == id);
        }

        public virtual async Task<TEntity> GetByIdTracked(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }
        public async Task AddRange(IEnumerable<TEntity> entitys)
        {
            DbSet.AddRange(entitys);
            await SaveChanges();
        }

        public async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public async Task UpdateRange(IEnumerable<TEntity> entitys)
        {
            DbSet.UpdateRange(entitys);
            await SaveChanges();
        }

        public async Task UpdateKey(TEntity t, object key)
        {
            TEntity exist = Db.Set<TEntity>().Find(key);

            if (exist != null)
            {
                Db.Entry(exist).CurrentValues.SetValues(t);
            }

            await SaveChanges();
        }

        public virtual async Task Remove(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task RemoveEntity(TEntity entity)
        {
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task RemoveRange(IEnumerable<TEntity> entitys)
        {
            DbSet.RemoveRange(entitys);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            Db.Set<TEntity>().Add(entity);
            await Db.SaveChangesAsync();       
            return entity;
        }
        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = Db.Set<TEntity>().Where(predicate);
            return query;
        }
        public virtual async Task<bool> CheckExist(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().AnyAsync(predicate);             
        }
        public void Dispose()
        {
            Db?.Dispose();
        }

   
    }
}
