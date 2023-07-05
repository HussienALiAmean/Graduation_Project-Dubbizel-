using Dubbizle.Data.Repository;
using Dubbizle.DTOs;
using Dubbizle.Models;
using Dubbizle.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dubbizle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        UserManager<ApplicationUser> userManager;
        AdvertismentService advertismentService;
        IRepository<FiltrationValue> repository;
        //public DetailsController(UserManager<ApplicationUser> _userManager, AdvertismentService _advertismentService, IRepository<Advertisment> _repository) 
        //{
        //    userManager= _userManager;
        //    advertismentService= _advertismentService;
        //    repository= _repository;
        //}

        //[HttpGet("Details/{AdID:int}")]
       
        //public async Task<IActionResult> getDetails(int AdID)
        //{
        //    List<Advertisment> advertisments = (List<Advertisment>)advertismentService.GetAllAdvertisments("AdvertismentImagesList", "Advertisment_FiltrationValuesList.filtrationValue", "Advertisment_RentOptionList", "ReviewsList");
        //    Advertisment advertisment = advertisments.FirstOrDefault(a => a.ID == AdID);
        //    ApplicationUser user = await userManager.FindByIdAsync(advertisment.ApplicationUserId);
        //    AdvertismentDetailsDTO advertismentDetailsDTO = new AdvertismentDetailsDTO();
        //    advertismentDetailsDTO.AdStatus = advertisment.AdStatus;
        //    advertismentDetailsDTO.Title = advertisment.Title;
        //    advertismentDetailsDTO.AdType = advertisment.AdType;
        //    advertismentDetailsDTO.ApplicationUserId = user.Id;
        //    advertismentDetailsDTO.ApplicationUserName = user.UserName;
        //    advertismentDetailsDTO.ApplicationEmail = user.Email;
        //    advertismentDetailsDTO.Date = advertisment.Date;
        //    advertismentDetailsDTO.ExpirationDate = advertisment.ExpirationDate;
        //    advertismentDetailsDTO.ExpireDateOfPremium = advertisment.ExpireDateOfPremium;
        //    advertismentDetailsDTO.Location = advertisment.Location;
        //    List<SubCategory_Filter> s = (List<SubCategory_Filter>)subCategory_FilterService.GetAllBySubCategory("Filter");
        //    foreach (Advertisment_FiltrationValue filter in advertisment.Advertisment_FiltrationValuesList)
        //    {
        //        FiltrationValue fil = repository.GetByID(filter.FiltrationValueID);
        //        SubCategory_Filter sub = s.FirstOrDefault(s => s.ID == fil.SubCategory_FilterID);


        //        AdvertismentFilterValueDTO advertismentValueDTO = new AdvertismentFilterValueDTO();
        //        advertismentValueDTO.Value = filter.filtrationValue.Value;
        //        advertismentValueDTO.Key = sub.Filter.Name;
        //        advertismentDetailsDTO.Advertisment_FiltrationValuesList.Add(advertismentValueDTO);
        //    }
        //    foreach (AdvertismentImage image in advertisment.AdvertismentImagesList)
        //    {
        //        AdvertismentImageDTO advertismentImageDTO = new AdvertismentImageDTO();
        //        advertismentImageDTO.ImageName = image.ImageName;
        //        advertismentDetailsDTO.AdvertismentImagesList.Add(advertismentImageDTO);
        //    }
        //    foreach (Advertisment_RentOption rentOption in advertisment.Advertisment_RentOptionList)
        //    {
        //        AdvertismentRentOptionDTO advertismentRentDTO = new AdvertismentRentOptionDTO();
        //        advertismentRentDTO.Cost = rentOption.Cost;
        //        advertismentDetailsDTO.Advertisment_RentOptionList.Add(advertismentRentDTO);

        //    }
        //    foreach (Review review in advertisment.ReviewsList)
        //    {
        //        ReviewDTO reviewDTO = new ReviewDTO();
        //        ApplicationUser applicationUser = await userManager.FindByIdAsync(review.AutherId);
        //        reviewDTO.text = review.Text;
        //        review.Auther.UserName = applicationUser.UserName;
        //        advertismentDetailsDTO.ReviewsList.Add(reviewDTO);
        //    }
        //    return Ok(advertismentDetailsDTO);
        //}
    }
}
