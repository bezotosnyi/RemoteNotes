using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RemoteNotes.DAL.Contact;
using RemoteNotes.DAL.Core.Entities;

namespace RemoteNotes.DAL.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        internal readonly DbContext RepositoryContext;

        protected RepositoryBase(DbContext repositoryContext)
        {
            RepositoryContext = repositoryContext ?? throw new ArgumentNullException(nameof(repositoryContext));
        }

        public IQueryable<TEntity> FindAll(bool trackChanges) =>
            !trackChanges
                ? RepositoryContext.Set<TEntity>()
                    .AsNoTracking()
                : RepositoryContext.Set<TEntity>();

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> filter, bool trackChanges) =>
            !trackChanges
                ? RepositoryContext.Set<TEntity>()
                    .Where(filter)
                    .AsNoTracking()
                : RepositoryContext.Set<TEntity>()
                    .Where(filter);

        public TEntity FindById(object id) => RepositoryContext.Set<TEntity>().Find(id);

        public void Insert(TEntity entity) => RepositoryContext.Set<TEntity>().Add(entity);

        public void Delete(object id) => Delete(FindById(id));

        public void Delete(TEntity entityToDelete) => RepositoryContext.Set<TEntity>().Remove(entityToDelete);

        public void Update(TEntity entityToUpdate) => RepositoryContext.Set<TEntity>().Update(entityToUpdate);

        #region Dispose pattern

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                RepositoryContext.Dispose();
            }
        }

        #endregion
    }
}