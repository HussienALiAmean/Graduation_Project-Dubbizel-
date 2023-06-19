﻿using Dubbizle.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.DTOs
{
    public class PackageDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int NumOfAds { get; set; }
        public int NumOfPremiumDays { get; set; }
        public float Cost { get; set; }
        public int AdDuration { get; set; }
        public int SubCategoryID { get; set; }
        public string? SubCategoryName { get; set; }

    }
}
