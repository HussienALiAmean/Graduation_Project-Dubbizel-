namespace Dubbizle.Models

{
    public class RentOption : BaseModel
    {
        public string Unit { get; set; }
        public int Duration { get; set; }
        public List<Advertisment_RentOption> Advertisment_RentOptionList { get; set; }

    }
}
