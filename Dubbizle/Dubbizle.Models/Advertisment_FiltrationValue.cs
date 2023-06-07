namespace Dubbizle.Models

{
    public class Advertisment_FiltrationValue : BaseModel
    {
        public Advertisment Advertisment { get; set; }
        public int AdvertismentID { get; set; }
        public FiltrationValue filtrationValue { get; set; }
        public int FiltrationValueID { get; set;}
    }
}
