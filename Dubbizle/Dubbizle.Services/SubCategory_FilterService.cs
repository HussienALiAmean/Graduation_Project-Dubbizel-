using Dubbizle.Data.Repository;
using Dubbizle.DTOs;
using Dubbizle.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dubbizle.Services
{
    public class SubCategory_FilterService
    {
        IRepository<SubCategory_Filter> _repository;

        public SubCategory_FilterService(IRepository<SubCategory_Filter> repository)
        {
            _repository = repository;
        }
        
        public IEnumerable<SubCategory_Filter> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        // Alzhraa
        public IEnumerable<SubCategory_Filter> GetAllBySubCategoryID(string property1, string property2, int id)
        {
            return _repository.GetAll(property1,property2).Where(F=>F.SubCategoryID==id).ToList();
        }
        public IEnumerable<SubCategory_Filter> GetAllBySubCategory(string property1)
        {
            return _repository.GetAll(property1).ToList();
        }
        public IEnumerable<SubCategory_Filter> Get(Expression<Func<SubCategory_Filter, bool>> expression)
        {
            return _repository.Get(expression);
        }

        public SubCategory_Filter GetByID(int id)
        {
            return _repository.GetByID(id);
        }
    }
}