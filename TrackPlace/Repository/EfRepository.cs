using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using TrackPlace.Repository.Interface;

namespace TrackPlace.Repository
{
    /// <summary>
    /// Method for Crud opertaions for every type of entity
    /// </summary>
    /// <typeparam name="TEntity"> 
    /// Entity name 
    /// </typeparam>
    public class EFRepository<TEntity> : IEFRepository<TEntity> where TEntity : class
    {
        public List<TEntity> All => RepositoryDbSet.ToList();

        protected DbContext RepositoryDbContext;
        protected DbSet<TEntity> RepositoryDbSet;
        public EFRepository(IContext dbContext)
        {
            RepositoryDbContext = dbContext as DbContext;
            if (RepositoryDbContext == null)
            {
                throw new ArgumentNullException(paramName: nameof(dbContext));
            }
            RepositoryDbSet = RepositoryDbContext.Set<TEntity>();
            if (RepositoryDbSet == null)
            {
                throw new NullReferenceException(message: nameof(RepositoryDbSet));
            }
        }
        public TEntity Find(int id)
        {
            return RepositoryDbSet.Find(id);
        }

        public void Remove(int id)
        {
            Remove(entity: Find(id: id));
        }

        // Entity Framework Add and Attach and Entity States
        // https://msdn.microsoft.com/en-us/library/jj592676(v=vs.113).aspx

        public void Remove(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = RepositoryDbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                RepositoryDbSet.Attach(entity);
                RepositoryDbSet.Remove(entity);
            }

        }

        public TEntity Add(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = RepositoryDbContext.Entry(entity: entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
                return entity;
            }
            return RepositoryDbSet.Add(entity: entity);
        }

        public void Update(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = RepositoryDbContext.Entry(entity: entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                RepositoryDbSet.Attach(entity: entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public int SaveChanges()
        {
            return RepositoryDbContext.SaveChanges();
        }
    }
}
