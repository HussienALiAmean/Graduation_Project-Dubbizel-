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
            ResultDTO resultDTO = new ResultDTO();
            resultDTO.Data = (List<Category>)_categoryServise.GetAll("SubCategoriesList");
            resultDTO.StatusCode = 200;
            return Ok(resultDTO);
        }


        [HttpGet("categoryId")]
        public async Task<IActionResult> GetAllbyId(int categoryId)
        {
            ResultDTO resultDTO = new ResultDTO();
            resultDTO.Data = (List<Category>)_categoryServise.GetAllByID("SubCategoriesList", categoryId);
            resultDTO.StatusCode= 200;
            return Ok(resultDTO);
        }
    }
}
