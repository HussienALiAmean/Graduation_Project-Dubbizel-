using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.DTOs
{
    public class ChatDTO
    {
        //public int ID { get; set; }
        public string SenderID { get; set; }
        public string ReceiverID { get; set; }
        public string Content { get; set; }
        public IFormFile? Image { get; set; }

        public bool Sold { get; set; }
        public int AdvertismentID { get; set; }

    }
}
