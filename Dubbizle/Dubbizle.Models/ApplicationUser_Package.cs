namespace Dubbizle.Models

{
    public class ApplicationUser_Package : BaseModel
    {
        public ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }
        public Package Package { get; set; }
        public int PackageID { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int NumOfRemainAds { get; set; }



    }
}
