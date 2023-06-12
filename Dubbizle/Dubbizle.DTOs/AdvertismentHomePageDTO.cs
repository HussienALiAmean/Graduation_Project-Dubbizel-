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
        public string Title { get; set; }
        public string AdType { get; set; }
        public string AdStatus { get; set; }
        public string Location { get; set; }
        public DateTime PostedAt { get; set; }
        public List<AdvertismentImageDTO> AdvertismentImagesList { get; set; }
        public List<Advertisment_RentOptionDTO> Advertisment_RentOptionList { get; set; }
       

    }
}
