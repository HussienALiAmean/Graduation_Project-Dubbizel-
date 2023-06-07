using System.ComponentModel.DataAnnotations.Schema;

namespace Dubbizle.Models

{
    public class SubCategory_Filter : BaseModel 
    {
        public Category SubCategory { get; set; }
        [ForeignKey("SubCategory")]
        public int SubCategoryID { get; set; }
        public Filter Filter { get; set; }
        public int FilterID { get; set; }
    }
}
