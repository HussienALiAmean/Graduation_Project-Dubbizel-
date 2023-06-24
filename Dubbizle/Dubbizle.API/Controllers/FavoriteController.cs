using Dubbizle.Data;
using Dubbizle.Data.Repository;
using Dubbizle.Data.UnitOfWork;
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
        //FavoriteService favoriteService;
        //ReviewService reviewService;
      private  readonly IRepository<Favorite> favoriteRepository;
        //readonly UnitOfWork unitOfWork;
        private  readonly Context _Context;
        public FavoriteController(Context context,
            //ReviewService _reviewService,
            //UnitOfWork _unitOfWork,
            IRepository<Favorite> _repository
            //,FavoriteService _favoriteService
            ) {
            //favoriteService = _favoriteService;
            //unitOfWork = _unitOfWork;
            favoriteRepository = _repository;
            //reviewService = _reviewService;
            _Context = context;
        }



        //[HttpPost("AddAdvertismentToFavorite")]
        //public IActionResult AddFavorite(FavoriteDTO favoriteDTO)
        //{
        //    Favorite favorite = new Favorite();
        //    //Favorite favorite1 =new Favorite();
        //    favorite.AdvertismentID = favoriteDTO.AdvertismentID;
        //    favorite.ApplicationUserId = favoriteDTO.ApplicationUserId;
        //    favoriteRepository.create(favorite);
        //    return Ok(favoriteDTO);
        //}
        [HttpPost("AddAdvertismentToFavorite")]
        public async Task<IActionResult> AddAdvertismentToFavorite(FavoriteDTO favoriteDTO)
        {
            //favoriteService.AddFavorit(favoriteDTO);
            //return Ok(favoriteDTO);
            //Context context = _Context;
            Favorite favorite = new Favorite();
            //Favorite favorite1 =new Favorite();
            favorite.AdvertismentID = favoriteDTO.AdvertismentID;
            favorite.ApplicationUserId = favoriteDTO.ApplicationUserId;
            //context.Add(favorite);
            favoriteRepository.Add(favorite);
            favoriteRepository.SaveChanges();
            //_Context.Favorites.Add(favorite);
            //_Context.SaveChanges();
            return Ok(favorite);
        }

        //[HttpGet]
        //public IActionResult GetFavorite()
        //{
        //    List<Favorite> favorites = (List<Favorite>)favoriteService.GetAll();
        //    return Ok(favorites);
        //}
        //[HttpPost("AddReview")]
        //public async Task<ActionResult<Review>> AddReview( ReviewDto reviewDTO)
        //{
        //    //ApplicationUser orgUser = await _userManager.FindByIdAsync(ApplicationUserId);
        //    //orgUser.Id = reviewDTO.AutherId;

        //    reviewService.AddReview(reviewDTO);



        //    return Ok(reviewDTO);
        //}
    }
}
