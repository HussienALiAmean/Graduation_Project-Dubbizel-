using Dubbizle.Data.Repository;
using Dubbizle.DTOs;
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
      
        // Alzhraa
        public IEnumerable<Category> GetAll(string property)
        {
            return _repository.GetAll(property).Where(c=>c.ParentCategoryID==null).ToList();
        }

        //Hussien
        public IEnumerable<Category> GetAllByID(string property,int id)
        {
            return _repository.GetAll(property).Where(c => c.ParentCategoryID == id).ToList();
        }

        public IEnumerable<Category> Get(Expression<Func<Category, bool>> expression)
        {
            return _repository.Get(expression).ToList();
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