using Dubbizle.DTOs;
using Dubbizle.Services;
using Microsoft.AspNetCore.Http;
using Dubbizle.Models;
using Dubbizle.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dubbizle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        AdminService _AdminService;
        public AdminController( AdminService adminService) {
            _AdminService = adminService;
         }


        //////////////////////////////////////////  Category /////////////////////////////////

        // Alzhraa 

        [HttpGet("GetAllCategories")]
        public ResultDTO GetAllCategories()
        {
            ResultDTO resultDTO = new ResultDTO();
            var categories = _AdminService.GetAllCategories("ParentCategory");
            resultDTO.StatusCode= 200;
            resultDTO.Data= categories;
            return resultDTO;
        }

        [HttpPost("PostCategory")]
        public ResultDTO PostCategory(SubCategoryDTO subCategoryDTO)
        {
            ResultDTO resultDTO= new ResultDTO();

            if(ModelState.IsValid)
            {
                Category categoryBeforeAdd= new Category();
                categoryBeforeAdd.Name=subCategoryDTO.Name;
                categoryBeforeAdd.ParentCategoryID = subCategoryDTO.ParentCategoryID;
                Category categoryAfterAdd = _AdminService.AddCategory(categoryBeforeAdd);
                resultDTO.StatusCode= 200;
                resultDTO.Data= categoryAfterAdd;
            }
            else
            {
                resultDTO.StatusCode = 404;
                resultDTO.Data = ModelState;
            }
            return resultDTO;
        }

        [HttpPut("EditCategory")]
        public ResultDTO EditCategory(SubCategoryDTO subCategoryDTO)
        {
            ResultDTO resultDTO = new ResultDTO();
            if (ModelState.IsValid)
            {
                Category categoryBeforeEdit = _AdminService.GetCategoryByID(subCategoryDTO.ID);
                categoryBeforeEdit.Name = subCategoryDTO.Name;
                categoryBeforeEdit.ParentCategoryID = subCategoryDTO.ParentCategoryID;
                Category categoryAfterEdit = _AdminService.EditCategory(categoryBeforeEdit);
                resultDTO.StatusCode = 200;
                resultDTO.Data = categoryAfterEdit; ;
            }
            else
            {
                resultDTO.StatusCode = 404;
                resultDTO.Data = ModelState;
            }
            return resultDTO;

        }
        [HttpDelete("DeleteCategory")]
        public ResultDTO DeleteCategory(int id)
        {
            _AdminService.DeleteCategory(id);
            ResultDTO resultDTO = new ResultDTO();
            resultDTO.StatusCode = 200;
            resultDTO.Message = "Deleted Successfully";
            return resultDTO;
        }




        //////////////////////////////////////////  Filter /////////////////////////////////

        [HttpGet("GetAllFilters")]
        public ResultDTO GetAllFilters()
        {
            ResultDTO resultDTO = new ResultDTO();
            var filters = _AdminService.GetAllFilters();
            resultDTO.StatusCode = 200;
            resultDTO.Data = filters;
            return resultDTO;
        }


        [HttpPost("PostFilter")]
        public ResultDTO PostFilter(FilterDTO filterDTO)
        {
            ResultDTO resultDTO = new ResultDTO();

            if (ModelState.IsValid)
            {
                Filter filterBeforeAdd = new Filter();
                filterBeforeAdd.Name = filterDTO.Name;
                Filter filterAfterAdd = _AdminService.AddFilter(filterBeforeAdd);
                resultDTO.StatusCode = 200;
                resultDTO.Data = filterAfterAdd;
            }
            else
            {
                resultDTO.StatusCode = 404;
                resultDTO.Data = ModelState;
            }
            return resultDTO;
        }

        [HttpPut("EditFilter")]
        public ResultDTO EditFilter(FilterDTO filterDTO)
        {
            ResultDTO resultDTO = new ResultDTO();
            if (ModelState.IsValid)
            {
                Filter filter = _AdminService.GetFilterByID(filterDTO.ID);
                filter.Name = filterDTO.Name;
                _AdminService.EditFilter(filter);
                resultDTO.StatusCode = 200;
                resultDTO.Data = filter; ;
            }
            else
            {
                resultDTO.StatusCode = 404;
                resultDTO.Data = ModelState;
            }
            return resultDTO;

        }

        [HttpDelete("DeleteFilter")]
        public ResultDTO DeleteFilter(int id)
        {
            _AdminService.DeleteFilter(id);
            ResultDTO resultDTO = new ResultDTO();
            resultDTO.StatusCode = 200;
            resultDTO.Message = "Deleted Successfully";
            return resultDTO;
        }


        ////////////////////////////////////////// SubCategory Filters /////////////////////////////////
        
        [HttpGet("GetAllSubCategoriesFilters")]
        public ResultDTO GetAllSubCategoriesFilters()
        {
            ResultDTO resultDTO = new ResultDTO();
            List<SubCatFilterDTO> subCatFilterDTOs = new List<SubCatFilterDTO>();
            SubCatFilterDTO subCatFilterDTO;
            var subCategoriesFilters = _AdminService.GetAllSubCategoryFilters();
            var filterList = _AdminService.GetAllFilters();
            var subCategoryList = _AdminService.GetSubCategoryList();
            foreach (var subCatFilter in subCategoriesFilters)
            {
                subCatFilterDTO = new SubCatFilterDTO();
                subCatFilterDTO.ID= subCatFilter.ID;
                subCatFilterDTO.FilterID= subCatFilter.FilterID;
                subCatFilterDTO.FilterName = subCatFilter.Filter.Name;
                subCatFilterDTO.SubCatID = subCatFilter.SubCategoryID;
                subCatFilterDTO.SubCatName = subCatFilter.SubCategory.Name;
                subCatFilterDTOs.Add(subCatFilterDTO);
            }
            resultDTO.StatusCode = 200;
            resultDTO.Data = new { subCatFilterDTOs = subCatFilterDTOs , filterList = filterList ,
            subCategoryList = subCategoryList  };
            return resultDTO;
        }

        [HttpPost("PostSubCategoryFilter")]
        public ResultDTO PostSubCategoryFilter(SubCatFilterDTO subCatFilterDTO)
        {
            ResultDTO resultDTO = new ResultDTO();

            if (ModelState.IsValid)
            {
                SubCategory_Filter subCategory_Filter =new SubCategory_Filter();
                subCategory_Filter.FilterID = subCatFilterDTO.FilterID;
                subCategory_Filter.SubCategoryID = subCatFilterDTO.SubCatID;
                SubCategory_Filter SubCategory_FilterAfter = _AdminService.PostSubCategoryFilter(subCategory_Filter);
                subCatFilterDTO.ID = subCategory_Filter.ID;
                subCatFilterDTO.SubCatName = SubCategory_FilterAfter.SubCategory.Name;
                subCatFilterDTO.FilterName = SubCategory_FilterAfter.Filter.Name;
                resultDTO.StatusCode = 200;
                resultDTO.Data = subCatFilterDTO;
            }
            else
            {
                resultDTO.StatusCode = 404;
                resultDTO.Data = ModelState;
            }
            return resultDTO;
        }


        [HttpPut("EditSubCategoryFilter")]
        public ResultDTO EditSubCategoryFilter(SubCatFilterDTO subCatFilterDTO)
        {
            ResultDTO resultDTO = new ResultDTO();

            if (ModelState.IsValid)
            {
                SubCategory_Filter subCategory_Filter = _AdminService.GetSubCatFilterByID(subCatFilterDTO.ID);
                subCategory_Filter.FilterID = subCatFilterDTO.FilterID;
                subCategory_Filter.SubCategoryID = subCatFilterDTO.SubCatID;
                SubCategory_Filter SubCategory_FilterAfter = _AdminService.EditSubCategoryFilter(subCategory_Filter);
                subCatFilterDTO.SubCatName = SubCategory_FilterAfter.SubCategory.Name;
                subCatFilterDTO.FilterName = SubCategory_FilterAfter.Filter.Name;
                resultDTO.StatusCode = 200;
                resultDTO.Data = subCatFilterDTO;
            }
            else
            {
                resultDTO.StatusCode = 404;
                resultDTO.Data = ModelState;
            }
            return resultDTO;
        }

        [HttpDelete("DeleteSubCatFilter")]
        public ResultDTO DeleteSubCatFilter(int id)
        {
            _AdminService.DeleteSubCatFilter(id);
            ResultDTO resultDTO = new ResultDTO();
            resultDTO.StatusCode = 200;
            resultDTO.Message = "Deleted Successfully";
            return resultDTO;
        }


        ////////////////////////////////////////// SubCategory Filters Values /////////////////////////////////
       
        [HttpGet("GetAllFiltrationValues")]
        public ResultDTO GetAllFiltrationValues()
        {
            ResultDTO resultDTO = new ResultDTO();
            List<FilterValueDTO> filterValueDTOs = new List<FilterValueDTO>();
            FilterValueDTO filterValueDTO;
            var filterValuesList = _AdminService.GetAllFiltrationValues();
            var subCategoriesFilters = _AdminService.GetAllSubCategoryFilters();

            foreach (var filterValue in filterValuesList)
            {
                filterValueDTO = new FilterValueDTO();
                filterValueDTO.ID=filterValue.ID;
                filterValueDTO.Value= filterValue.Value;
                filterValueDTO.SubCategory_FilterID= filterValue.SubCategory_FilterID;
                filterValueDTO.SubCategory_FilterName = filterValue.SubCategory_Filter.SubCategory.Name +" # "+ filterValue.SubCategory_Filter.Filter.Name;
                filterValueDTOs.Add(filterValueDTO);
            }
            resultDTO.StatusCode = 200;
            resultDTO.Data = new
            {
                filterValueDTOs = filterValueDTOs,
                subCategoriesFilters = subCategoriesFilters
            };
            return resultDTO;
        }

        [HttpPost("PostFilterValue")]
        public ResultDTO PostFilterValue(FilterValueDTO filterValueDTO)
        {
            ResultDTO resultDTO = new ResultDTO();

            if (ModelState.IsValid)
            {
                FiltrationValue filtrationValue = new FiltrationValue();
                filtrationValue.Value = filterValueDTO.Value;
                filtrationValue.SubCategory_FilterID=filterValueDTO.SubCategory_FilterID;
                FiltrationValue filtrationValueAfter = _AdminService.PostFilterValue(filtrationValue);
                filterValueDTO.ID=filtrationValueAfter.ID;
                filterValueDTO.SubCategory_FilterName= filtrationValueAfter.SubCategory_Filter.SubCategory.Name + " # " + filtrationValueAfter.SubCategory_Filter.Filter.Name;
                resultDTO.StatusCode = 200;
                resultDTO.Data = filterValueDTO;
            }
            else
            {
                resultDTO.StatusCode = 404;
                resultDTO.Data = ModelState;
            }
            return resultDTO;
        }


        [HttpPut("EditFilterValue")]
        public ResultDTO EditFilterValue(FilterValueDTO filterValueDTO)
        {
            ResultDTO resultDTO = new ResultDTO();

            if (ModelState.IsValid)
            {
                FiltrationValue filtrationValue =_AdminService.GetFilterValueByID(filterValueDTO.ID);   
                filtrationValue.Value=filterValueDTO.Value;
                filtrationValue.SubCategory_FilterID= filterValueDTO.SubCategory_FilterID;
                FiltrationValue filtrationValueAfter = _AdminService.EditFilterValue(filtrationValue);
                filterValueDTO.SubCategory_FilterName = filtrationValueAfter.SubCategory_Filter.SubCategory.Name + " # " + filtrationValueAfter.SubCategory_Filter.Filter.Name;
                resultDTO.StatusCode = 200;
                resultDTO.Data = filterValueDTO;
            }
            else
            {
                resultDTO.StatusCode = 404;
                resultDTO.Data = ModelState;
            }
            return resultDTO;
        }

        [HttpDelete("DeleteFilterValue")]
        public ResultDTO DeleteFilterValue(int id)
        {
            _AdminService.DeleteFilterValue(id);
            ResultDTO resultDTO = new ResultDTO();
            resultDTO.StatusCode = 200;
            resultDTO.Message = "Deleted Successfully";
            return resultDTO;
        }


        ////////////////////////////////////////// Packages ////////////////////////////////////////

        [HttpGet("GetAllPackages")]
        public ResultDTO GetAllPackages()
        {
            ResultDTO resultDTO = new ResultDTO();
            List<Package> packages = (List<Package>)_AdminService.GetAllPackages();
            var filterList = _AdminService.GetAllFilters();
            var subCategoryList = _AdminService.GetSubCategoryList();
            List<PackageDTO> packageDTOs = new List<PackageDTO>();
            PackageDTO packageDTO;
            foreach(var package in packages)
            {
                packageDTO = new PackageDTO();
                packageDTO.ID= package.ID;
                packageDTO.Name = package.Name;
                packageDTO.NumOfAds = package.NumOfAds;
                packageDTO.NumOfPremiumDays = package.NumOfPremiumDays;
                packageDTO.Cost= package.Cost;
                packageDTO.AdDuration= package.AdDuration;
                packageDTO.SubCategoryID=package.SubCategory.ID;
                packageDTO.SubCategoryName=package.SubCategory.Name;
                packageDTOs.Add(packageDTO);
            }
            resultDTO.StatusCode = 200;
            resultDTO.Data = new
            {
                packageDTOs = packageDTOs,
                filterList = filterList,
                subCategoryList = subCategoryList
            };
            return resultDTO;
        }

        [HttpPost("PostPackage")]
        public ResultDTO PostPackage(PackageDTO packageDTO)
        {
            ResultDTO resultDTO = new ResultDTO();

            if (ModelState.IsValid)
            {
                Package package = new Package();
                package.Name= packageDTO.Name;
                package.NumOfAds= packageDTO.NumOfAds;
                package.NumOfPremiumDays= packageDTO.NumOfPremiumDays;
                package.Cost= packageDTO.Cost;  
                package.AdDuration= packageDTO.AdDuration;
                package.SubCategoryID= packageDTO.SubCategoryID;
                Package packageAfter = _AdminService.PostPackage(package);
                packageDTO.ID=packageAfter.ID;
                packageDTO.SubCategoryName=packageAfter.SubCategory.Name;
                resultDTO.StatusCode = 200;
                resultDTO.Data = packageDTO;
            }
            else
            {
                resultDTO.StatusCode = 404;
                resultDTO.Data = ModelState;
            }
            return resultDTO;
        }


        [HttpPut("EditPackage")]
        public ResultDTO EditPackage(PackageDTO packageDTO)
        {
            ResultDTO resultDTO = new ResultDTO();

            if (ModelState.IsValid)
            {
                Package package = _AdminService.GetPackageByID(packageDTO.ID);
                package.Name= packageDTO.Name;
                package.NumOfAds= packageDTO.NumOfAds;
                package.NumOfPremiumDays = packageDTO.NumOfPremiumDays;
                package.Cost= packageDTO.Cost;
                package.AdDuration = packageDTO.AdDuration;
                package.SubCategoryID = packageDTO.SubCategoryID;
                Package packageAfter = _AdminService.EditPackage(package);
                packageDTO.SubCategoryName= packageAfter.SubCategory.Name;
                resultDTO.StatusCode = 200;
                resultDTO.Data = packageDTO;
            }
            else
            {
                resultDTO.StatusCode = 404;
                resultDTO.Data = ModelState;
            }
            return resultDTO;
        }

        [HttpDelete("DeletePackage")]
        public ResultDTO DeletePackage(int id)
        {
            _AdminService.DeletePackage(id);
            ResultDTO resultDTO = new ResultDTO();
            resultDTO.StatusCode = 200;
            resultDTO.Message = "Deleted Successfully";
            return resultDTO;
        }


        ////////////////////////////////////////// Advertisments /////////////////////////////////

        [HttpGet("GetAllNotActiveAdvertisments")]
        public ResultDTO GetAllNotActiveAdvertisments()
        {
            ResultDTO resultDTO = new ResultDTO();

            var Advertisments = _AdminService.GetAllNotActiveAdvertisments();
            List<NotActiveAdvertismntDTO> NotActiveadvertismentDTOs = new List<NotActiveAdvertismntDTO>();
            NotActiveAdvertismntDTO NotActiveadvertismentDTO;
            foreach(var ad in Advertisments)
            {
                NotActiveadvertismentDTO= new NotActiveAdvertismntDTO();
                NotActiveadvertismentDTO.ID= ad.ID;
                NotActiveadvertismentDTO.Title = ad.Title;
                NotActiveadvertismentDTO.SubCategoryName = ad.SubCategory.Name;
                NotActiveadvertismentDTO.Image = ad.AdvertismentImagesList[0].ImageName;
                NotActiveadvertismentDTOs.Add(NotActiveadvertismentDTO);
            }
            resultDTO.StatusCode = 200;
            resultDTO.Data= NotActiveadvertismentDTOs;
            return resultDTO;
        }

        [HttpPut("EditAdvertisment")]
        public ResultDTO EditAdvertisment(int id)
        {
            ResultDTO resultDTO = new ResultDTO();

            if (ModelState.IsValid)
            {
               Advertisment advertisment = _AdminService.GetAdvertismentByID(id);
                advertisment.AdStatus = "Active";
                _AdminService.EditAdvertismentState(advertisment);
                resultDTO.StatusCode = 200;
            }
            else
            {
                resultDTO.StatusCode = 404;
                resultDTO.Data = ModelState;
            }
            return resultDTO;
        }


        ////////////////////////////////////////// Users ////////////////////////////////////////



    }
}
