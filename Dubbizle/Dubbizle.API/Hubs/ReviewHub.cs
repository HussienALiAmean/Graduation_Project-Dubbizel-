﻿using Dubbizle.Data.Repository;
using Dubbizle.DTOs;
using Dubbizle.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Dubbizle.API.Hubs
{
    public class ReviewHub : Hub
    {
        IRepository<Review> _reviewRepo;
        public ReviewHub(IRepository<Review> reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public async void NewReview(ReviewDto reviewDTO)
        {
            Clients.All.SendAsync("NewReviewNotify", reviewDTO);
        }
        public async void RemoveReview(ReviewDto reviewDTO)
        {
            Clients.All.SendAsync("RemoveReviewNotify", reviewDTO);
        }

        public async void EditReview(ReviewDto newReviewDTO)
        {
            Clients.All.SendAsync("EditReviewNotify", newReviewDTO);
        }
    }
}
