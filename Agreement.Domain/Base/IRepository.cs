using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Agreement.Domain.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetAll();
        int Count();
        //T GetSingle(int id);
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate);
        bool IsExist(Expression<Func<TEntity, bool>> predicate);
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Update(TEntity entity, byte[] version = null);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> result);
        void DeleteWhere(Expression<Func<TEntity, bool>> predicate);
        void Commit();


        IQueryable<TEntity> GetAllIncluding(params System.Linq.Expressions.Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }

    }
}
