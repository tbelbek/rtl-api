using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using RtlAPI.Data.Entity.Base;

namespace RtlAPI.Data
{
    public class DataRepository<TKeyType, TEntity> : IDataRepository<TEntity, TKeyType>
        where TEntity : BaseEntity<TKeyType> where TKeyType : new()
    {
        protected void SetContextSettings()
        {
            Context.Configuration.LazyLoadingEnabled = false;
            Context.Configuration.ProxyCreationEnabled = false;
        }

        public DataRepository()
        {
            if (Context == null)
            {
                Context = new CustomContext();
            }

            SetContextSettings();
        }

        public DataRepository(CustomContext context)
        {
            Context = context;
        }

        protected CustomContext Context = new CustomContext();
        public DbContext BaseContext => this.Context;
        public DbSet<TEntity> EntitySet => this.Context.Set<TEntity>();
        public virtual int Count()
        {
            return this.EntitySet.Count();
        }

        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] properties)
        {
            IQueryable<TEntity> query = EntitySet;

            query = properties.Aggregate(query, (current, property) => current.Include(property));

            return query;
        }

        public IQueryable<TEntity> GetAllWithInclude()
        {
            IQueryable<TEntity> query = EntitySet;
            var type = typeof (TEntity);
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var isVirtual = property.GetGetMethod().IsVirtual;
                if (isVirtual && properties.FirstOrDefault(c => c.Name == property.Name + "Id") != null)
                {
                    query = query.Include(property.Name);
                }
            }
            return query;
        }

        public virtual IQueryable<TEntity> GetAll() => EntitySet.Where(x => !x.IsDeleted).AsNoTracking();

        public virtual IQueryable<TEntity> GetByExpression(Expression<Func<TEntity, bool>> func)
        {
            return this.EntitySet.Where(x => !x.IsDeleted).Where(func).AsNoTracking();
        }
        public virtual void InsertList(List<TEntity> dtoModel)
        {
            this.EntitySet.AddRange(dtoModel);
            this.Save();
        }

        public int Save() => this.Context.ChangeTracker.HasChanges() ? this.Context.SaveChanges() : -1;
    }

    public static class ContextExtensions
    {
        public static string GetTableName<T>(this DbContext context) where T : class
        {
            ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;

            return objectContext.GetTableName<T>();
        }

        public static string GetTableName<T>(this ObjectContext context) where T : class
        {
            string sql = context.CreateObjectSet<T>().ToTraceString();
            Regex regex = new Regex(@"FROM\s+(?<table>.+)\s+AS");
            Match match = regex.Match(sql);

            string table = match.Groups["table"].Value;
            return table;
        }
    }
}