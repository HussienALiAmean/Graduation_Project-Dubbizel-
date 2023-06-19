
using System.ComponentModel.DataAnnotations.Schema;

namespace Dubbizle.Models
{
    public class Advertisment : BaseModel
    {
        public string Title { get; set; }
        public string Location { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        //public DateTime PostedAt { get; set; }
        public Category Category { get; set; }
        public int CategoryID { get; set; }
        public Category SubCategory { get; set; }
        [ForeignKey("SubCategory")]
        public int SubCategoryID { get; set; }
        public string AdType { get; set; }
        public string AdStatus { get; set; }
        //public string Location { get; set; }
        public DateTime Date { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime ExpireDateOfPremium { get; set;}
        public List<AdvertismentImage> AdvertismentImagesList { get; set; }
        public List<Advertisment_FiltrationValue> Advertisment_FiltrationValuesList { get; set; }   
        public List<Advertisment_RentOption> Advertisment_RentOptionList { get; set; }  
        public List<Favorite> FavoritesList { get; set; }
        public List<Review> ReviewsList { get; set; }
    }
}
