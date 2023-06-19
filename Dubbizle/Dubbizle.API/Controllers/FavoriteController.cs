using Dubbizle.DTOs;
using Dubbizle.Models;
using Dubbizle.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dubbizle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        
        favoriteService _fs;
        public FavoriteController(favoriteService fs)
        {
            _fs= fs;
        }
        [HttpPost("AddAdvertismentToFavorite")]
        public IActionResult AddFavorite(FavorieDTO favoriteDTO)
        {
            _fs.AddFavorit(favoriteDTO);
            return Ok(favoriteDTO);
        }
    }
}
