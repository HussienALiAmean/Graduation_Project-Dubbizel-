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
    public class AdvertismentController : ControllerBase
    {
        private readonly AdvertismentServise _advertismentServise;
        private readonly CategoryServise _categoryServise;

        public AdvertismentController(AdvertismentServise advertismentServise, CategoryServise categoryServise)
        {
            _advertismentServise = advertismentServise;
            _categoryServise = categoryServise;
        }


        // Alzhraa & Hussien
        [HttpGet("subCategoryID")]
        public async Task<IActionResult> GetAllBySubCategoryID(int subCategoryID)
        {
            ResultDTO resultDTO = new ResultDTO();
            List<Advertisment> advertisments=(List<Advertisment>)_advertismentServise.GetAllBySubCategoryID("Advertisment_FiltrationValuesList.filtrationValue", "AdvertismentImagesList", subCategoryID);
            List< AdvertismentDTO > advertismentDTOs = new List<AdvertismentDTO>();
            AdvertismentDTO advertismentDTO;

            foreach (Advertisment ad in advertisments)
            {
                advertismentDTO = new AdvertismentDTO();
                advertismentDTO.ID= ad.ID;
                advertismentDTO.Title= ad.Title;
                advertismentDTO.CategoryID= ad.CategoryID;
                advertismentDTO.SubCategoryID= ad.SubCategoryID;
                advertismentDTO.AdType= ad.AdType;
                advertismentDTO.AdStatus= ad.AdStatus;
                advertismentDTO.Location= ad.Location;
                advertismentDTO.Date= ad.Date;
                advertismentDTO.ExpirationDate= ad.ExpirationDate;
                advertismentDTO.ExpireDateOfPremium= ad.ExpireDateOfPremium;
                if(ad.ExpireDateOfPremium>DateTime.Now)
                    advertismentDTO.IsPremium= true;
                else
                    advertismentDTO.IsPremium= false;
                advertismentDTO.Advertisment_FiltrationValuesList = new List<string>();
                foreach(Advertisment_FiltrationValue item in ad.Advertisment_FiltrationValuesList)
                {
                    advertismentDTO.Advertisment_FiltrationValuesList.Add(item.filtrationValue.Value);
                }
                advertismentDTO.AdvertismentImagesList= new List<string>(); 
                foreach (AdvertismentImage item in ad.AdvertismentImagesList)
                {
                    advertismentDTO.AdvertismentImagesList.Add(item.ImageName);
                }
                
                advertismentDTOs.Add(advertismentDTO);
            }

            resultDTO.StatusCode= 200;
            resultDTO.Data= advertismentDTOs;
            return Ok(resultDTO);
        }



        //Alzhraa & Hussien
        [HttpGet("CategoryID")]
        public async Task<IActionResult> GetAllByCategoryID(int CategoryID)
        {
            ResultDTO resultDTO = new ResultDTO();
            List<Advertisment> advertisments = (List<Advertisment>)_advertismentServise.GetAllByCategoryID("Advertisment_FiltrationValuesList.filtrationValue", "AdvertismentImagesList", CategoryID);
            List<AdvertismentDTO> advertismentDTOs = new List<AdvertismentDTO>();
            List<Category> categories = (List<Category>)_categoryServise.Get(e =>  e.ParentCategoryID == CategoryID);

            AdvertismentDTO advertismentDTO;

            foreach (Advertisment ad in advertisments)
            {
                advertismentDTO = new AdvertismentDTO();
                advertismentDTO.ID = ad.ID;
                advertismentDTO.Title = ad.Title;
                advertismentDTO.CategoryID = ad.CategoryID;
                advertismentDTO.SubCategoryID = ad.SubCategoryID;
                advertismentDTO.AdType = ad.AdType;
                advertismentDTO.AdStatus = ad.AdStatus;
                advertismentDTO.Location = ad.Location;
                advertismentDTO.Date = ad.Date;
                advertismentDTO.ExpirationDate = ad.ExpirationDate;
                advertismentDTO.ExpireDateOfPremium = ad.ExpireDateOfPremium;
                if (ad.ExpireDateOfPremium > DateTime.Now)
                    advertismentDTO.IsPremium = true;
                else
                    advertismentDTO.IsPremium = false;
                advertismentDTO.Advertisment_FiltrationValuesList = new List<string>();
                foreach (Advertisment_FiltrationValue item in ad.Advertisment_FiltrationValuesList)
                {
                    advertismentDTO.Advertisment_FiltrationValuesList.Add(item.filtrationValue.Value);
                }
                advertismentDTO.AdvertismentImagesList = new List<string>();
                foreach (AdvertismentImage item in ad.AdvertismentImagesList)
                {
                    advertismentDTO.AdvertismentImagesList.Add(item.ImageName);
                }
                advertismentDTOs.Add(advertismentDTO);
            }
            resultDTO.StatusCode = 200;
            resultDTO.Data = advertismentDTOs;
            return Ok(resultDTO);
        }
    }
}
