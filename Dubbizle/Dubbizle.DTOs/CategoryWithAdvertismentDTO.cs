using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.DTOs
{
    public class CategoryWithAdvertismentDTO
    {
        public string Name { get; set; }
        public List<AdvertismentHomePageDTO> CategoryAdvertismentsList { get; set; }
    }
}
