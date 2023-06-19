using Dubbizle.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.DTOs
{
    public class Advertisments_s_User
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int CountAds { get; set; }
        public List<AdvertismetsUsersDTO> advertismetsUsersDTOs { get; set; } = new List<AdvertismetsUsersDTO>();
    }
    public class AdvertismetsUsersDTO
    {
       public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string AdType { get; set; }
        public string AdStatus { get; set; }
        public DateTime Date { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime ExpireDateOfPremium { get; set; }
        public List<AdvertismentImageDTO> imageDTOs { get; set; }=new List<AdvertismentImageDTO>();
        public List<AdvertismentRentOptionDTO> Advertisment_RentOptionList { get; set; } = new List<AdvertismentRentOptionDTO>();
    }
}
