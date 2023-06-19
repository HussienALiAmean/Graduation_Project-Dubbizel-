using Dubbizle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.DTOs
{
    public class AdvertismentDetailsDTO
    {
        public string AdStatus { get; set; }
        public string Title { get; set; }
        public string AdType { get; set; }
        public string ApplicationUserId { get; set; }
        public string ApplicationUserName { get; set; }
        public string ApplicationEmail { get; set; }
        public DateTime Date { get; set; }

        public DateTime ExpirationDate { get; set; }
        public DateTime ExpireDateOfPremium { get; set; }
        public string Location { get; set; }
        public List<ReviewDTO> ReviewsList { get; set; }
        public List<AdvertismentRentOptionDTO> Advertisment_RentOptionList { get; set; }
        public List<AdvertismentImageDTO> AdvertismentImagesList { get; set; }
        public List<AdvertismentFilterValueDTO> Advertisment_FiltrationValuesList { get; set; }
    }
}
