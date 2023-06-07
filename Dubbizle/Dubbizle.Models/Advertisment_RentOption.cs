namespace Dubbizle.Models

{
    public class Advertisment_RentOption : BaseModel
    {
        public Advertisment Advertisment { get; set; }
        public int AdvertismentID { get; set; }
        public RentOption RentOption { get; set;}
        public int RentOptionID { get; set; }
        public float Cost { get; set; }

    }
}
