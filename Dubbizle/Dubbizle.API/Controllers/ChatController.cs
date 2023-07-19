using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Dubbizle.Data;
using Dubbizle.DTOs;
using Dubbizle.Models;
using Dubbizle.API.Helper;
using Microsoft.Extensions.Hosting;
using Dubbizle.Data.Repository;
using Dubbizle.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace Dubbizle.API.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
    public class ChatController : Controller
    {
       private readonly ChatServices chatServices;
        private readonly IRepository<Chat> _chatRepository;
        private readonly IRepository<Room> _roomRepository;
        readonly UserManager<ApplicationUser> _userManager;
        public ChatController(
            UserManager<ApplicationUser> userManager
          , IRepository<Chat> repository
            , IRepository<Room> roomRepository
            , ChatServices chatServices)
        {
          this.chatServices = chatServices;
            _userManager = userManager;
            _chatRepository = repository;
            _roomRepository = roomRepository;
        }
        // to Add message first create room then create message 
        [HttpPost("AddMessage")]
        public IActionResult AddMessage([FromForm] ChatDTO Msg)
        {
           
            return Ok(chatServices.Add(Msg));
            //MessageRepo.AddMessage(Msg);
        }
        [HttpGet("GetMessages")]
        public async Task<IActionResult> GetMessages( string sender , string reciver,int Advertisment,int top,int skip)
        {
            var chats = await chatServices.Get(sender, reciver, Advertisment, top,skip);
            

            return Ok(chatServices.Get(sender, reciver, Advertisment, top,skip));

        }
         [HttpGet("GetLastMessages")]
        public async Task<IActionResult> GetLastMessages( string sender , string reciver)
        {
            

            return Ok(chatServices.GetLast(sender, reciver));

        }

        [HttpPut]
        public async Task<IActionResult> Delete(int id)
        {
            _chatRepository.Delete(id);
            //Chat chat = _chatRepository.Delete(id);
            //chat.Deleted = true;
            //_chatRepository.Update(chat);
            _chatRepository.SaveChanges();
            return Ok("Your obj has been deleted");
        }

        [HttpPut("DeleteMessagesRoom")]
        public async Task<IActionResult> DeleteRoom(int id)
        {

            _roomRepository.Delete(id);
           Room room = _roomRepository.GetByID(id);
           List<Chat> chats = _chatRepository.GetAll(c=> c.RoomId == id).ToList();
            foreach (Chat chat in chats)
            {
                chat.Deleted = true;
            }
            //Chat chat = _chatRepository.Delete(id);
            //chat.Deleted = true;
            //_chatRepository.Update(chat);
            _chatRepository.SaveChanges();
            return Ok("Your Room has been deleted");
        }

        [HttpGet("GetChatUsers")]
        public async Task<IActionResult> GetChatUsers(string id, string loginId, int Advertisment)
        {
            if (id == loginId)
            {
                //IEnumerable<Chat> Ownerchats =
                //_chatRepository
                //.GetAll(c => c.SenderID == id || c.ReciverID == id)
                //.ToList();
                IEnumerable<Room> chats =
                    _roomRepository
                    .GetAll(c => c.Deleted == false && c.AdvertismentID == Advertisment && c.SenderId == id || c.ReceiverId == id)
                    .ToList();
                string ID = chats.FirstOrDefault().SenderId;
                List<ApplicationUser> users = new List<ApplicationUser>();
                ApplicationUser user = await _userManager.FindByIdAsync(ID);
                //users.Add(user);

                foreach (Room chat in chats)
                {
                    if (chat.ReceiverId != ID)
                    {
                        ApplicationUser applicationUser = new ApplicationUser();
                        applicationUser = await _userManager.FindByIdAsync(chat.SenderId);
                        users.Add(applicationUser);
                    }
                }
                users.Select(u => new { u.Gender, u.UserName });
                return Ok(users);
            }
            else
            {
                List<ApplicationUser> users = new List<ApplicationUser>();

                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser = await _userManager.FindByIdAsync(id);
                users.Add(applicationUser);

                return Ok(users);
            }
        }

        //[HttpGet("GetChatUsers")]
        //public async Task<IActionResult> GetChatUsers(string id, string loginId, int Advertisment)
        //{
        //    IEnumerable<Room> chats =
        //        _roomRepository
        //        .GetAll(c => c.Deleted == false && c.SenderId == loginId || c.ReceiverId == loginId)
        //        .ToList();
        //    List<ApplicationUser> users = new List<ApplicationUser>();
        //    ApplicationUser applicationUser;
        //    foreach (Room chat in chats)
        //    {
        //        if (chat.SenderId != loginId)
        //        {
        //            applicationUser = new ApplicationUser();
        //            applicationUser = await _userManager.FindByIdAsync(chat.SenderId);
        //            users.Add(applicationUser);
        //        }
        //        if (chat.ReceiverId != loginId)
        //        {
        //            applicationUser = new ApplicationUser();
        //            applicationUser = await _userManager.FindByIdAsync(chat.ReceiverId);
        //            users.Add(applicationUser);
        //        }
        //    }
        //    if (chats.Where(c => c.ReceiverId == id || c.SenderId == id).ToList().Count == 0)
        //    {
        //        applicationUser = new ApplicationUser();
        //        applicationUser = await _userManager.FindByIdAsync(id);
        //        users.Add(applicationUser);
        //    }
        //    return Ok(users);
        //}

    }
}
