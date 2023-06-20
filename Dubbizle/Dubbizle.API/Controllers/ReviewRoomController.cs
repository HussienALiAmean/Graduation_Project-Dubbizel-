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
        ReviewService _reviewService;
        public ReviewRoomController(IRepository<Review> reviewRepo, ReviewService reviewService)
        {
            _reviewRepo = reviewRepo;
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

        [HttpPost]
        public async Task<ActionResult<Review>> AddReview(ReviewDto reviewDTO)
        {
            
            Review review = new Review();
            review.Text = reviewDTO.text;
            review.Rate = reviewDTO.Rate;
            review.AutherId = reviewDTO.AutherId;
            review.AdvertismentID = reviewDTO.AdvertismentID;
            _reviewService.AddReview(review);
            reviewDTO.ID = review.ID;
            return Ok(reviewDTO);
        }

        [HttpPut]
        public async Task<ActionResult<ReviewDto>> EditReview(ReviewDto reviewDTO)
        {
            _reviewService.UpdateReview(reviewDTO);

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
