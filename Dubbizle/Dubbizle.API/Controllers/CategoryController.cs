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
    public class CategoryController : ControllerBase
    {
        CategoryServise categoryServise;
        public CategoryController( CategoryServise _categoryServise) {
            categoryServise = _categoryServise;
         }
        [HttpGet("CategoriesWithSubcategoriesAndAdvertisment")]
        public IEnumerable<CategoryWithSubCategoriesDTO> GetCategoriesWithSubCategories()
        {
            var categories = categoryServise.GetCategoryWithSubCategories(c=>c.ParentCategoryID==null);
            return categories;

        }
        
        


        // Alzhraa 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ResultDTO resultDTO = new ResultDTO();
<<<<<<< HEAD
            resultDTO.Data = (List<Category>)_categoryServise.GetAll("SubCategoriesList");
            resultDTO.StatusCode = 200;
            return Ok(resultDTO);
        }


        [HttpGet("categoryId")]
        public async Task<IActionResult> GetAllbyId(int categoryId)
        {
            ResultDTO resultDTO = new ResultDTO();
            resultDTO.Data = (List<Category>)_categoryServise.GetAllByID("SubCategoriesList", categoryId);
=======
            resultDTO.Data = (List<Category>)categoryServise.GetAll("SubCategoriesList");
>>>>>>> c249ac7b5e103d1984d12f50ca8bd6586c4a77a5
            resultDTO.StatusCode= 200;
            return Ok(resultDTO);
        }
    }
}
