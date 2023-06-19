using Dubbizle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.DTOs
{
    public class FilterDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    // Alzhraa
    public class SubCatFilterDTO
    {
        public int ID { get; set; }
        public int FilterID { get; set; }
        public string? FilterName { get; set; }
        public int SubCatID { get; set; }
        public string? SubCatName { get; set; }

    }
    // Alzhraa
    public class FilterValueDTO
    {
        public int ID { get; set; }
        public string Value { get; set; }
        public int SubCategory_FilterID { get; set; }
        public string? SubCategory_FilterName { get; set;}
    }
}
