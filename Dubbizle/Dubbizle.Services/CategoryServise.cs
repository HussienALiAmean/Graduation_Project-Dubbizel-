using Dubbizle.Data.Repository;
using Dubbizle.Models;
using System.Linq.Expressions;

namespace Dubbizle.Services
{
    public class CategoryServise
    {
        IRepository<Category> _repository;

        public CategoryServise(IRepository<Category> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Category> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public IEnumerable<Category> Get(Expression<Func<Category, bool>> expression)
        {
            return _repository.Get(expression);
        }

        public Category GetByID(int id)
        {
            return _repository.GetByID(id);
        }

        public Category Add(Category category)
        {
            return _repository.Add(category);
        }

        public void Update(Category category)
        {
            category.Name = "Cairo";
          //  _repository.Update(category, nameof(category.Name));
        }

        //public void UpdateName()
        //{
        //    var branch = _repository.Get(x => x.ID == 1).FirstOrDefault();
        //    branch.Name = "test";

        //    //_repository.Update(branch, nameof(Branch.Name));
        //}


        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }

    }
}