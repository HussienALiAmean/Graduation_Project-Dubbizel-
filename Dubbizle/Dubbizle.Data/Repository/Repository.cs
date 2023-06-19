using Dubbizle.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Dubbizle.Data.Repository;

public class Repository<T> : IRepository<T> where T : BaseModel
{
    Context _context;
    public Repository(Context context)
    {
        _context = context;
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>();//.Where(x=> !x.Deleted);
    }

    public IQueryable<T> GetAll(string property)
    {
        return _context.Set<T>().Include(property);
    }

    public IQueryable<T> GetAll(string property1, string property2)
    {
        return _context.Set<T>().Include(property1).Include(property2);
    }
    
    public IQueryable<T> GetAll(string property1, string property2, string property3, string property4)
    {
        return _context.Set<T>().Include(property1).Include(property2).Include(property3).Include(property4);
    }
    public IQueryable<T> Get(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }
   
    public T GetByID(int id)
    {
        return _context.Set<T>().FirstOrDefault(x => x.ID == id);
    }

    public T GetByID(int id, string property1, string property2)
    {
        return _context.Set<T>().Include(property1).Include(property2).FirstOrDefault(x => x.ID == id);
    }

    public T Add(T entity)
    {
        entity.Deleted = false;
        _context.Set<T>().Add(entity);
        return entity;
    }
    public void create(T t)
    {
        _context.Set<T>().Add(t);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _context.Update(entity);
    }

    //public void Update(T entity, params string[] properties)
    //{
    //    var localEntity = _context.Set<T>().Local.Where(x => x.ID == entity.ID).FirstOrDefault();

    //    EntityEntry entityEntry;

    //    if (localEntity is null)
    //    {
    //        entityEntry = _context.Set<T>().Entry(entity);
    //    }
    //    else
    //    {
    //        entityEntry =
    //            _context.ChangeTracker.Entries<T>()
    //            .Where(x => x.Entity.ID == entity.ID).FirstOrDefault();
    //    }

    //    foreach (var property in entityEntry.Properties)
    //    {
    //        if (properties.Contains(property.Metadata.Name))
    //        {
    //            property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name).GetValue(entity);
    //            property.IsModified = true;
    //        }
    //    }

    //    entity.UpdatedDate = DateTime.Now;
    //    //entity.UpdatedBy = 12;
    //}

    public void Delete(int id)
    {
        var entity = GetByID(id);
        entity.Deleted = true;
    }
    public void delete(T tt)
    {
        _context.Remove<T>(tt);
    }
    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}
