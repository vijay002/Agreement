using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Agreement.Domain.Base
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected ApplicationDbContext _context { get; set; }
        protected readonly DbSet<TEntity> _dbSet;
      
        public BaseRepository(ApplicationDbContext respositoryContext)
        {
            _context = respositoryContext;
            this._dbSet = _context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            //EntityEntry dbEntityEntry = _context.Entry<TEntity>(entity);
            _context.Set<TEntity>().Add(entity);
        }

        public IEnumerable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsEnumerable();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public int Count()
        {
            return _context.Set<TEntity>().Count();
        }

        public void Delete(TEntity entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<TEntity>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public void Delete(IEnumerable<TEntity> result)
        {
            _context.RemoveRange(result);
        }

        public void DeleteWhere(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> entities = _context.Set<TEntity>().Where(predicate);

            foreach (var entity in entities)
            {
                _context.Entry<TEntity>(entity).State = EntityState.Deleted;
            }
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsEnumerable();

        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate).FirstOrDefault();
        }

        public bool IsExist(Expression<Func<TEntity, bool>> predicate)
        {
            return (_context.Set<TEntity>().AsNoTracking().FirstOrDefault(predicate) == null ? false : true);
        }

        public void Update(TEntity entity, byte[] version = null)
        {
            EntityEntry dbEntityEntry = _context.Entry<TEntity>(entity);

            if (version != null)
                dbEntityEntry.Property("Version").OriginalValue = version;

            _context.Update<TEntity>(entity);
            dbEntityEntry.State = EntityState.Modified;

        }

        public IQueryable<TEntity> GetAllIncluding(params System.Linq.Expressions.Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = _dbSet;
            foreach (System.Linq.Expressions.Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<TEntity, object>(includeProperty);
            }

            return queryable;
        }
        /// <summary>
        /// Gets a table
        /// </summary>
        public IQueryable<TEntity> Table
        {
            get
            {
                return this._dbSet;
            }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public IQueryable<TEntity> TableNoTracking
        {
            get
            {
                return this._dbSet.AsNoTracking();
            }
        }





    }
}
