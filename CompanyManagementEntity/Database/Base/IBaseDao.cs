using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CompanyManagementEntity.Database.Base
{
    public interface IBaseDao<T>
    {
        void Add(T entity);
        void Delete(T entity);
        void DeleteMulti(Expression<Func<T, bool>> where);
        void Update(T entity);
        List<T> GetList(Expression<Func<T, bool>> where);
        int Count(Expression<Func<T, bool>> where);
    }
}