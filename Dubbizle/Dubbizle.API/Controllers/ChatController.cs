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

namespace Dubbizle.API.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
    public class ChatController : Controller
    {
       private readonly ChatServices chatServices;
        private readonly IRepository<Chat> _chatRepository;
        readonly UserManager<ApplicationUser> _userManager;
        public ChatController(
            UserManager<ApplicationUser> userManager
          , IRepository<Chat> repository
            , ChatServices chatServices)
        {
          this.chatServices = chatServices;
            _userManager = userManager;
            _chatRepository = repository;
        }

        [HttpPost("AddMessage")]
        public IActionResult AddMessage([FromForm] ChatDTO Msg)
        {
           
            return Ok(chatServices.Add(Msg));
            //MessageRepo.AddMessage(Msg);
        }
        [HttpGet("GetMessages")]
        public async Task<IActionResult> GetMessages( string sender , string reciver)
        {
            var chats = await chatServices.Get(sender, reciver);
            //List<Chat> chats

            //     = _chatRepository.GetAll(c => (c.SenderID == sender && c.ReciverID == reciver) ||
            //     (c.ReciverID == sender && c.SenderID == reciver))
            //        .ToList();
            //.Include(r => r.Sender).Include(r => r.Reciver)

            return Ok(chatServices.Get(sender, reciver));

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
        [HttpGet()]
        public async Task<IActionResult> GetChatUsers(string id)
        {
            IEnumerable<Chat> chats = 
                _chatRepository
                .GetAll(c=> c.SenderID == id)
                .ToList();
            string ID = chats.FirstOrDefault().ReciverID;
            List<ApplicationUser> users = new List<ApplicationUser>();
            ApplicationUser user = await _userManager.FindByIdAsync(ID);
            users.Add(user);
            foreach (Chat chat in chats)
            {
                if(chat.ReciverID != ID)
                {
                    ApplicationUser applicationUser = new ApplicationUser();
                    applicationUser = await _userManager.FindByIdAsync(chat.ReciverID);
                    users.Add(applicationUser);
                }
            }
            users.Select(u => new { u.Gender,u.UserName });
            return Ok(users.Select(u => new { u.Gender, u.UserName }));
        }
    }
}
