
namespace Dubbizle.Models
{
    public class Filter : BaseModel
    {
        public string Name { get; set; }
        public List<FiltrationValue> FiltrationValuesList { get; set; }
        public List<SubCategory_Filter> SubCategory_FiltersList { get; set; }
        public bool IsDeleted { get; set; }


    }
}
