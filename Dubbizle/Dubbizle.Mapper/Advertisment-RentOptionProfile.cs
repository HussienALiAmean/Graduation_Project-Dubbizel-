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
    internal class Advertisment_RentOptionProfile:Profile
    {
        public Advertisment_RentOptionProfile()
        {
            CreateMap<Advertisment_RentOption, AdvertismentRentOptionDTO>();
        }
    }
}
