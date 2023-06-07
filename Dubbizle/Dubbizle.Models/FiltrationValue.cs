namespace Dubbizle.Models

{
    public class FiltrationValue : BaseModel 
    {
        public string Value { get; set; }
        public Filter Filter { get; set; }
        public int FilterID { get; set; }
        public List<Advertisment_FiltrationValue> Advertisment_FiltrationValuesList { get; set; }

    }
}
