using Dubbizle.Models;
using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;
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
        public List<ReviewDto> ReviewsList { get; set; }= new List<ReviewDto>();
        public List<AdvertismentRentOptionDTO> Advertisment_RentOptionList { get; set; } = new List<AdvertismentRentOptionDTO>();
        public List<AdvertismentImageDTO> AdvertismentImagesList { get; set; } = new List<AdvertismentImageDTO>();
        public List<AdvertismentFilterValueDTO> Advertisment_FiltrationValuesList { get; set; } = new List<AdvertismentFilterValueDTO>();
        //public string Title { get; set; }
        //public string Location { get; set; }
        //public string ApplicationUserId { get; set; }
        //public string ApplicationUserName { get; set; }
        //public string ApplicationEmail { get; set; }
        //public string AdType { get; set; }
        //public string AdStatus { get; set; }
        //public DateTime Date { get; set; }
        //public DateTime ExpirationDate { get; set; }
        //public DateTime ExpireDateOfPremium { get; set; }
        //public List<AdvertismentImageDTO> AdvertismentImagesList { get; set; } = new List<AdvertismentImageDTO>();
        //public List<AdvertismentFilterValueDTO> Advertisment_FiltrationValuesList { get; set; } = new List<AdvertismentFilterValueDTO>();
        //public List<AdvertismentRentOptionDTO> Advertisment_RentOptionList { get; set; } = new List<AdvertismentRentOptionDTO>();
        //public List<ReviewDTO> ReviewsList { get; set; }=new List<ReviewDTO>();
    }
    public class AdvertismentFilterValueDTO
    {
        public string Key { get; set; }
        public string Value { get; set; }

    }
    public class ReviewDTO
    {
        public string Text { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }


    }
}
