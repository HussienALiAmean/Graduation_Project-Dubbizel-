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
      
        public void PostApplicationUser_Package(PackageAppUserDTO packageAppUserDTO)
        {
            ApplicationUser_Package applicationUser_Package = new ApplicationUser_Package();
            applicationUser_Package.ApplicationUserId= packageAppUserDTO.ApplicationUserId;
            applicationUser_Package.PackageID = packageAppUserDTO.PackageID;
            applicationUser_Package.NumOfRemainAds = packageAppUserDTO.NumOfRemainAds;
            applicationUser_Package.ExpirationDate = DateTime.Now.AddDays(30);
            _applicationUser_PackageRepo.Add(applicationUser_Package);
            _applicationUser_PackageRepo.SaveChanges();
        }

    }
}