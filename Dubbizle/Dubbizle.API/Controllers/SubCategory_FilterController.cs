using Dubbizle.DTOs;
using Dubbizle.Models;
using Dubbizle.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dubbizle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategory_FilterController : ControllerBase
    {
        private readonly SubCategory_FilterService _subCategory_FilterService;

        public SubCategory_FilterController(SubCategory_FilterService subCategory_FilterService)
        {
            _subCategory_FilterService = subCategory_FilterService;  
        }


        // Alzhraa & Hussien
        [HttpGet("subCategoryID")]
        public async Task<IActionResult> GetAllBySubCategoryID(int subCategoryID)
        {
            ResultDTO resultDTO = new ResultDTO();

            List<SubCategory_Filter> SubCategory_Filters = 
                (List<SubCategory_Filter>)_subCategory_FilterService.GetAllBySubCategoryID("Filter", "FiltrationValuesList", subCategoryID);
           
            List< SubCategory_FilterDTO > subCategory_FilterDTOs = new List<SubCategory_FilterDTO>();
            SubCategory_FilterDTO subCategory_FilterDTO;

            foreach (SubCategory_Filter SubCatFilter in SubCategory_Filters)
            {
                subCategory_FilterDTO = new SubCategory_FilterDTO();
                subCategory_FilterDTO.FilterID = SubCatFilter.ID;
                subCategory_FilterDTO.FilterName = SubCatFilter.Filter.Name; ;
                subCategory_FilterDTO.FiltrationValuesList = new List<string>();
                foreach (FiltrationValue filtrationValue in SubCatFilter.FiltrationValuesList)
                {
                    subCategory_FilterDTO.FiltrationValuesList.Add(filtrationValue.Value);
                }
                subCategory_FilterDTOs.Add(subCategory_FilterDTO);
            }
            resultDTO.StatusCode= 200;
            resultDTO.Data= subCategory_FilterDTOs;
            return Ok(resultDTO);
        }




        // Hussien
        [HttpGet("GetAllFiltersWithAndValuesID")]
        public async Task<IActionResult> GetAllFiltersWithAndValuesID(int subCategoryID)
        {
            ResultDTO resultDTO = new ResultDTO();

            List<SubCategory_Filter> SubCategory_Filters =
                (List<SubCategory_Filter>)_subCategory_FilterService.GetAllBySubCategoryID("Filter", "FiltrationValuesList", subCategoryID);

            List<SubCategory_FilterWithIDsDTO> subCategory_FilterWithIDsDTOs = new List<SubCategory_FilterWithIDsDTO>();
            SubCategory_FilterWithIDsDTO subCategory_FilterWithIDsDTO;

            foreach (SubCategory_Filter SubCatFilter in SubCategory_Filters)
            {
                subCategory_FilterWithIDsDTO = new SubCategory_FilterWithIDsDTO();
                subCategory_FilterWithIDsDTO.FilterName = SubCatFilter.Filter.Name; ;
                subCategory_FilterWithIDsDTO.FiltrationValuesList = new List<filterValuesDTO>();
                filterValuesDTO filterValuesDTO;
                foreach (FiltrationValue filtrationValue in SubCatFilter.FiltrationValuesList)
                {
                    filterValuesDTO = new filterValuesDTO(); 
                    filterValuesDTO.ID  = filtrationValue.ID;
                    filterValuesDTO.Value= filtrationValue.Value;
                    subCategory_FilterWithIDsDTO.FiltrationValuesList.Add(filterValuesDTO);
                }
                subCategory_FilterWithIDsDTOs.Add(subCategory_FilterWithIDsDTO);
            }
            resultDTO.StatusCode = 200;
            resultDTO.Data = subCategory_FilterWithIDsDTOs;
            return Ok(resultDTO);
        }

    }
}
