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

        public AdvertismentController(AdvertismentServise advertismentServise)
        {
            _advertismentServise = advertismentServise;  
        }


        // Alzhraa 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ResultDTO resultDTO = new ResultDTO();
            List<Advertisment> advertisments=(List<Advertisment>)_advertismentServise.GetAll("Advertisment_FiltrationValuesList.filtrationValue");
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
                advertismentDTOs.Add(advertismentDTO);
            }

            resultDTO.StatusCode= 200;
            resultDTO.Data= advertismentDTOs;
            return Ok(resultDTO);
        }
    }
}
