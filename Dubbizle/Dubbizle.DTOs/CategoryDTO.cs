using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.DTOs
{
    public class CategoryDTO
    {
        public int ID { get; set; } 
        public string Name { get; set; } 
        public List<SubCategoryDTO> subCategoryDTOs { get; set; }

    }

    public class SubCategoryDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryID { get; set; }

    }


    //List<CategoryDTO> categoriesDTO = new List<CategoryDTO>();
    //List<SubCategoryDTO> subCategoryDTOs;
    //CategoryDTO categoryDTO;
    //SubCategoryDTO subCategoryDTO;
    //foreach (var category in categories)
    //{
    //    categoryDTO = new CategoryDTO();
    //    categoryDTO.ID = category.ID;
    //    categoryDTO.Name = category.Name;
    //    subCategoryDTOs = new List<SubCategoryDTO>();
    //    foreach (var subCategory in category.SubCategoriesList)
    //    {
    //        subCategoryDTO = new SubCategoryDTO();
    //        subCategoryDTO.ID = subCategory.ID;
    //        subCategoryDTO.Name = subCategory.Name;
    //        subCategoryDTOs.Add(subCategoryDTO);
    //    }
    //    categoryDTO.subCategoryDTOs = subCategoryDTOs;
    //    categoriesDTO.Add(categoryDTO);
    //}
    //return Ok(categoriesDTO);
}
