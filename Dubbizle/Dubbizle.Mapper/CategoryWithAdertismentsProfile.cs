using AutoMapper;
using Dubbizle.DTOs;
using Dubbizle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.Mapper
{
    internal class CategoryWithAdertismentsProfile:Profile
    {
        public CategoryWithAdertismentsProfile()
        {
            CreateMap<Category, CategoryWithAdvertismentDTO>();
        }
    }
}
