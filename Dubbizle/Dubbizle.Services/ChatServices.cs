using Dubbizle.Data.Repository;
using Dubbizle.DTOs;
using Dubbizle.Models;
using Dubbizle.API.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dubbizle.Services
{
    public class ChatServices
    {
        private readonly IRepository<Chat> _chatRepository;

        public ChatServices(IRepository<Chat> repository
            )
        {
            _chatRepository = repository;
           
        }

        public async Task<string> Add(ChatDTO Msg)
        {
            if (Msg != null)
            {
                Chat chat = new Chat();
                chat.SenderID = Msg.SenderID;
                chat.ReciverID = Msg.ReceiverID;
                chat.Content = Msg.Content;
                if (Msg.Image != null)
                {
                    chat.File = ImagesHelper.UploadImg(Msg.Image, "ChatImages");
                }
                chat.Date = DateTime.Now;

                _chatRepository.Add(chat);
                _chatRepository.SaveChanges();
                return "Message has been created successfully";
            }
            else
            {
                return "BadRequest()";
            }

        }
        public int total { get; set; }
        public async Task<dynamic> Get(string sender, string reciver)
        {
           if(sender != null || reciver != null)
            {



                var chats = _chatRepository.GetAll(c => c.Deleted == false &&
                    (c.SenderID == sender && c.ReciverID == reciver) 
                    ||(c.ReciverID == sender && c.SenderID == reciver))
                        .Include(r => r.Sender).Include(r => r.Reciver)
                    .Take(29)
                    .Select(x => new { gender = x.Reciver.Gender, ReseverName = x.Reciver.UserName, contenten = x.Content, image = x.File })
                    .ToList();
         
                total = chats.Count();
                //IEnumerable<Chat> pageChat =
                //    _chatRepository.GetAll(c => c.Deleted == false &&
                // (c.SenderID == sender && c.ReciverID == reciver) ||
                // (c.ReciverID == sender && c.SenderID == reciver))
                //    .Skip(1)
                //    .Take(total)
                // .ToList();
                IEnumerable<Chat> chat = _chatRepository.GetAll(c => c.Deleted == false &&
                    (c.SenderID == sender && c.ReciverID == reciver)
                    || (c.ReciverID == sender && c.SenderID == reciver))
                    .ToList();
                //.Include(r => r.Sender).Include(r => r.Reciver)
                //.OrderBy(c => c.ID)
                //.Take(9)

                return chat;
            }
            else
            {
                return "sender and receiver should not be null";
            }
           

        }
    }
}
