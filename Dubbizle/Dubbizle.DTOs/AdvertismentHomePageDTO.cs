using System;
﻿using Dubbizle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.DTOs
{
    public class AdvertismentHomePageDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AdType { get; set; }
        public string AdStatus { get; set; }
        public string Location { get; set; }
        public bool IsSaved { get; set; }
        public DateTime Date { get; set; }
        public List<AdvertismentImageDTO> AdvertismentImagesList { get; set; }=new List<AdvertismentImageDTO> ();
        public List<AdvertismentRentOptionDTO> Advertisment_RentOptionList { get; set; } = new List<AdvertismentRentOptionDTO>();   
       

    }
}
