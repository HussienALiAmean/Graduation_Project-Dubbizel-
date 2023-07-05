using Dubbizle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.DTOs
{
    public class SubCategory_FilterDTO
    {
        public int FilterID { get; set; }
        public string FilterName { get; set; }
       public List<string> FiltrationValuesList { get; set; }

    }

    public class SubCategory_FilterWithIDsDTO
    {
        public string FilterName { get; set; }
        public List<filterValuesDTO> FiltrationValuesList { get; set; }

    }

    public class filterValuesDTO
    {
        public int ID { get; set; }
        public string Value { get; set; }

    }
}
