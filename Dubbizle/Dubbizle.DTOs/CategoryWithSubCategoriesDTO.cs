using Dubbizle.Models;

namespace Dubbizle.DTOs
{
    public class CategoryWithSubCategoriesDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int ParentCategoryID { get; set; }
        public List<SubCategoryDTO> SubCategoriesList { get; set; }
        public List<AdvertismentHomePageDTO> CategoryAdvertismentsList { get; set; }

    }
}