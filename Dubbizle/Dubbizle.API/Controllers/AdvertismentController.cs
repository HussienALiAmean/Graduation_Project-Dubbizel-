using Dubbizle.DTOs;
using Dubbizle.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dubbizle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertismentController : ControllerBase
    {
        AdvertismentService advertismentService;
        public AdvertismentController(AdvertismentService _advertismentService) { 
             advertismentService = _advertismentService;
        }

        [HttpGet("GetAllAdvertismentByCategory/categoryId")]
        public IEnumerable<AdvertismentDTO> GetAllAdvertismentByCategory(int categoryId)
        {
            var ads = advertismentService.GetAdvertismentsAll(c => c.CategoryID == categoryId);
            return ads;
        }
    }
}
