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
        IQueryable<T> GetAll(string property1, string property2);
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        T GetByID(int id);
        T GetByID(int id, string property1, string property2);
        T Add(T entity);
        void Update(T entity);
        //void Update(T entity, params string[] properties);
        void Delete(int id);
        void SaveChanges();
    }
}
