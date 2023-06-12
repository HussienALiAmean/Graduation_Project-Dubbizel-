namespace Dubbizle.Models

{
    public class FiltrationValue : BaseModel 
    {
        public string Value { get; set; }
        public SubCategory_Filter SubCategory_Filter { get; set; }
        public int SubCategory_FilterID { get; set; }
        public List<Advertisment_FiltrationValue> Advertisment_FiltrationValuesList { get; set; }

    }
}
