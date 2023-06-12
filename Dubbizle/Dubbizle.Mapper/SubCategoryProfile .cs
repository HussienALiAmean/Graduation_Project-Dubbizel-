using AutoMapper;
using Dubbizle.DTOs;
using Dubbizle.Models;
using System.Security.Cryptography;

namespace Dubbizle.Mapper
{
    public class SubCategoryProfile:Profile
    {
        public SubCategoryProfile() {
            CreateMap<Category, SubCategoryDTO>();
        }
        

    }
}