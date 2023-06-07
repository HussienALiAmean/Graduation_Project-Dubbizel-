using System.ComponentModel.DataAnnotations.Schema;

namespace Dubbizle.Models

{
    public class Package : BaseModel
    {
        public string Name { get; set; }
        public string Advantage { get; set; }

        public int NumOfAds { get; set; }
        public int NumOfPremiumDays { get; set; }
        public float Cost { get; set; }
        public int AdDuration { get; set; }
        public Category SubCategory { get; set;}
        [ForeignKey("SubCategory")]
        public int SubCategoryID { get; set; }
        public List<ApplicationUser_Package> ApplicationUser_PackagesList { get; set; }



    }
}
