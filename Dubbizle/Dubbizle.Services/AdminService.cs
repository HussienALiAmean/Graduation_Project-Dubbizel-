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
using System.Text;
using System.Threading.Tasks;


namespace Dubbizle.Services
{
    public class AdminService
    {
        IRepository<Category> _categoryRepository;
        IRepository<Filter> _filterRepository;
        IRepository<SubCategory_Filter> _subCatFilterRepository;
        IRepository<Package> _packageRepository;
        IRepository<FiltrationValue> _filtrationValueRepository;
        IRepository<Advertisment> _advertismentRepository;


        public AdminService(IRepository<Category> categoryRepository, IRepository<Filter> filterRepository,
            IRepository<SubCategory_Filter> subCatFilterRepository, IRepository<Package> packageRepository,
             IRepository<FiltrationValue> filtrationValueRepository, IRepository<Advertisment> advertismentRepository)
        {
            _categoryRepository = categoryRepository;
            _filterRepository = filterRepository;
            _subCatFilterRepository = subCatFilterRepository;
            _packageRepository = packageRepository;
            _filtrationValueRepository = filtrationValueRepository;
            _advertismentRepository = advertismentRepository;

        }


        //////////////////////////////////////////  Category /////////////////////////////////

        //public IEnumerable<Category> GetAllCategories()
        //{
        //    return _categoryRepository.GetAll().Where(c => c.Deleted == false).ToList();
        //}
        public IEnumerable<Category> GetAllCategories(string property)
        {
            return _categoryRepository.GetAll(property).Where(c => c.Deleted == false).ToList();
        }

        public Category GetCategoryByID(int id)
        {
            return _categoryRepository.GetByID(id);
        }
        public Category EditCategory(Category categoty)
        {
            _categoryRepository.Update(categoty);
            _categoryRepository.SaveChanges();
            if (categoty.ParentCategoryID != null)
            {
                categoty.ParentCategory = _categoryRepository.GetByID((int)categoty.ParentCategoryID);

            }
            return categoty;
        }

        public Category AddCategory(Category category)
        {
            Category newCategory = _categoryRepository.Add(category);
            _categoryRepository.SaveChanges();
            if (newCategory.ParentCategoryID != null)
            {
                newCategory.ParentCategory = _categoryRepository.GetByID((int)newCategory.ParentCategoryID);

            }
            return newCategory;
        }

        public void DeleteCategory(int id)
        {
            // _categoryRepository.Delete(id);
            Category category = _categoryRepository.GetByID(id);
            category.Deleted = true;
            _categoryRepository.Update(category);
            _categoryRepository.SaveChanges();
        }

        public bool CheckCategory(int id)
        {
            var category = _categoryRepository.GetAll("SubCategoriesList", "CategoryAdvertismentsList", "SubCaategoryAdvertismentsList", "SubCaategoryPackagesList", "SubCategory_FilterList").FirstOrDefault(c => c.ID == id);

            // Check if any related records exist
            if (
                category.SubCategoriesList.Any(L1 => L1.Deleted == false) ||
                category.CategoryAdvertismentsList.Any(L2 => L2.Deleted == false) ||
                category.SubCaategoryAdvertismentsList.Any(L3 => L3.Deleted == false) ||
                category.SubCaategoryPackagesList.Any(L4 => L4.Deleted == false) ||
                category.SubCategory_FilterList.Any(L5 => L5.Deleted == false)
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //////////////////////////////////////////  Filter /////////////////////////////////

        public IEnumerable<Filter> GetAllFilters()
        {
            return _filterRepository.GetAll().Where(c => c.Deleted == false).ToList();
        }

        public Filter AddFilter(Filter filter)
        {
            Filter newFilter = _filterRepository.Add(filter);
            _filterRepository.SaveChanges();
            return newFilter;
        }

        public Filter GetFilterByID(int id)
        {
            return _filterRepository.GetByID(id);
        }
        public void EditFilter(Filter filter)
        {
            _filterRepository.Update(filter);
            _filterRepository.SaveChanges();
        }

        public void DeleteFilter(int id)
        {
            Filter filter = _filterRepository.GetByID(id);
            filter.Deleted = true;
            _filterRepository.Update(filter);
            _filterRepository.SaveChanges();
        }

        public bool CheckFilter(int id)
        {
            var filter = _filterRepository.GetAll("SubCategory_FiltersList").FirstOrDefault(c => c.ID == id);

            // Check if any related records exist
            if (filter.SubCategory_FiltersList.Any(L1 => L1.Deleted == false))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        ////////////////////////////////////////// SubCategory Filters /////////////////////////////////

        public IEnumerable<SubCategory_Filter> GetAllSubCategoryFilters()
        {
            return _subCatFilterRepository.GetAll("SubCategory", "Filter").Where(SF => SF.Deleted == false).ToList();
        }
        public IEnumerable<Category> GetSubCategoryList()
        {
            return _categoryRepository.GetAll().Where(c => c.ParentCategoryID != null && c.Deleted == false).ToList();
        }

        public SubCategory_Filter PostSubCategoryFilter(SubCategory_Filter subCategory_Filter)
        {
            SubCategory_Filter newSubCategory_Filter = _subCatFilterRepository.Add(subCategory_Filter);
            _categoryRepository.SaveChanges();
            newSubCategory_Filter.Filter = _filterRepository.GetByID(newSubCategory_Filter.FilterID);
            newSubCategory_Filter.SubCategory = _categoryRepository.GetByID(newSubCategory_Filter.SubCategoryID);
            return newSubCategory_Filter;
        }

        public SubCategory_Filter GetSubCatFilterByID(int id)
        {
            return _subCatFilterRepository.GetByID(id);
        }
        public SubCategory_Filter EditSubCategoryFilter(SubCategory_Filter subCategory_Filter)
        {
            _subCatFilterRepository.Update(subCategory_Filter);
            _subCatFilterRepository.SaveChanges();
            subCategory_Filter.Filter = _filterRepository.GetByID(subCategory_Filter.FilterID);
            subCategory_Filter.SubCategory = _categoryRepository.GetByID(subCategory_Filter.SubCategoryID);
            return subCategory_Filter;
        }

        public void DeleteSubCatFilter(int id)
        {
            SubCategory_Filter subCategory_Filter = _subCatFilterRepository.GetByID(id);
            subCategory_Filter.Deleted = true;
            _subCatFilterRepository.Update(subCategory_Filter);
            _subCatFilterRepository.SaveChanges();
        }



        public bool CheckSubCategoryFilter(int id)
        {
            var subCategoryFilter = _subCatFilterRepository.GetAll("FiltrationValuesList").FirstOrDefault(c => c.ID == id);

            // Check if any related records exist
            if (subCategoryFilter.FiltrationValuesList.Any(L1 => L1.Deleted == false))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        ////////////////////////////////////////// SubCategory Filters Values /////////////////////////////////
        public IEnumerable<FiltrationValue> GetAllFiltrationValues()
        {
            return _filtrationValueRepository.GetAll("SubCategory_Filter.SubCategory", "SubCategory_Filter.Filter").Where(FV => FV.Deleted == false).ToList();
        }

        public FiltrationValue PostFilterValue(FiltrationValue filtrationValue)
        {
            FiltrationValue newFiltrationValue = _filtrationValueRepository.Add(filtrationValue);
            _filtrationValueRepository.SaveChanges();
            newFiltrationValue.SubCategory_Filter = _subCatFilterRepository.GetByID(newFiltrationValue.SubCategory_FilterID, "SubCategory", "Filter");
            return newFiltrationValue;
        }

        public FiltrationValue GetFilterValueByID(int id)
        {
            return _filtrationValueRepository.GetByID(id);
        }

        public FiltrationValue EditFilterValue(FiltrationValue filtrationValue)
        {
            _filtrationValueRepository.Update(filtrationValue);
            _filtrationValueRepository.SaveChanges();
            filtrationValue.SubCategory_Filter = _subCatFilterRepository.GetByID(filtrationValue.SubCategory_FilterID, "SubCategory", "Filter");
            return filtrationValue;
        }

        public void DeleteFilterValue(int id)
        {
            FiltrationValue filtrationValue = _filtrationValueRepository.GetByID(id);
            filtrationValue.Deleted = true;
            _filtrationValueRepository.Update(filtrationValue);
            _filtrationValueRepository.SaveChanges();
        }


        public bool CheckFilterValue(int id)
        {
            var filterValue = _filtrationValueRepository.GetAll("Advertisment_FiltrationValuesList").FirstOrDefault(c => c.ID == id);

            // Check if any related records exist
            if (filterValue.Advertisment_FiltrationValuesList.Any(L1 => L1.Deleted == false))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        ////////////////////////////////////////// Packages ////////////////////////////////////////

        public IEnumerable<Package> GetAllPackages()
        {
            return _packageRepository.GetAll("SubCategory").Where(P => P.Deleted == false).ToList();
        }

        public Package PostPackage(Package package)
        {
            Package newPackage = _packageRepository.Add(package);
            _packageRepository.SaveChanges();
            newPackage.SubCategory = _categoryRepository.GetByID(newPackage.SubCategoryID);
            return newPackage;
        }

        public Package EditPackage(Package package)
        {
            _packageRepository.Update(package);
            _packageRepository.SaveChanges();
            package.SubCategory = _categoryRepository.GetByID(package.SubCategoryID);
            return package;
        }

        public Package GetPackageByID(int id)
        {
            return _packageRepository.GetByID(id);
        }
        public void DeletePackage(int id)
        {
            Package package = _packageRepository.GetByID(id);
            package.Deleted = true;
            _packageRepository.Update(package);
            _packageRepository.SaveChanges();
        }


        public bool CheckPackage(int id)
        {
            var package = _packageRepository.GetAll("ApplicationUser_PackagesList").FirstOrDefault(c => c.ID == id);

            // Check if any related records exist
            if (package.ApplicationUser_PackagesList.Any(L1 => L1.Deleted == false))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        ////////////////////////////////////////// Advertisments /////////////////////////////////
        public IEnumerable<Advertisment> GetAllNotActiveAdvertisments()
        {
            return _advertismentRepository.GetAll("SubCategory", "AdvertismentImagesList").Where(A => A.Deleted == false && A.AdStatus == "Not Active").ToList();
        }
        public Advertisment GetAdvertismentByID(int id)
        {
            return _advertismentRepository.GetByID(id);
        }
        public void EditAdvertismentState(Advertisment advertisment)
        {
            _advertismentRepository.Update(advertisment);
            _advertismentRepository.SaveChanges();
        }



        ////////////////////////////////////////// Users ////////////////////////////////////////






    }
}
