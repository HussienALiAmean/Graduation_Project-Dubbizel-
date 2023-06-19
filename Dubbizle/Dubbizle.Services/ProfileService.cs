using AutoMapper;
using Dubbizle.Data.UnitOfWork;
using Dubbizle.DTOs;
using Dubbizle.Models;
using Microsoft.AspNetCore.Identity;

namespace Dubbizle.Services
{
    public class ProfileService
    {
        UnitOfWork _unitOfWork;
        //IMapper _mapper;
        readonly UserManager<ApplicationUser> _userManager;
        UnitOfWork unitOfWork;
        public ProfileService(UserManager<ApplicationUser> userManager, UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_mapper = mapper;
            _userManager = userManager;
        }
        //public async Task<ApplicationUser> Edit(string id, ProfileDTO newUser)
        //{
        //    ApplicationUser orgUser = await _userManager.FindByIdAsync(id);
        //    orgUser.Gender = newUser.Gender;
        //    orgUser.PhoneNumber = newUser.PhoneNumber;
        //    orgUser.BirthDate = newUser.BirthDate;
        //    orgUser.AboutMe = newUser.AboutMe;
        //    await _userManager.UpdateAsync(orgUser);
        //    return orgUser;
        //}

        public void SaveChanges()
        {

            _unitOfWork.SaveChanges();
            //_userrepository.savechanges();  
        }

    }
}
