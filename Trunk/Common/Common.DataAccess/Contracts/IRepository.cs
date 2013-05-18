using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsWebPt.Common.DataAccess
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(IEnumerable<String> includes);
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
    }
}
