using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.Data.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(string property);
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        T GetByID(int id);
        T Add(T entity);
        void Update(T entity);
        //void Update(T entity, params string[] properties);
        void Delete(int id);
        void SaveChanges();
    }
}
