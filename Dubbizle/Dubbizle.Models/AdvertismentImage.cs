namespace Dubbizle.Models

{
    public class AdvertismentImage : BaseModel
    {
        public string ImageName { get; set; }
        public Advertisment Advertisment { get; set; }
        public int AdvertismentID { get; set; }
    }
}
