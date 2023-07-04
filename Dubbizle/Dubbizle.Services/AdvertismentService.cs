using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dubbizle.Data.Repository;
using Dubbizle.Data.UnitOfWork;
using Dubbizle.DTOs;
using Dubbizle.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace Dubbizle.Services
{
    public class AdvertismentService
    {
        IRepository<Advertisment> _repository;
        IMapper _mapper;
        UnitOfWork unitOfWork;
        public AdvertismentService(IRepository<Advertisment> repository, IMapper mapper, UnitOfWork _unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            unitOfWork = _unitOfWork;
        }


      
        public IEnumerable<Advertisment> GetAllByFielteration(string location, Dictionary<int , string> filterationtable)
        {


            var data  = _repository.GetAll().Include("Advertisment_FiltrationValuesList").Where(x => x.Location.Contains(location)).ToList();
            var addvertisment=new List<Advertisment>();

            foreach (var item in data)
            {
                if (item.Advertisment_FiltrationValuesList.Join(filterationtable, f => f.filtrationValue.SubCategory_FilterID, s => s.Key, (d, c) => d.ID).Count()>0)
                addvertisment.Add(item);
            }



            //  var data3 = data2.Where(x => x.Advertisment_FiltrationValuesList.Join(filterationtable, f => f.filtrationValue.SubCategory_FilterID, s => s.Key, (f, s) => new { id=f.ID} ).ToList().Count()>0);  //.Where(x => x.f.filtrationValue.Value == x.s.Value).Count() < filterationtable.Count)  ;
            //  var data4 = data2.Where(x => x.f.filtrationValue.Value == x.s.Value).Count() < filterationtable.Count);

            return addvertisment;
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
        public IEnumerable<Advertisment> GetAllBySubCategoryID(string property1, string property2, string property3, int id)
        {
            return _repository.GetAll(property1, property2 , property3).Where(A => A.ExpirationDate > DateTime.Now && A.SubCategoryID == id).OrderByDescending(A => A.ExpireDateOfPremium).ToList();
        }
        // Alzhraa & Hussien
        public IEnumerable<Advertisment> GetAllByCategoryID(string property1, string property2, int id)
        {
            return _repository.GetAll(property1, property2).Where(A => A.ExpirationDate > DateTime.Now && A.CategoryID==id).OrderByDescending(A => A.ExpireDateOfPremium).ToList();
        }
        // Alzhraa & Hussien
        public IEnumerable<Advertisment> GetAllByCategoryID(string property1, string property2, string property3 , int id)
        {
            return _repository.GetAll(property1, property2, property3).Where(A => A.ExpirationDate > DateTime.Now && A.CategoryID==id).OrderByDescending(A => A.ExpireDateOfPremium).ToList();
        }

        public IEnumerable<Advertisment> Get(Expression<Func<Advertisment, bool>> expression)
        {
            return _repository.Get(expression);
        }

        public Advertisment GetByID(int id)
        {
            return _repository.GetByID(id);
        }

        public Advertisment Add(Advertisment advertisment)
        {
            return _repository.Add(advertisment);
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
