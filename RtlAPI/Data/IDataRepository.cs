using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RtlAPI.Data.Entity.Base;

namespace RtlAPI.Data
{
    public interface IDataRepository<TEntity, TKeyType> where TEntity : BaseEntity<TKeyType> where TKeyType : new()
    {

        DbContext BaseContext { get; }
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllWithInclude();
        IQueryable<TEntity> GetByExpression(Expression<Func<TEntity, bool>> func);
        void InsertList(List<TEntity> dtoModel);
        int Count();
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] properties);
        int Save();
    }
}