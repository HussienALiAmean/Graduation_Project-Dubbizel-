using AutoMapper;
using Dubbizle.DTOs;
using Dubbizle.Models;
using System.Security.Cryptography;

namespace Dubbizle.Mapper
{
    public class CategoryWithSubCategoryProfile:Profile
    {
        public CategoryWithSubCategoryProfile() {
            CreateMap<Category, CategoryWithSubCategoriesDTO>();
        }
        

    }
}