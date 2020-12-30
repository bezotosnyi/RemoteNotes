using System;
using System.Linq;
using System.Linq.Expressions;
using RemoteNotes.DAL.Domain.Entities;

namespace RemoteNotes.DAL.Contact
{
    public interface IRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        IQueryable<TEntity> FindAll(bool trackChanges = false);

        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> filter, bool trackChanges = false);

        TEntity FindById(object id);

        void Insert(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);
    }
}