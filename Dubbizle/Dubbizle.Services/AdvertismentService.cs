using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dubbizle.Data.Repository;
using Dubbizle.Data.UnitOfWork;
using Dubbizle.DTOs;
using Dubbizle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Dubbizle.Services
{
    public class AdvertismentService
    {
        IRepository<Advertisment> _repository;
        IMapper _mapper;
        //UnitOfWork unitOfWork;
        public AdvertismentService(IRepository<Advertisment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            //unitOfWork = _unitOfWork;
        }
        
        public IEnumerable<Advertisment> GetAll()
        {
            return _repository.GetAll().ToList();
        }


        public IEnumerable<AdvertismentHomePageDTO> GetAdvertismentsAll(Expression<Func<Advertisment, bool>> expression)
        {
            var categories = _repository.Get(expression);
            return categories.ProjectTo<AdvertismentHomePageDTO>(_mapper.ConfigurationProvider);
        }
    


        // Alzhraa & Hussien
        public IEnumerable<Advertisment> GetAllBySubCategoryID(string property1, string property2, int id)
        {
            return _repository.GetAll(property1,property2).Where(A=>A.ExpirationDate > DateTime.Now && A.SubCategoryID==id).OrderByDescending(A=>A.ExpireDateOfPremium).ToList();
        }

        // Alzhraa & Hussien
        public IEnumerable<Advertisment> GetAllByCategoryID(string property1, string property2, int id)
        {
            return _repository.GetAll(property1, property2).Where(A => A.ExpirationDate > DateTime.Now && A.CategoryID==id).OrderByDescending(A => A.ExpireDateOfPremium).ToList();
        }

        public IEnumerable<Advertisment> Get(Expression<Func<Advertisment, bool>> expression)
        {
            return _repository.Get(expression).ToList();
        }
        public IEnumerable<Advertisment> GetAdvertismentUsers(string id,string property1,string property2)
        {
            return _repository.GetAll(property1,property2).Where(a=>a.ApplicationUserId==id).ToList();
        }
        public IEnumerable<Advertisment> GetAllAdvertisments(string property1, string property2, string property3, string property4)
        {
            return _repository.GetAll(property1, property2, property3, property4).ToList();
        }
        public IEnumerable<Advertisment> GetAllAdvertisments(string property1, string property2,int id)
        {
            return _repository.GetAll(property1, property2).Where(a=>a.CategoryID==id).ToList();
        }
        public Advertisment GetByID(int id)
        {
            return _repository.GetByID(id);
        }


        public Advertisment GetAdsByID(int id,string prperty1,string property2)
        {
            return _repository.GetAll(property2,prperty1).FirstOrDefault(a=>a.ID==id);
        }


        // Alzhraa
        public IEnumerable<Advertisment> GetMyAdvertisments(string ApplicationUserId)
        {
            return _repository.GetAll("AdvertismentImagesList", "Advertisment_RentOptionList").Where(A => A.ApplicationUserId == ApplicationUserId&&A.Deleted==false).ToList();
        }


        public void Add(Advertisment advertisment)
        {
            _repository.Add(advertisment);
            _repository.SaveChanges();
        }

        public void Update(Advertisment advertisment)
        {
            advertisment.Location = "Cairo";
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
