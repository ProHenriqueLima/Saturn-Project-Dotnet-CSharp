using WdaApi.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WdaApi.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Add(TEntity entity);

        Task AddRange(IEnumerable<TEntity> entitys);

        Task<TEntity> GetById(Guid id);

        Task<TEntity> GetByIdTracked(Guid id);

        Task<IEnumerable<TEntity>> GetAll();

        Task Update(TEntity entity);

        Task UpdateRange(IEnumerable<TEntity> entitys);

        Task UpdateKey(TEntity t, object key);

        Task Remove(Guid id);

        Task RemoveRange(IEnumerable<TEntity> entitys);

        Task RemoveEntity(TEntity entity);

        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);

        Task<int> SaveChanges();

        Task<TEntity> AddAsync(TEntity entity);

        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        Task<bool> CheckExist(Expression<Func<TEntity, bool>> predicate);
    }
}
