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
        public async Task NewChatHub( ChatDTO message,string image)
        {
            string content = message.Content;
            string img; 
            if(message.Image != null)
            {

             img = message.Image.FileName.ToString();
            }
            else
            {
                 img = "empty";
            }
            await Clients.All.SendAsync("NewMessageNotify", content, image);

        }

        public async Task ChatDelete(string id)
        {
            string hide = "d-none";
            await Clients.All.SendAsync("ChatDeleteNotify", hide,id);

        }
    }
}
