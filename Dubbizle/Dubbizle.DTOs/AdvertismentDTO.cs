using Microsoft.AspNetCore.Http;
using Dubbizle.Models;
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
        public bool IsPremium { get; set; }
        public List<filterValuKey> Advertisment_FiltrationValuesList { get; set; }
        public List<string> AdvertismentImagesList { get; set; }
        public List<RentOptionDTO> advertisment_RentOptionsList { get; set; }
        public bool IsSaved { get; set; }
        public string ApplicationUserId { get; set; }

    }

    public class NotActiveAdvertismntDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string SubCategoryName { get; set; }
        public string Image { get; set; }

    }


    public class PostAdvertismentDTO
    {
        public int? ID { get; set; }
        public string Title { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public string AdType { get; set; }
        public string Location { get; set; }
        public string AdvertismentFiltrationValuesList { get; set; }
        public List<IFormFile> AdvertismentImagesList { get; set; }
        public string AdvertismentRentOptions { get; set; }
        public string ApplicationUserId { get; set; }
    }

    public class filterValueDTo2
    {
        public int filterValueID { get; set; }
    }

    public class RentOptionDTO
    {
        public int Duration { get; set; }
        public string Unit { get; set; }
        public float Cost { get; set; }
    }


    public class filterValuKey
    {
        public int id { get; set; }
        public string filtervalue { get; set; }
    }

}