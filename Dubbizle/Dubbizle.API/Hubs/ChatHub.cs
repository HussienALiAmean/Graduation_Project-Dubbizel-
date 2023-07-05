using Dubbizle.DTOs;
using Dubbizle.Models;
using Microsoft.AspNetCore.SignalR;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace Dubbizle.API.Hubs
{
    public class ChatHub : Hub
    {
        public async Task NewChatHub( string Content,string image)
        {
            //string content = message.Content;
            string img; 
            //if(message.Image != null)
            //{
            var date = DateTime.Now;
            // img = message.Image.FileName.ToString();
            //}
            //else
            //{
            //     img = "empty";
            //}
            await Clients.All.SendAsync("NewMessageNotify", Content, image, date);

        }

        public async Task ChatDelete(string id)
        {
            string hide = "d-none";
            await Clients.All.SendAsync("ChatDeleteNotify", hide,id);

        }
    }
}
