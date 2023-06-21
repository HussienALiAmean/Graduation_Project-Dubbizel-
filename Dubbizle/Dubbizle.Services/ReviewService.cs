using Dubbizle.Data.Repository;
using Dubbizle.DTOs;
using Dubbizle.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.Services
{
    public class ReviewService
    {
        private IRepository<Review> _reviewRepo;
        public ReviewService(IRepository<Review> reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }

        public List<Review> GetAllReviews()
        {
            List<Review> reviews = _reviewRepo.GetAll("Auther").Where(d => d.Deleted == false).ToList();
            return reviews;
        }
        public List<Review> GetThreeReviews()
        {
            List<Review> reviews = _reviewRepo.GetAll("Auther").Where(d => d.Deleted == false).Take(3).ToList();
            return reviews;
        }

        public async Task UpdateReview(ReviewDto reviewDTO)
        {
            Review review = _reviewRepo.GetAll().FirstOrDefault(d => d.ID == reviewDTO.ID);
            review.Text = reviewDTO.text;
            review.Rate = reviewDTO.Rate;
            _reviewRepo.Update(review);
            _reviewRepo.SaveChanges();
        }
        public void DeleteReview(int reviewId)
        {
            Review review = _reviewRepo.GetByID(reviewId);
            review.Deleted = true;
            _reviewRepo.Update(review);
            _reviewRepo.SaveChanges();
        }

        public Review AddReview(Review review)
        {
            Review reviewAfter = _reviewRepo.Add(review);
            _reviewRepo.SaveChanges();
            return reviewAfter;
        }

    }
}