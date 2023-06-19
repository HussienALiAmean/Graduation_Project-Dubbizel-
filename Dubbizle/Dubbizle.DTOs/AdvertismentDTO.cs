using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.DTOs
{
    public class AdvertismentDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public string AdType { get; set; }
        public string AdStatus { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime ExpireDateOfPremium { get; set; }
        
        public Boolean IsPremium { get; set; }
        public List<string> Advertisment_FiltrationValuesList { get; set; }
        public List<string> AdvertismentImagesList { get; set; }

    }
}
