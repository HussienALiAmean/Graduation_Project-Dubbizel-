using Dubbizle.Data.Repository;
using Dubbizle.DTOs;
using Dubbizle.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Dubbizle.API.Hubs
{
    public class ReviewHub : Hub
    {
     
      
        public async void NewReview(ReviewDto reviewDTO)
        {
            Clients.All.SendAsync("NewReviewNotify", reviewDTO);
        }
        //public async void RemoveReview(int id)
        //{
        //    Clients.All.SendAsync("RemoveReviewNotify", id);
        //}
        public async void RemoveReview(int index)
        {
            await Clients.All.SendAsync("RemoveReviewNotify", index);

        }


        public async void EditReview(int editIndex,ReviewDto newReviewDTO)
        {
            Clients.All.SendAsync("EditReviewNotify", editIndex, newReviewDTO);
        }
    }
}
