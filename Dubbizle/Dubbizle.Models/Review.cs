using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dubbizle.Models

{
    public class Review : BaseModel
    {
        public int Rate { get; set; }
        public string Text { get; set; }
        public Advertisment Advertisment { get; set; }
        public int AdvertismentID { get; set; }
       
        public ApplicationUser Auther { get; set;}
        [ForeignKey("Auther")]
        public string AutherId { get; set;}

    }
}
