using Dubbizle.Data.Repository;
using Dubbizle.DTOs;
using Dubbizle.Models;
using Dubbizle.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Dubbizle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewRoomController : ControllerBase
    {
        private IRepository<Review> _reviewRepo;
        private UserManager<ApplicationUser> _userManager;
        ReviewService _reviewService;
        public ReviewRoomController(IRepository<Review> reviewRepo,UserManager<ApplicationUser> userManager, ReviewService reviewService)
        {
            _reviewRepo = reviewRepo;
            _userManager = userManager;
            _reviewService = reviewService;
        }

        [HttpGet]
        public IActionResult GetAllReviews()
        {
           
            return Ok(_reviewService.GetAllReviews());
        }
        [HttpGet("display")]
        public IActionResult GetThreeReviews()
        {
            return Ok(_reviewService.GetThreeReviews());
        }

        [HttpPost("ApplicationUserId")]
        public async Task<ActionResult<Review>> AddReview(ReviewDTO reviewDTO, string ApplicationUserId)
        {
            ApplicationUser orgUser = await _userManager.FindByIdAsync(ApplicationUserId);
            orgUser.Id = reviewDTO.AutherId;
            Review review = new Review();
            review.Text = reviewDTO.text;
            review.Rate = reviewDTO.Rate;
            orgUser.UserName = reviewDTO.userName;
            review.AutherId = reviewDTO.AutherId;
            review.AdvertismentID = reviewDTO.AdvertismentID;
            _reviewRepo.Add(review);
            _reviewRepo.SaveChanges();


            return Ok(reviewDTO);
        }

        [HttpPut("reviewId")]
        public async Task<ActionResult<ReviewDTO>> EditReview(int reviewId, ReviewDTO reviewDTO)
        {
            _reviewService.UpdateReview(reviewId, reviewDTO);

            return Ok(reviewDTO);
        }

        [HttpDelete("Id")]
        public ActionResult DeleteReview(int Id)
        {
            _reviewService.DeleteReview(Id);
            return Ok();
        }

    }
}
