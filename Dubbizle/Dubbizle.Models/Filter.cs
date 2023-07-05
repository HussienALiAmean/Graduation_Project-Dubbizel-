
using System.Text.Json.Serialization;

namespace Dubbizle.Models
{
    public class Filter : BaseModel
    {
        public string Name { get; set; }
        [JsonIgnore]
        public List<SubCategory_Filter> SubCategory_FiltersList { get; set; }


    }
}
