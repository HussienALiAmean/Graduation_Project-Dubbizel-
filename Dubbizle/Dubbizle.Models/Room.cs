using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.Models
{
    public class Room : BaseModel
    {

        [ForeignKey("Sender")]
        public string SenderId { get; set; }
        [ForeignKey("Reciver")]
        public string ReceiverId { get; set; }
        [ForeignKey("Advertisment")]

        public int AdvertismentID { get; set; }
        public Advertisment Advertisment { get; set; }
        public bool Sold { get; set; }
        public virtual ApplicationUser Sender { get; set; }
        public virtual ApplicationUser Reciver { get; set; }


    }
}



