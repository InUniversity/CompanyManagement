using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CompanyManagementEntity.Utilities;

namespace CompanyManagementEntity.Database.Base
{
    public abstract class BaseDao<T> : IBaseDao<T> where T : class
    {
        public void Add(T entity)
        {
            NewDbContext(db =>
            {
                db.Set<T>().Add(entity);
                db.SaveChanges();
            });
        }

        public void Delete(T entity)
        {
            NewDbContext(db =>
            {
                db.Set<T>().Remove(entity);
                db.SaveChanges();
            });
        }

        public void DeleteMulti(Expression<Func<T, bool>> where)
        {
            NewDbContext(db =>
            {
                IEnumerable<T> objects = db.Set<T>().Where<T>(where).AsEnumerable();
                foreach (T obj in objects)
                    db.Set<T>().Remove(obj);
            });
        }

        public virtual void Update(T entity)
        {
            NewDbContext(db =>
            {
                db.Set<T>().Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
            });
        }

        public List<T> GetList(Expression<Func<T, bool>> where)
        {
            List<T> list = default(List<T>);
            NewDbContext(db =>
            {
                list = db.Set<T>().Where<T>(where).ToList();
            });
            return list;
        }

        public int Count(Expression<Func<T, bool>> where)
        {
            int count = 0;
            NewDbContext(db =>
            {
                db.Set<T>().Count(where);
            });
            return count;
        }
        
        protected void NewDbContext(Action<CompanyManagementContext> action)
        {
            try
            {
                using (var db = new CompanyManagementContext())
                {
                    action(db);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(BaseDao<T>), ex.Message);
            }
        }
    }
}