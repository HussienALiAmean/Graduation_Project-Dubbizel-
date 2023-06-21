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
        public async Task<IActionResult> GetNameAndEmail(string id)
        {
            ApplicationUser orgUser = await _userManager.FindByIdAsync(id);
            ProfileDTO profileDTO = new ProfileDTO();

            profileDTO.userName = orgUser.UserName;

            profileDTO.Gender = orgUser.Gender;
            profileDTO.PhoneNumber = orgUser.PhoneNumber;
            profileDTO.Email = orgUser.Email;
            profileDTO.BirthDate = (DateTime)orgUser.BirthDate;
            profileDTO.Day = profileDTO.BirthDate.Day.ToString();
            profileDTO.Month = profileDTO.BirthDate.Month.ToString();
            profileDTO.Year = profileDTO.BirthDate.Year.ToString();
            profileDTO.AboutMe= orgUser.AboutMe;
            profileDTO.AboutMe = orgUser.AboutMe;

            return Ok(profileDTO);

        }

        [HttpPut("id")]
        public async Task<IActionResult> Edit(string id, ProfileDTO newUser)
        {
            ApplicationUser orgUser = await _userManager.FindByIdAsync(id);
            orgUser.UserName = newUser.userName;
            orgUser.Email = newUser.Email;
            orgUser.Gender = newUser.Gender;
            orgUser.PhoneNumber = newUser.PhoneNumber;
           // orgUser.BirthDate = newUser.BirthDate;
            // orgUser.BirthDate = newUser.BirthDate;
            DateTime date = new DateTime(int.Parse(newUser.Year), int.Parse(newUser.Month), int.Parse(newUser.Day));
            orgUser.BirthDate = date;

            //newUser.Day = newUser.BirthDate.Day.ToString();
            //newUser.Month = newUser.BirthDate.Month.ToString();
            //newUser.Year = newUser.BirthDate.Year.ToString();
            orgUser.AboutMe = newUser.AboutMe;
            await _userManager.UpdateAsync(orgUser);

            return Ok(newUser);

        }

    }
}
