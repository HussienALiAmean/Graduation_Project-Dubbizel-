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
        readonly UserManager<ApplicationUser> _userManager;
        IRepository<Advertisment> _advertismentRepo;
        IRepository<Review> _reviewRepo;
        IRepository<ApplicationUser_Package> _packageRepo;
        FavoriteService _favoriteService;
        //FilterValueService _filtrationValue;
        IRepository<FiltrationValue> _filtrationValueRepo;
        public ProfileController(UserManager<ApplicationUser> userManager, IRepository<Advertisment> advertismentRepo, 
            IRepository<Review> reviewRepo, IRepository<ApplicationUser_Package> packageRepo, FavoriteService favoriteService, IRepository<FiltrationValue> filtrationValueRepo ) 
        {
            _userManager= userManager;
            _advertismentRepo = advertismentRepo;
            _reviewRepo = reviewRepo;
            _packageRepo = packageRepo;
            _favoriteService = favoriteService;
            _filtrationValueRepo = filtrationValueRepo;
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
            if(orgUser.BirthDate!=null)
            {
                profileDTO.BirthDate = (DateTime)orgUser.BirthDate;
                profileDTO.Day = ((DateTime)profileDTO.BirthDate).Day.ToString();
                profileDTO.Month = ((DateTime)profileDTO.BirthDate).Month.ToString();
                profileDTO.Year = ((DateTime)profileDTO.BirthDate).Year.ToString();
            }
          
            profileDTO.AboutMe= orgUser.AboutMe;
            profileDTO.AboutMe = orgUser.AboutMe;

            return Ok(profileDTO);

        }

        [HttpPut("editProfile/{id}")]
        public async Task<IActionResult> Edit(string id, ProfileDTO newUser)
        {
            ApplicationUser orgUser = await _userManager.FindByIdAsync(id);
            orgUser.UserName = newUser.userName;
            orgUser.Email = newUser.Email;
            orgUser.Gender = newUser.Gender;
            orgUser.PhoneNumber = newUser.PhoneNumber;
            if(newUser.Year!=null && newUser.Month!=null && newUser.Day!=null)
            {
                DateTime date = new DateTime(int.Parse(newUser.Year), int.Parse(newUser.Month), int.Parse(newUser.Day));
                orgUser.BirthDate = date;
            }
            

            orgUser.AboutMe = newUser.AboutMe;
            await _userManager.UpdateAsync(orgUser);

            return Ok(newUser);

        }


        [HttpDelete("deleteProfile/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser AppUser = await _userManager.FindByIdAsync(id);
            AppUser.Deleted = true;
            await _userManager.UpdateAsync(AppUser);

            List<Advertisment> advertismentsList =_advertismentRepo.GetAll("Advertisment_FiltrationValuesList.filtrationValue", "AdvertismentImagesList").Where(ad => ad.ApplicationUserId == id).ToList();
            foreach (Advertisment advertisment in advertismentsList)
            {
                advertisment.Deleted = true;
                foreach (AdvertismentImage adImage in advertisment.AdvertismentImagesList)
                {
                    adImage.Deleted = true;
                }

                foreach (Advertisment_FiltrationValue filterValue in advertisment.Advertisment_FiltrationValuesList)
                {
                    filterValue.Deleted = true;
                }
            }

            List<Favorite> favoritesList = (List<Favorite>)_favoriteService.GetAllByUserId(id);
            foreach (Favorite favorite in favoritesList)
            {
                favorite.Deleted = true;
            }

            List<Review> reviewsList = _reviewRepo.GetAll().Where(rev => rev.AutherId == id).ToList();
            foreach (Review review in reviewsList)
            {
                review.Deleted = true;
            }

            List<ApplicationUser_Package> packagesList = _packageRepo.GetAll().Where(p => p.ApplicationUserId == id).ToList();
            foreach (ApplicationUser_Package package in packagesList)
            {
                package.Deleted = true;
            }

           
            return Ok();
        }

     }
}
