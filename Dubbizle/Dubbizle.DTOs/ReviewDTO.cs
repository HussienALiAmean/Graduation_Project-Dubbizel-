using Dubbizle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.DTOs
{
    public class ReviewDTO
    {
        public int ID { get; set; }
       public string userName { get; set; }
        public string text { get; set; }
        public int Rate { get; set; }
        public string AutherId { get; set; }
        public int AdvertismentID { get; set; }

    }
    
}
