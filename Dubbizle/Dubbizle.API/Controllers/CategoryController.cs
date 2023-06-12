using Dubbizle.DTOs;
using Dubbizle.Services;
using Microsoft.AspNetCore.Http;
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
        
    }
}
