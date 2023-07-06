using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dubbizle.Data.Repository;
using Dubbizle.Data.UnitOfWork;
using Dubbizle.DTOs;
using Dubbizle.Models;
using Microsoft.AspNetCore.Mvc;
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
        //UnitOfWork unitOfWork;
        public AdvertismentService(IRepository<Advertisment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            //unitOfWork = _unitOfWork;
        }


        public IEnumerable<Advertisment> GetAllByFielterationBagenatio(string location, Dictionary<int, string> filterationtable, int limetitemNumber, int pagenumber)
        {

            List<Advertisment> addvertismentlist = new List<Advertisment>();
            var data = _repository.GetAll().Include("Advertisment_FiltrationValuesList.filtrationValue").Where(x => x.Location.Contains(location)).ToList();
            foreach (var item in data)
            {
                if (item.Advertisment_FiltrationValuesList.Join(filterationtable, f => f.filtrationValue.SubCategory_FilterID, c => c.Key, (f, c) => new { f, c }).Count() > 0)
                {
                    addvertismentlist.Add(item);
                }
            }
            return addvertismentlist.Skip(pagenumber * limetitemNumber).Take(limetitemNumber);
        }

        public IEnumerable<Advertisment> GetAllByFieltBagenatio(int limetitemNumber, int pagenumber)
        {
            List<Advertisment> addvertismentlist = new List<Advertisment>();
            var data = _repository.GetAll().Include("Advertisment_FiltrationValuesList.filtrationValue").ToList();
            return addvertismentlist.Skip(pagenumber * limetitemNumber).Take(limetitemNumber);
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
            return _repository.GetAll(property1, property2).Where(A => A.AdStatus == "Active" && A.ExpirationDate > DateTime.Now && A.SubCategoryID == id).OrderByDescending(A => A.ExpireDateOfPremium).ToList();
        }
        // Alzhraa & Hussien
        public IEnumerable<Advertisment> GetAllBySubCategoryID(string property1, string property2, string property3, int id)
        {
            return _repository.GetAll(property1, property2 , property3).Where(A => A.AdStatus == "Active" && A.ExpirationDate > DateTime.Now && A.SubCategoryID == id).OrderByDescending(A => A.ExpireDateOfPremium).ToList();
        }
        // Alzhraa & Hussien
        public IEnumerable<Advertisment> GetAllByCategoryID(string property1, string property2, int id)
        {
            return _repository.GetAll(property1, property2).Where(A => A.AdStatus == "Active" && A.ExpirationDate > DateTime.Now && A.CategoryID == id).OrderByDescending(A => A.ExpireDateOfPremium).ToList();
        }
        // Alzhraa & Hussien
        public IEnumerable<Advertisment> GetAllByCategoryID(string property1, string property2, string property3 , int id)
        {
            return _repository.GetAll(property1, property2, property3).Where(A => A.AdStatus == "Active" && A.ExpirationDate > DateTime.Now && A.CategoryID==id).OrderByDescending(A => A.ExpireDateOfPremium).ToList();
        }

        public IEnumerable<Advertisment> Get(Expression<Func<Advertisment, bool>> expression)
        {
            return _repository.Get(expression).ToList();
        }
        public IEnumerable<Advertisment> GetAdvertismentUsers(string id, string property1, string property2)
        {
            return _repository.GetAll(property1, property2).Where(a => a.ApplicationUserId == id).ToList();
        }
        public IEnumerable<Advertisment> GetAllAdvertisments(string property1, string property2, string property3, string property4)
        {
            return _repository.GetAll(property1, property2, property3, property4).ToList();
        }
        public IEnumerable<Advertisment> GetAllAdvertisments(string property1, string property2, int id)
        {
            return _repository.GetAll(property1, property2).Where(a => a.AdStatus == "Active" && a.CategoryID == id).ToList();
        }
        public Advertisment GetByID(int id)
        {
            return _repository.GetByID(id);
        }


        public Advertisment GetAdsByID(int id, string prperty1, string property2)
        {
            return _repository.GetAll(property2, prperty1).FirstOrDefault(a => a.ID == id);
        }


        // Alzhraa
        public IEnumerable<Advertisment> GetMyAdvertisments(string ApplicationUserId)
        {
            return _repository.GetAll("AdvertismentImagesList", "Advertisment_RentOptionList").Where(A => A.ApplicationUserId == ApplicationUserId&&A.Deleted==false ).ToList();
        }


        public IEnumerable<Advertisment> GetBySearchQuery(string query,string property1,string property2,string property3,string property4)
        {
            var results = _repository.GetAll(property1, property2,property3, property4).Where(A =>A.AdStatus=="Active"&& A.ExpirationDate > DateTime.Now && (A.Title.Contains(query) || A.Category.Name.Contains(query) || A.SubCategory.Name.Contains(query))).OrderByDescending(A => A.ExpireDateOfPremium).ToList();
            return results;
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
