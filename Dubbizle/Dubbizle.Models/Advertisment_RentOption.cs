namespace Dubbizle.Models

{
    public class Advertisment_RentOption : BaseModel
    {
        public Advertisment Advertisment { get; set; }
        public int AdvertismentID { get; set; }
        public int Duration { get; set; }
        public string Unit { get; set; }
        public float Cost { get; set; }

    }
}
