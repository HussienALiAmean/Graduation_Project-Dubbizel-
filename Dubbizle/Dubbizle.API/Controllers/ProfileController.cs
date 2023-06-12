using Dubbizle.Data.Repository;
using Dubbizle.Data.UnitOfWork;
using Dubbizle.DTOs;
using Dubbizle.Models;
using Dubbizle.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Dubbizle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        ProfileService _profileService;

        UnitOfWork _unitOfWork;
        //ProfileService profileService
        readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager) 
        {
            //_profileService = profileService;
            _userManager= userManager;
        }
        [HttpGet("getProfile/{id}")]
        public async Task <IActionResult> GetNameAndEmail(string id)
        {
            ApplicationUser orgUser = await _userManager.FindByIdAsync(id);
            ProfileDTO profileDTO =new ProfileDTO();

            profileDTO.userName = orgUser.UserName;

            profileDTO.Gender = orgUser.Gender;
            profileDTO.PhoneNumber = orgUser.PhoneNumber;
            profileDTO.Email = orgUser.Email;
            profileDTO.BirthDate = (DateTime)orgUser.BirthDate;
            profileDTO.Day = profileDTO.BirthDate.Day;
            profileDTO.Month = profileDTO.BirthDate.Month;
            profileDTO.Year = profileDTO.BirthDate.Year;
            profileDTO.AboutMe= orgUser.AboutMe;

            return Ok(profileDTO);
            
        }

        [HttpPut("id")]
        public async Task <IActionResult> Edit(string id,ProfileDTO newUser)
        {
            ApplicationUser orgUser = await _userManager.FindByIdAsync(id);
            orgUser.UserName = newUser.userName;
            orgUser.Email = newUser.Email;
            orgUser.Gender = newUser.Gender;
            orgUser.PhoneNumber = newUser.PhoneNumber;
            orgUser.BirthDate = newUser.BirthDate;
            newUser.Day= newUser.BirthDate.Day;
            newUser.Month = newUser.BirthDate.Month;
            newUser.Year = newUser.BirthDate.Year;
            orgUser.AboutMe = newUser.AboutMe;
            await _userManager.UpdateAsync(orgUser);

            return Ok(newUser);

        }

    }
}
