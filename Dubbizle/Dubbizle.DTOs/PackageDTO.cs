using Dubbizle.Models;
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

    public class PackageAppUserDTOCreated
    {
        public string ApplicationUserId { get; set; }
        public int PackageID { get; set; }
        public int NumOfRemainAds { get; set; }
    }


    public class PackageAppUserDTO
    {
        public int NumOfRemainAds { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Status { get; set; }
    }


}
