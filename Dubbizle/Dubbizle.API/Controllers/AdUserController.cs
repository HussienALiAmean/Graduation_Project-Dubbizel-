using Dubbizle.DTOs;
using Dubbizle.Services;
using Microsoft.AspNetCore.Http;
using Dubbizle.Models;
using Dubbizle.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Dubbizle.Data;

namespace Dubbizle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdUserController : ControllerBase
    {
        AdUserService _AdUserService;
        public AdUserController( AdUserService adUserService) {
            _AdUserService = adUserService;
         }

        [HttpPut("DeActivateMyAd")]
        public ResultDTO DeActivateMyAd(int id)
        {
            ResultDTO resultDTO = new ResultDTO();
            Advertisment advertisment = _AdUserService.GetAdvertismentByID(id);
            advertisment.AdStatus = "Moderated";
            _AdUserService.EditAdvertismentState(advertisment);
            resultDTO.StatusCode = 200;
            return resultDTO;
        }

        [HttpPut("AvtivateMyAd")]
        public ResultDTO AvtivateMyAd(int id)
        {
            ResultDTO resultDTO = new ResultDTO();
            Advertisment advertisment = _AdUserService.GetAdvertismentByID(id);
            advertisment.AdStatus = "Active";
            _AdUserService.EditAdvertismentState(advertisment);
            resultDTO.StatusCode = 200;
            return resultDTO;
        }




    }
}
