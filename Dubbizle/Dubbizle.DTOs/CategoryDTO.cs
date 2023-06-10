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
    }
}
