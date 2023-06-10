using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dubbizle.Models

{
    public class Category: BaseModel
    {
        public string Name { get; set; }
        public Category? ParentCategory { get; set; }
        [ForeignKey("ParentCategory")]
        public int? ParentCategoryID { get; set; }
        public List<Category> SubCategoriesList { get; set; }

        [InverseProperty("Category")]
        public List<Advertisment> CategoryAdvertismentsList { get; set; }
        [InverseProperty("SubCategory")]
        public List<Advertisment> SubCaategoryAdvertismentsList { get; set; }
        public List<Package> SubCaategoryPackagesList { get; set; }
        public List<SubCategory_Filter> SubCategory_FilterList { get; set; }

    }
}