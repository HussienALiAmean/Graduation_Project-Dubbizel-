using AutoMapper;
using Dubbizle.DTOs;
using Dubbizle.Models;

namespace Mapper
{
    public class ProfileMap:Profile
    {
        public ProfileMap()
        {
            CreateMap<ApplicationUser, ApplicationUser>();

        }

    }
}