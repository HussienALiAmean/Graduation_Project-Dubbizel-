using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dubbizle.Data.Repository;
using Dubbizle.Data.UnitOfWork;
﻿using Dubbizle.Data.Repository;
using Dubbizle.DTOs;
using Dubbizle.Models;
using System.Linq.Expressions;

namespace Dubbizle.Services
{
    public class PackageServise
    {
        IRepository<Package> _packageRepo;
        IRepository<ApplicationUser_Package> _applicationUser_PackageRepo;
        public PackageServise(IRepository<Package> packageRepo, IRepository<ApplicationUser_Package> applicationUser_PackageRepo)
        {
            _packageRepo= packageRepo;
            _applicationUser_PackageRepo = applicationUser_PackageRepo;
        }
        
        public IEnumerable<Package> GetAllBySubCategoryID(int SubCategoryID)
        {
            return _packageRepo.Get(p=>p.SubCategoryID== SubCategoryID&&p.Deleted==false).ToList();
        } 
      
        public void PostApplicationUser_Package(PackageAppUserDTOCreated packageAppUserDTO)
        {
            ApplicationUser_Package applicationUser_Package = new ApplicationUser_Package();
            applicationUser_Package.ApplicationUserId= packageAppUserDTO.ApplicationUserId;
            applicationUser_Package.PackageID = packageAppUserDTO.PackageID;
            applicationUser_Package.NumOfRemainAds = packageAppUserDTO.NumOfRemainAds;
            applicationUser_Package.ExpirationDate = DateTime.Now.AddDays(30);
            _applicationUser_PackageRepo.Add(applicationUser_Package);
            _applicationUser_PackageRepo.SaveChanges();
        }

        public IEnumerable<PackageAppUserDTO> GetAllByUserID(string ApplicationUserId)
        {
            List<ApplicationUser_Package> applicationUser_Packages=_applicationUser_PackageRepo.Get(p => p.ApplicationUserId== ApplicationUserId).ToList();
            List<PackageAppUserDTO> packageAppUserDTOs = new List<PackageAppUserDTO>();
            PackageAppUserDTO packageAppUserDTO;
            foreach (var package in applicationUser_Packages)
            {
                packageAppUserDTO=new PackageAppUserDTO();
                packageAppUserDTO.ExpirationDate=package.ExpirationDate;
                packageAppUserDTO.NumOfRemainAds=package.NumOfRemainAds;
                if (DateTime.Now > packageAppUserDTO.ExpirationDate)
                {
                    packageAppUserDTO.Status = "Expired";
                }
                else
                {
                    packageAppUserDTO.Status = "Active";
                }
                packageAppUserDTOs.Add(packageAppUserDTO);
            }
            return packageAppUserDTOs;
        }
    }
}