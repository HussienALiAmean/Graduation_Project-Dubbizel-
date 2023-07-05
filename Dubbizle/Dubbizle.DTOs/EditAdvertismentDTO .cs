using Microsoft.AspNetCore.Http;
﻿using Dubbizle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.DTOs
{
    public class EditAdvertismentDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string AdType { get; set; }
        public string Location { get; set; }
        public List<int> AdvertismentFiltrationValuesList { get; set; }=new List<int>();
        public List<ImageDTO> AdvertismentImagesList { get; set; }= new List<ImageDTO>();
        public List<EditRentOptionDTO> AdvertismentRentOptions { get; set; }= new List<EditRentOptionDTO>();
    }

    public class EditRentOptionDTO
    {
        public int? ID { get; set; }
        public int Duration { get; set; }
        public string Unit { get; set; }
        public float Cost { get; set; }
    }

    public class ImageDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }


    public class SaveAdvertismentDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string AdvertismentFiltrationValuesList { get; set; }
        public List<IFormFile>? AdvertismentImagesList { get; set; } = new List<IFormFile>();
        public string? AdvertismentRentOptions { get; set; }
        public List<int>? DeletedImages { get; set; }= new List<int> ();
    }



}
