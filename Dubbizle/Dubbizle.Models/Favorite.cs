namespace Dubbizle.Models

{
    public class Favorite : BaseModel 
    {
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public Advertisment Advertisment { get; set; }
        public int AdvertismentID { get; set; } 
    }
}
