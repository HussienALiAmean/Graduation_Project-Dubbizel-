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
using Azure;
//using Dubbizle.Data.Migrations;

namespace Dubbizle.Services
{
    public class ChatServices
    {
        private readonly IRepository<Chat> _chatRepository;
        private readonly IRepository<Room> _roomRepository;
        public ChatServices(IRepository<Chat> repository
            , IRepository<Room> roomRopository
            )
        {
            _chatRepository = repository;
            _roomRepository = roomRopository;
            limit += 7;
        }

        public async Task<string> Add(ChatDTO Msg)
        {
            if (Msg != null)
            {
                //int AdvertismentId =int.Parse( Msg.AdvertismentID.ToString());
              Room Room = _roomRepository.GetObject(r => r.SenderId == Msg.SenderID || r.ReceiverId == Msg.SenderID 
              && r.ReceiverId == Msg.ReceiverID || r.SenderId == Msg.ReceiverID);
                Chat chat = new Chat();
                chat.SenderID = Msg.SenderID;
                chat.ReciverID = Msg.ReceiverID;
                chat.Content = Msg.Content;
                if (Msg.Image != null)
                {
                    chat.File = ImagesHelper.UploadImg(Msg.Image, "ChatImages");
                }
                chat.Date = DateTime.Now;
                if(Room is null)
                {
                    Room room = new Room();
                    room.AdvertismentID = Msg.AdvertismentID;
                    room.SenderId = Msg.SenderID;
                    room.ReceiverId = Msg.ReceiverID;
                    room.Sold = false;
                    _roomRepository.Add(room);
                    _roomRepository.SaveChanges();
                    chat.RoomId = room.ID;

                }
                else
                {
                    chat.RoomId = Room.ID;
                }

                _chatRepository.Add(chat);
                _chatRepository.SaveChanges();
                return "Message has been created successfully";
            }
            else
            {
                return "BadRequest Please input your data correctly";
            }

        }
         static int  limit { get;  set; }
        public int page { get; private set; }
        public int total { get; set; }
        public async Task<dynamic> Get(string sender, string reciver,int top, int Skip)
        {
           if(sender != null || reciver != null)
            {



                var chats = _chatRepository.GetAll(c => c.Deleted == false &&
                    (c.SenderID == sender && c.ReciverID == reciver) 
                    ||(c.ReciverID == sender && c.SenderID == reciver))
                        .Include(r => r.Sender).Include(r => r.Reciver)
                        .Skip(Skip)
                    .Take(top)
                    .Select(x => new { gender = x.Reciver.Gender, ReseverName = x.Reciver.UserName, contenten = x.Content, image = x.File })
                    .ToList();
         
                total = chats.Count();
               
                
                IEnumerable<Chat> pageChat =
                    _chatRepository.GetAll(c => c.Deleted == false &&
                 (c.SenderID == sender && c.ReciverID == reciver) ||
                 (c.ReciverID == sender && c.SenderID == reciver))
                    .Include(c=>c.Room)
                    .Skip(Skip)
                    .Take(top)
                 .ToList();
                
                return pageChat; 
            }
            else
            {
                return "sender and receiver should not be null";
            }
           

        }
    }
}
