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
    public class CategoryController : ControllerBase
    {
        private readonly CategoryServise _categoryServise;

        public CategoryController(CategoryServise categoryServise)
        {
            _categoryServise= categoryServise;  
        }


        // Alzhraa 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Category> categories = (List<Category>)_categoryServise.GetAll("SubCategoriesList");
            List< CategoryDTO > categoriesDTO = new List<CategoryDTO>(); 
            List<SubCategoryDTO> subCategoryDTOs;
            CategoryDTO categoryDTO;
            SubCategoryDTO subCategoryDTO;
            foreach (var category in categories)
            {
                categoryDTO=new CategoryDTO();
                categoryDTO.ID = category.ID;
                categoryDTO.Name = category.Name;
                subCategoryDTOs=new List<SubCategoryDTO>();
                foreach (var subCategory in category.SubCategoriesList)
                {
                    subCategoryDTO=new SubCategoryDTO();
                    subCategoryDTO.ID = subCategory.ID;
                    subCategoryDTO.Name= subCategory.Name;
                    subCategoryDTOs.Add(subCategoryDTO);
                }
                categoryDTO.subCategoryDTOs=subCategoryDTOs;
                categoriesDTO.Add(categoryDTO);
            }
            return Ok(categoriesDTO);  
        }
    }
}
